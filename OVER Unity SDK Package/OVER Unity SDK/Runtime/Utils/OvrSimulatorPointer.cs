/**
 * OVER Unity SDK License
 *
 * Copyright 2021 Over The Realty
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * 1. The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * 2. All copies of substantial portions of the Software may only be used in connection
 * with services provided by OVER.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using OverSDK.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OverSDK
{
    /// <summary>
    /// Simulates in the Editor the taps the OVER app turns into SDK events.
    /// Left click (Game view focused) at the pointer = a tap:
    /// ClickOnScreen / ClickOnNotUIScreen, double clicks, Press / Click / LongPress messages to the touched object
    /// (OverClickableObjectUVS, OvrClickableObject), OverTouchListener ("On Touch" node)
    /// and world-space / overlay UI buttons.
    /// Lives on the OVER_PlayerSimulator, which is never published (CleanScene removes it).
    /// </summary>
    public class OvrSimulatorPointer : MonoBehaviour
    {
        public enum PointerMode { ScreenCenter, MousePosition }

        [Tooltip("ScreenCenter: taps where the dot is (first-person aim). MousePosition: taps under the mouse cursor.")]
        public PointerMode mode = PointerMode.ScreenCenter;
        public bool showPointer = true;
        [Range(2, 20)] public int pointerSize = 6;
        public Color pointerColor = Color.white;
        public bool logClicks = true;

        const float LongPressTime = 0.5f;     // same threshold as OverTouchListener
        const float DoubleClickTime = 0.3f;
        const float DoubleClickMaxDistance = 30f;

        Texture2D m_Dot;

        float m_PressStart = -1f;
        Vector2 m_PressPosition;
        // Where the UI was pressed and on which kind of canvas (see UIDown)
        Vector2 m_UIPosition;
        bool m_UIWorldOnly;
        float m_LastClickTime = -10f;
        Vector2 m_LastClickPosition;

        GameObject m_PressedUI;
        readonly List<RaycastResult> m_UIHits = new List<RaycastResult>();

        void Awake()
        {
            if (!Application.isEditor) enabled = false;
        }

        void Update()
        {
            // The right button (look around) does not block the tap: aim with the right, tap with the left
            Vector2 position = PointerPosition();

            if (Input.GetMouseButtonDown(0))
            {
                m_PressStart = Time.unscaledTime;
                m_PressPosition = position;
                m_PressedUI = UIDown(position, Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0) && m_PressStart >= 0f)
            {
                float duration = Time.unscaledTime - m_PressStart;
                m_PressStart = -1f;
                Release(m_PressPosition, duration);
            }
        }

        Vector2 PointerPosition() =>
            mode == PointerMode.ScreenCenter ? new Vector2(Screen.width * 0.5f, Screen.height * 0.5f) : (Vector2)Input.mousePosition;

        Camera RayCamera
        {
            get
            {
                var simulator = GetComponent<OvrPlayerSimulator>();
                return simulator != null && simulator.mainCamera != null ? simulator.mainCamera : Camera.main;
            }
        }

        void Release(Vector2 position, float duration)
        {
            bool longPress = duration >= LongPressTime;
            bool overUI = UIUp(longPress);

            Camera cam = RayCamera;
            RaycastHit hit = default;
            bool hasHit = cam != null && Physics.Raycast(cam.ScreenPointToRay(position), out hit, Mathf.Infinity);

            if (!longPress)
            {
                OverAppEventListeners.OnClickOnScreen(hit, position);
                if (!overUI) OverAppEventListeners.OnClickOnNotUIScreen(hit, position);

                bool isDouble = Time.unscaledTime - m_LastClickTime <= DoubleClickTime &&
                                Vector2.Distance(position, m_LastClickPosition) <= DoubleClickMaxDistance;
                if (isDouble)
                {
                    OverAppEventListeners.OnDoubleClickOnScreen(hit, position);
                    if (!overUI) OverAppEventListeners.OnDoubleClickOnNotUIScreen(hit, position);
                    m_LastClickTime = -10f;
                }
                else
                {
                    m_LastClickTime = Time.unscaledTime;
                    m_LastClickPosition = position;
                }
            }

            // Same dispatch as the app's TouchManager: SendMessage to the touched object, so every receiver
            // (OverClickableObjectUVS, legacy OvrClickableObject, ...) gets Press, then Click or LongPress
            if (!overUI && hasHit)
            {
                GameObject touched = hit.collider.gameObject;
                touched.SendMessage("Press", hit, SendMessageOptions.DontRequireReceiver);
                touched.SendMessage(longPress ? "LongPress" : "Click", hit, SendMessageOptions.DontRequireReceiver);
            }

            if (!overUI && cam != null) TouchListeners(cam, position, longPress);

            if (logClicks)
                Debug.Log($"[OVER][Simulator] {(longPress ? "Long press" : "Click")} at {position} -> " +
                          (overUI ? $"UI '{m_LastUITarget}'" : hasHit ? $"'{hit.collider.name}'" : "nothing"));
        }

        /// <summary>Same test as OverTouchListener: a ray with the listener's own layer mask must hit its collider.</summary>
        static void TouchListeners(Camera cam, Vector2 position, bool longPress)
        {
            Ray ray = cam.ScreenPointToRay(position);
            foreach (OverTouchListener listener in FindObjectsByType<OverTouchListener>(FindObjectsSortMode.None))
            {
                if (!listener.isActiveAndEnabled) continue;
                if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, listener.layerMask)) continue;
                if (hit.collider.gameObject != listener.gameObject) continue;

                if (longPress) listener.onLongPress?.Invoke(hit);
                else
                {
                    listener.onClick?.Invoke(hit);
                    listener.onPress?.Invoke(hit);
                }
            }
        }

        // ------------------------------------------------------------------ UI

        PointerEventData m_PointerData;
        string m_LastUITarget;
        EventSystem m_PointerEventSystem;

        /// <summary>
        /// The OVER app always has an EventSystem; an experience scene in the Editor usually does not,
        /// and without one no UI receives clicks. Create one (without input module: the clicks are ours).
        /// </summary>
        static EventSystem EnsureEventSystem()
        {
            if (EventSystem.current != null) return EventSystem.current;
            EventSystem existing = FindAnyObjectByType<EventSystem>();
            if (existing != null) return existing;
            var go = new GameObject("OVER_SimulatorEventSystem") { hideFlags = HideFlags.DontSave };
            return go.AddComponent<EventSystem>();
        }

        /// <summary>An existing input module (StandaloneInputModule of the OVER canvases) already clicks the UI under the cursor.</summary>
        static bool HasInputModule => EventSystem.current != null && EventSystem.current.currentInputModule != null;

        PointerEventData PointerData(Vector2 position)
        {
            EventSystem es = EnsureEventSystem();
            if (es == null) return null;
            if (m_PointerData == null || m_PointerEventSystem != es)
            {
                m_PointerData = new PointerEventData(es) { button = PointerEventData.InputButton.Left, pointerId = -100 };
                m_PointerEventSystem = es;
            }
            m_PointerData.position = position;
            return m_PointerData;
        }

        // worldOnly: only world-space canvases; otherwise only screen-space ones (overlay / camera); null: any
        GameObject RaycastUI(Vector2 position, bool? worldOnly)
        {
            PointerEventData data = PointerData(position);
            if (data == null) return null;
            AssignWorldCanvasCamera();
            m_UIHits.Clear();
            m_PointerEventSystem.RaycastAll(data, m_UIHits);
            foreach (RaycastResult hit in m_UIHits)
            {
                if (worldOnly.HasValue)
                {
                    Canvas canvas = hit.gameObject.GetComponentInParent<Canvas>();
                    bool world = canvas != null && canvas.rootCanvas.renderMode == RenderMode.WorldSpace;
                    if (world != worldOnly.Value) continue;
                }
                data.pointerCurrentRaycast = hit;
                return hit.gameObject;
            }
            return null;
        }

        /// <summary>
        /// A world-space canvas without event camera falls back to Camera.main, which does not exist here
        /// (the simulator camera is tagged "PlayerCam"): the app assigns its AR camera, we assign ours.
        /// </summary>
        void AssignWorldCanvasCamera()
        {
            Camera cam = RayCamera;
            if (cam == null) return;
            foreach (Canvas canvas in FindObjectsByType<Canvas>(FindObjectsSortMode.None))
                if (canvas.isRootCanvas && canvas.renderMode == RenderMode.WorldSpace && canvas.worldCamera == null)
                    canvas.worldCamera = cam;
        }

        /// <summary>
        /// ScreenCenter: the world-space canvases are aimed with the dot (first person, as before); the screen-space
        /// canvases are tapped under the cursor, as with a finger, unless an input module already does it.
        /// MousePosition: everything under the cursor, unless an input module already does it.
        /// </summary>
        GameObject UIDown(Vector2 pointer, Vector2 mouse)
        {
            GameObject target = null;
            if (mode == PointerMode.ScreenCenter)
            {
                // The input module clicks what is under the cursor: if the dot aims the same button, leave it to the module
                GameObject underCursor = HasInputModule ? RaycastUI(mouse, null) : null;
                GameObject cursorHandler = underCursor != null ? ExecuteEvents.GetEventHandler<IPointerClickHandler>(underCursor) : null;
                m_UIPosition = pointer; m_UIWorldOnly = true;
                target = RaycastUI(pointer, true);
                if (target != null && cursorHandler != null && ExecuteEvents.GetEventHandler<IPointerClickHandler>(target) == cursorHandler)
                    return null;
                if (target == null && !HasInputModule)
                {
                    m_UIPosition = mouse; m_UIWorldOnly = false;
                    target = RaycastUI(mouse, false);
                }
            }
            else if (!HasInputModule)
            {
                m_UIPosition = mouse; m_UIWorldOnly = false;
                target = RaycastUI(mouse, null);
            }
            if (target == null) return null;
            Vector2 position = m_UIPosition;

            PointerEventData data = m_PointerData;
            data.pressPosition = position;
            data.pointerPressRaycast = data.pointerCurrentRaycast;
            data.eligibleForClick = true;
            data.clickTime = Time.unscaledTime;
            GameObject pressed = ExecuteEvents.ExecuteHierarchy(target, data, ExecuteEvents.pointerDownHandler)
                                 ?? ExecuteEvents.GetEventHandler<IPointerClickHandler>(target);
            data.pointerPress = pressed;
            data.rawPointerPress = target;
            return pressed;
        }

        /// <summary>Returns true when the tap landed on UI (also with the mouse pointer, handled by the input module).</summary>
        bool UIUp(bool longPress)
        {
            // Pressed nothing ourselves: the tap is on UI if the input module has the cursor over it, or (ScreenCenter)
            // if the dot is on a world-space canvas
            if (m_PressedUI == null)
            {
                bool overModuleUI = HasInputModule && EventSystem.current.IsPointerOverGameObject();
                GameObject aimed = mode == PointerMode.ScreenCenter ? RaycastUI(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), true) : null;
                m_LastUITarget = aimed != null ? aimed.name : overModuleUI ? "(input module)" : null;
                return overModuleUI || aimed != null;
            }

            GameObject target = RaycastUI(m_UIPosition, mode == PointerMode.ScreenCenter ? m_UIWorldOnly : (bool?)null);
            m_LastUITarget = target != null ? target.name : null;
            if (m_PressedUI != null)
            {
                PointerEventData data = m_PointerData;
                ExecuteEvents.Execute(m_PressedUI, data, ExecuteEvents.pointerUpHandler);
                GameObject clickHandler = target != null ? ExecuteEvents.GetEventHandler<IPointerClickHandler>(target) : null;
                if (clickHandler == m_PressedUI && data.eligibleForClick)
                    ExecuteEvents.Execute(m_PressedUI, data, ExecuteEvents.pointerClickHandler);
                data.pointerPress = null;
                m_PressedUI = null;
            }
            return target != null;
        }

        // ------------------------------------------------------------------ pointer

        void OnGUI()
        {
            if (!showPointer || mode != PointerMode.ScreenCenter) return;
            if (m_Dot == null)
            {
                m_Dot = new Texture2D(1, 1) { hideFlags = HideFlags.HideAndDontSave };
                m_Dot.SetPixel(0, 0, Color.white);
                m_Dot.Apply();
            }
            float s = pointerSize;
            var center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            Color previous = GUI.color;
            GUI.color = new Color(0f, 0f, 0f, 0.6f * pointerColor.a);
            GUI.DrawTexture(new Rect(center.x - s * 0.5f - 1, center.y - s * 0.5f - 1, s + 2, s + 2), m_Dot);
            GUI.color = Input.GetMouseButton(0) ? Color.yellow : pointerColor;
            GUI.DrawTexture(new Rect(center.x - s * 0.5f, center.y - s * 0.5f, s, s), m_Dot);
            GUI.color = previous;
        }

        void OnDestroy()
        {
            if (m_Dot != null) Destroy(m_Dot);
        }
    }
}
