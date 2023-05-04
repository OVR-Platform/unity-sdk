/**
 * OVR Unity SDK License
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
 * with services provided by OVR.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OverSDK.VisualScripting
{
    public enum OverVariableType
    {
        None = 0,
        Bool = 1,
        Int = 2,
        Float = 3,
        Vector2 = 4,
        Vector3 = 5,
        Quaternion = 6,
        String = 7,

        Transform = 10,
        Object = 11,
        Renderer = 12,
        RectTransform = 13,
        LineRenderer = 14,

        Rigidbody = 20,

        Light = 30,

        Audio = 50,
        Video = 51,
        Animator = 52,

        //Button = 100,
        Text = 101,
        TextTMP = 102,
        Image = 103,
        RawImage = 104,
        Color = 105
    }

    [Serializable]
    public class OverVariableData
    {
        public string id;
        public OverVariableType type;
        public bool isGlobal;

        public int integerValue;
        public float floatValue;
        public string stringValue;
        public bool boolValue;

        public Vector2 vector2Value;
        public Vector3 vector3Value;
        public Quaternion QuaternionValue;

        public Transform transformValue;
        public GameObject gameObject;
        public Renderer renderer;
        public RectTransform rectTransform;
        public LineRenderer lineRenderer;

        public Rigidbody rigidbodyValue;
        public Light light;

        public AudioSource audioSource;
        public VideoPlayer videoPlayer;
        public Animator animator;

        //public Button button;
        public Text text;
        public TMPro.TextMeshProUGUI textTMP;

        public Image image;
        public RawImage rawImage;
        public Color color;

        public OverVariableData Clone()
        {
            return new OverVariableData()
            {
                id = id,
                type = type,
                integerValue = integerValue,
                floatValue = floatValue,
                stringValue = stringValue,
                boolValue = boolValue,
                vector2Value = vector2Value,
                vector3Value = vector3Value,
                QuaternionValue = QuaternionValue,
                transformValue = transformValue,
                gameObject = gameObject,
                renderer = renderer,
                rectTransform = rectTransform,
                lineRenderer = lineRenderer,

                audioSource = audioSource,
                videoPlayer = videoPlayer,
                animator = animator,

                rigidbodyValue = rigidbodyValue,
                light = light,

                //button = button,
                text = text,
                textTMP = textTMP,
                image = image,
                rawImage = rawImage,
                color = color
            };
        }
    }

    [Serializable]
    public class OverScriptData
    {
        [OverVariableList] public List<OverVariableData> variableDatas = new List<OverVariableData>();

        private Dictionary<string, OverVariableData> variableDict = new Dictionary<string, OverVariableData>();
        public Dictionary<string, OverVariableData> VariableDict
        {
            get
            {
                variableDict = new Dictionary<string, OverVariableData>();
                foreach (var data in variableDatas)
                {
                    variableDict.Add(data.id, data);
                }
                return variableDict;
            }
        }
    }

    public class OverScript : MonoBehaviour
    {
        [SerializeField][ReadOnly] OverGraph overGraph;
        public OverGraph OverGraph { get { return overGraph; } set { overGraph = value; OnOverGraphChanged(); } }

        public OverScriptData data;

        // MONOBEHAVIOURS

        void Awake()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourAwake();
        }

        void Start()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourStart();
        }

        void OnEnable()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourEnable();
        }

        void OnDisable()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourDisable();
        }

        void OnDestroy()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourDestroy();
        }

        // Updates
        void Update()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourUpdate();
        }

        void LateUpdate()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourLateUpdate();
        }

        void FixedUpdate()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourFixedUpdate();
        }

        private void OnOverGraphChanged()
        {
            if (OverGraph != null && string.IsNullOrEmpty(OverGraph.GUID))
                OverGraph.GUID = System.Guid.NewGuid().ToString();

            if (OverScriptManager.Main == null)
            {
                Debug.LogError("No manager found! Create one first in order to use variables.");
            }

            data.variableDatas.Clear();
            ReloadGraph();
        }

        void ReloadGraph()
        {
            if (OverGraph != null && string.IsNullOrEmpty(OverGraph.GUID))
            {
                OverGraph.GUID = Guid.NewGuid().ToString();
            }

            Dictionary<string, OverVariableData> globals = new Dictionary<string, OverVariableData>();

            if (OverScriptManager.Main != null)
            {
                OverScriptManager.Main.UpdateMappings();
                globals = OverScriptManager.Main.globalVariables.VariableDict;
            }

            if (OverGraph != null)
            {
                List<OverGetVariable> getNodes = OverGraph.GetGraphNodes<OverGetVariable>();
                List<OverSetVariable> setNodes = OverGraph.GetGraphNodes<OverSetVariable>();

                foreach (OverGetVariable overGet in getNodes)
                {
                    OverVariableData data = new OverVariableData();
                    data.id = overGet._id;
                    data.type = overGet.Type;

                    if (!this.data.VariableDict.ContainsKey(data.id) && !globals.ContainsKey(data.id))
                    {
                        this.data.variableDatas.Add(data);
                    }
                }

                foreach (OverSetVariable overSet in setNodes)
                {
                    OverVariableData data = new OverVariableData();
                    data.id = overSet._id;
                    data.type = overSet.Type;

                    if (!this.data.VariableDict.ContainsKey(data.id) && !globals.ContainsKey(data.id))
                    {
                        this.data.variableDatas.Add(data);
                    }
                }
            }
        }
    }
}
