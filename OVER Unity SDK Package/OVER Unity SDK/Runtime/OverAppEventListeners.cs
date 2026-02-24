using OverSDK.VisualScripting;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OverSDK
{
    public class OverAppEventListeners : MonoBehaviour
    {
        /*************** Image Target Events for AR *****************/

        public static event Action<string> TargetImageFound = delegate { };
        public static event Action<string> TargetImageLost = delegate { };

        public enum ImageTargetEventType { Found, Lost }
        [HideInInspector]
        public ImageTargetEventType testImageTargetEventType;

        /*************** Camera Events for AR *****************/

        public static event Action ArCameraTeleported = delegate { };

        /************** Touch Events for AR/VR *******************/

        public enum OneTouchEventType { Enter, EnterOverUI, EnterNotOverUI, Exit, In, InNotOverUI, InOverUI }
        [HideInInspector]
        public OneTouchEventType testOneTouchEventType;

        public enum TwoTouchesEventType { Enter, EnterNotOverUI, EnterOverUI, Exit, In, InNotOverUI, InOverUI }
        [HideInInspector]
        public TwoTouchesEventType testTwoTouchesEventType;

        public enum ClickEventType { ClickOnScreen, ClickOnNotUIScreen }
        [HideInInspector]
        public ClickEventType testClickEventType;

        public enum DoubleClickEventType { DoubleClickOnScreen, DoubleClickOnNotUIScreen }
        [HideInInspector]
        public DoubleClickEventType testDoubleClickEventType;

        //One Touch events
        public static event Action<Vector2> OneTouchEnter = delegate { };
        public static event Action<Vector2> OneTouchEnterOverUI = delegate { };
        public static event Action<Vector2> OneTouchEnterNotOverUI = delegate { };
        public static event Action<Vector2> OneTouchExit = delegate { };
        public static event Action<Vector2, Vector2> OneTouchIn = delegate { };
        public static event Action<Vector2, Vector2> OneTouchInNotOverUI = delegate { };
        public static event Action<Vector2, Vector2> OneTouchInOverUI = delegate { };

        //Two Touches events
        public static event Action<Vector2, Vector2> TwoTouchesEnter = delegate { };
        public static event Action<Vector2, Vector2> TwoTouchesEnterNotOverUI = delegate { };
        public static event Action<Vector2, Vector2> TwoTouchesEnterOverUI = delegate { };
        public static event Action<Vector2, Vector2> TwoTouchesExit = delegate { };
        public static event Action<Vector2, Vector2, Vector2, Vector2> TwoTouchesIn = delegate { };
        public static event Action<Vector2, Vector2, Vector2, Vector2> TwoTouchesInNotOverUI = delegate { };
        public static event Action<Vector2, Vector2, Vector2, Vector2> TwoTouchesInOverUI = delegate { };

        //Click events
        public static event Action<RaycastHit, Vector2> ClickOnScreen = delegate { };
        public static event Action<RaycastHit, Vector2> ClickOnNotUIScreen = delegate { };

        //Double Click events
        public static event Action<RaycastHit, Vector2> DoubleClickOnScreen = delegate { };
        public static event Action<RaycastHit, Vector2> DoubleClickOnNotUIScreen = delegate { };


        public void Awake()
        {
            //Image Target Events for AR
            TargetImageFound += OnTargetImageFound;
            TargetImageLost += OnTargetImageLost;

            //Camera Events for AR
            ArCameraTeleported += OnArCameraTeleported;

            /************** Touch Events for AR/VR *******************/
            OneTouchEnter += OnOneTouchEnter;
            OneTouchEnterOverUI += OnOneTouchEnterOverUI;
            OneTouchEnterNotOverUI += OnOneTouchEnterNotOverUI;
            OneTouchExit += OnOneTouchExit;
            OneTouchIn += OnOneTouchIn;
            OneTouchInNotOverUI += OnOneTouchInNotOverUI;
            OneTouchInOverUI += OnOneTouchInOverUI;

            TwoTouchesEnter += OnTwoTouchesEnter;
            TwoTouchesEnterNotOverUI += OnTwoTouchesEnterNotOverUI;
            TwoTouchesEnterOverUI += OnTwoTouchesEnterOverUI;
            TwoTouchesExit += OnTwoTouchesExit;
            TwoTouchesIn += OnTwoTouchesIn;
            TwoTouchesInNotOverUI += OnTwoTouchesInNotOverUI;
            TwoTouchesInOverUI += OnTwoTouchesInOverUI;

            ClickOnScreen += OnClickOnScreen;
            ClickOnNotUIScreen += OnClickOnNotUIScreen;

            DoubleClickOnScreen += OnDoubleClickOnScreen;
            DoubleClickOnNotUIScreen += OnDoubleClickOnNotUIScreen;
        }

        private void OnDestroy()
        {
            //Image Target Events for AR
            TargetImageFound -= OnTargetImageFound;
            TargetImageLost -= OnTargetImageLost;

            //Camera Events for AR
            ArCameraTeleported -= OnArCameraTeleported;

            /************** Touch Events for AR/VR *******************/
            OneTouchEnter -= OnOneTouchEnter;
            OneTouchEnterOverUI -= OnOneTouchEnterOverUI;
            OneTouchEnterNotOverUI -= OnOneTouchEnterNotOverUI;
            OneTouchExit -= OnOneTouchExit;
            OneTouchIn -= OnOneTouchIn;
            OneTouchInNotOverUI -= OnOneTouchInNotOverUI;
            OneTouchInOverUI -= OnOneTouchInOverUI;

            TwoTouchesEnter -= OnTwoTouchesEnter;
            TwoTouchesEnterNotOverUI -= OnTwoTouchesEnterNotOverUI;
            TwoTouchesEnterOverUI -= OnTwoTouchesEnterOverUI;
            TwoTouchesExit -= OnTwoTouchesExit;
            TwoTouchesIn -= OnTwoTouchesIn;
            TwoTouchesInNotOverUI -= OnTwoTouchesInNotOverUI;
            TwoTouchesInOverUI -= OnTwoTouchesInOverUI;

            ClickOnScreen -= OnClickOnScreen;
            ClickOnNotUIScreen -= OnClickOnNotUIScreen;

            DoubleClickOnScreen -= OnDoubleClickOnScreen;
            DoubleClickOnNotUIScreen -= OnDoubleClickOnNotUIScreen;
        }



        /*************** Image Target Events for AR *****************/

#if APP_MAIN
        public static void InvokeTargetImageFound(string id)
        {
            OnTargetImageFound(id);
        }

        public static void InvokeTargetImageLost(string id)
        {
            OnTargetImageLost(id);
        }
#endif

        public static void OnTargetImageFound(string idImageTarget)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.OverArImageTargetFoundEvent, idImageTarget);
#endif
        }
        public static void OnTargetImageLost(string idImageTarget)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.OverArImageTargetLostEvent, idImageTarget);
#endif
        }

        /*************** Camera Events for AR *****************/

#if APP_MAIN
        public static void InvokeArCameraTeleported()
        {
            OnArCameraTeleported();
        }
#endif

        public static void OnArCameraTeleported()
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.ArCameraTeleportedEvent);
#endif
        }

        /************** Touch Events for AR/VR *******************/

#if APP_MAIN
        /**************** INVOKE METHODS ****************/

        //One Touch
        public static void InvokeOneTouchEnter(Vector2 new0)
        {
            OnOneTouchEnter(new0);
        }

        public static void InvokeOneTouchEnterOverUI(Vector2 new0)
        {
            OnOneTouchEnterOverUI(new0);
        }

        public static void InvokeOneTouchEnterNotOverUI(Vector2 new0)
        {
            OnOneTouchEnterNotOverUI(new0);
        }

        public static void InvokeOneTouchExit(Vector2 old0)
        {
            OnOneTouchExit(old0);
        }

        public static void InvokeOneTouchIn(Vector2 old0, Vector2 new0)
        {
            OnOneTouchIn(old0, new0);
        }

        public static void InvokeOneTouchInNotOverUI(Vector2 old0, Vector2 new0)
        {
            OnOneTouchInNotOverUI(old0, new0);
        }

        public static void InvokeOneTouchInOverUI(Vector2 old0, Vector2 new0)
        {
            OnOneTouchInOverUI(old0, new0);
        }

        //Two Touches
        public static void InvokeTwoTouchesEnter(Vector2 new0, Vector2 new1)
        {
            OnTwoTouchesEnter(new0, new1);
        }

        public static void InvokeTwoTouchesEnterNotOverUI(Vector2 new0, Vector2 new1)
        {
            OnTwoTouchesEnterNotOverUI(new0, new1);
        }

        public static void InvokeTwoTouchesEnterOverUI(Vector2 new0, Vector2 new1)
        {
            OnTwoTouchesEnterOverUI(new0, new1);
        }

        public static void InvokeTwoTouchesExit(Vector2 old0, Vector2 old1)
        {
            OnTwoTouchesExit(old0, old1);
        }

        public static void InvokeTwoTouchesIn(Vector2 old0, Vector2 old1, Vector2 new0, Vector2 new1)
        {
            OnTwoTouchesIn(old0, old1, new0, new1);
        }

        public static void InvokeTwoTouchesInNotOverUI(Vector2 old0, Vector2 old1, Vector2 new0, Vector2 new1)
        {
            OnTwoTouchesInNotOverUI(old0, old1, new0, new1);
        }

        public static void InvokeTwoTouchesInOverUI(Vector2 old0, Vector2 old1, Vector2 new0, Vector2 new1)
        {
            OnTwoTouchesInOverUI(old0, old1, new0, new1);
        }

        //Click
        public static void InvokeClickOnScreen(RaycastHit raycastHit, Vector2 position)
        {
            OnClickOnScreen(raycastHit, position);
        }

        public static void InvokeClickOnNotUIScreen(RaycastHit raycastHit, Vector2 position)
        {
            OnClickOnNotUIScreen(raycastHit, position);
        }

        //Double Click
        public static void InvokeDoubleClickOnScreen(RaycastHit raycastHit, Vector2 position)
        {
            OnDoubleClickOnScreen(raycastHit, position);
        }

        public static void InvokeDoubleClickOnNotUIScreen(RaycastHit raycastHit, Vector2 position)
        {
            OnDoubleClickOnNotUIScreen(raycastHit, position);
        }
#endif

        //One Touch
        public static void OnOneTouchEnter(Vector2 new0)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.OneTouchEnterEvent, new0);
#endif
        }

        public static void OnOneTouchEnterOverUI(Vector2 new0)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.OneTouchEnterOverUIEvent, new0);
#endif
        }

        public static void OnOneTouchEnterNotOverUI(Vector2 new0)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.OneTouchEnterNotOverUIEvent, new0);
#endif
        }

        public static void OnOneTouchExit(Vector2 old0)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.OneTouchExitEvent, old0);
#endif
        }

        public static void OnOneTouchIn(Vector2 old0, Vector2 new0)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.OneTouchInEvent, (old0, new0));
#endif
        }

        public static void OnOneTouchInNotOverUI(Vector2 old0, Vector2 new0)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.OneTouchInNotOverUIEvent, (old0, new0));
#endif
        }

        public static void OnOneTouchInOverUI(Vector2 old0, Vector2 new0)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.OneTouchInOverUIEvent, (old0, new0));
#endif
        }

        //Two Touches
        public static void OnTwoTouchesEnter(Vector2 new0, Vector2 new1)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.TwoTouchesEnterEvent, (new0, new1));
#endif
        }

        public static void OnTwoTouchesEnterNotOverUI(Vector2 new0, Vector2 new1)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.TwoTouchesEnterNotOverUIEvent, (new0, new1));
#endif
        }

        public static void OnTwoTouchesEnterOverUI(Vector2 new0, Vector2 new1)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.TwoTouchesEnterOverUIEvent, (new0, new1));
#endif
        }

        public static void OnTwoTouchesExit(Vector2 old0, Vector2 old1)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.TwoTouchesExitEvent, (old0, old1));
#endif
        }

        public static void OnTwoTouchesIn(Vector2 old0, Vector2 old1, Vector2 new0, Vector2 new1)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.TwoTouchesInEvent, (old0, old1, new0, new1));
#endif
        }

        public static void OnTwoTouchesInNotOverUI(Vector2 old0, Vector2 old1, Vector2 new0, Vector2 new1)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.TwoTouchesInNotOverUIEvent, (old0, old1, new0, new1));
#endif
        }

        public static void OnTwoTouchesInOverUI(Vector2 old0, Vector2 old1, Vector2 new0, Vector2 new1)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.TwoTouchesInOverUIEvent, (old0, old1, new0, new1));
#endif
        }

        //Click
        public static void OnClickOnScreen(RaycastHit raycastHit, Vector2 position)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.ClickOnScreenEvent, (raycastHit, position));
#endif
        }

        public static void OnClickOnNotUIScreen(RaycastHit raycastHit, Vector2 position)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.ClickOnNotUIScreenEvent, (raycastHit, position));
#endif
        }

        //Double Click
        public static void OnDoubleClickOnScreen(RaycastHit raycastHit, Vector2 position)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.DoubleClickOnScreenEvent, (raycastHit, position));
#endif
        }

        public static void OnDoubleClickOnNotUIScreen(RaycastHit raycastHit, Vector2 position)
        {
#if (!APP_MAIN && !SDK_NO_VS) || (APP_MAIN && OVR_PLUGIN_VISUALSCRIPTING)
            EventBus.Trigger(EventNames.DoubleClickOnNotUIScreenEvent, (raycastHit, position));
#endif
        }
    }
}