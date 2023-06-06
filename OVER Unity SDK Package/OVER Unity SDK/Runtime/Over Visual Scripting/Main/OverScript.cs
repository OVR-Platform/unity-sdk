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
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        Material = 15,
        ParticleSystem = 16,

        Rigidbody = 20,
        Collider = 21,

        Light = 30,

        AudioSource = 50,
        Video = 51,
        Animator = 52,
        AudioClip = 53,

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
        public string GUID;
        public string name;
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

        public Material material;
        public ParticleSystem particleSystem;

        public Rigidbody rigidbodyValue;
        public Collider colliderValue;

        public Light light;

        public AudioSource audioSource;
        public AudioClip audioClip;
        public VideoPlayer videoPlayer;
        public Animator animator;
        
        public Text text;
        public TMPro.TextMeshProUGUI textTMP;

        public Image image;
        public RawImage rawImage;
        public Color color;

        public OverVariableData Clone()
        {
            return new OverVariableData()
            {
                GUID = GUID,
                name = name,
                type = type,
                isGlobal = isGlobal,
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
                material = material,
                particleSystem = particleSystem,

                audioSource = audioSource,
                audioClip = audioClip,
                videoPlayer = videoPlayer,
                animator = animator,

                rigidbodyValue = rigidbodyValue,
                colliderValue = colliderValue,
                light = light,

                text = text,
                textTMP = textTMP,
                image = image,
                rawImage = rawImage,
                color = color
            };
        }

        public OverGraphVariableData ToGraphData()
        {
            return new OverGraphVariableData()
            {
                GUID = GUID,
                name = name,
                type = type,
                isGlobal = isGlobal
            };
        }
    }

    [Serializable]
    public class OverScriptData
    {
        [OverVariableList] public List<OverVariableData> variables = new List<OverVariableData>();

        private Dictionary<string, OverVariableData> variableDict = new Dictionary<string, OverVariableData>();
        public Dictionary<string, OverVariableData> VariableDict
        {
            get
            {
                variableDict = new Dictionary<string, OverVariableData>();
                foreach (var data in variables)
                {
                    if (string.IsNullOrEmpty(data.GUID) || variableDict.ContainsKey(data.GUID))
                    {
                        data.GUID = Guid.NewGuid().ToString();
                    }
                    variableDict.Add(data.GUID, data);
                }
                return variableDict;
            }
        }
    }

    public class OverScript : MonoBehaviour
    {
        [SerializeField][ReadOnly] public string GUID;  
        [SerializeField][ReadOnly] OverGraph overGraph;
        public OverGraph OverGraph { get { return overGraph; } set { overGraph = value; OnOverGraphChanged(); } }

        [SerializeField] OverScriptData data;
        public OverScriptData Data
        {
            get
            {
                if (data == null) data = new OverScriptData();
                return data;
            }
            set { data = value; }
        }
        public bool MarkedAsDirty { get; set; }


        //inner
        private bool isReloading = false;
        [SerializeField][HideInInspector] List<OverGraphVariableData> ghostVariables = new List<OverGraphVariableData>();

        // MONOBEHAVIOURS
        private void OnValidate()
        {
            Dictionary<string, OverVariableData> dict = Data.VariableDict;

            if (string.IsNullOrEmpty(GUID))
            {
                GUID = Guid.NewGuid().ToString();
            }

            if (!isReloading && ghostVariables.Count != Data.variables.Count)
            {
                //Debug.LogError("something has been added removed....");
                if (overGraph != null) overGraph.UpdateDataFromScript(this);
                
                ghostVariables = new List<OverGraphVariableData>(Data.variables.Select(x => x.ToGraphData()));
            }

            if (overGraph != null && Data.variables.Count == 0)
            {
                //Debug.LogError("SCR - Empty");
                overGraph.Data.variables.Clear();
            }

            //bruttino
            for (int i = 0; i < Data.variables.Count; i++)
            {
                if (Data.variables[i].name != ghostVariables[i].name || 
                    Data.variables[i].type != ghostVariables[i].type)
                {
                    ApplyVariableChangesTo(Data.variables[i], OverGraph);
                }
            }
        }

        void Awake()
        {
            if(OverScriptManager.Main != null && !OverScriptManager.Main.overDataMappings.ContainsKey(GUID)) 
            {
                GUID = Guid.NewGuid().ToString();
                OverScriptManager.Main.UpdateMappings();
            }

            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourAwake(this);
        }

        void Start()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourStart(this);
        }

        void OnEnable()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourEnable(this);
        }

        void OnDisable()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourDisable(this);
        }

        void OnDestroy()
        {
            if (OverScriptManager.Main != null)
            {
                OverScriptManager.Main.UpdateMappings();
            }

            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourDestroy(this);
        }

        // Updates
        void Update()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourUpdate(this);
        }

        void LateUpdate()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourLateUpdate(this);
        }

        void FixedUpdate()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourFixedUpdate(this);
        }

        private void OnOverGraphChanged()
        {
            if (OverGraph != null && string.IsNullOrEmpty(OverGraph.GUID))
                OverGraph.GUID = System.Guid.NewGuid().ToString();

            if (OverScriptManager.Main == null)
            {
                Debug.LogError("No manager found! Create one first in order to use variables.");
            }

            Data.variables.Clear();
            ghostVariables.Clear();
            ReloadGraph();
        }

        void ReloadGraph()
        {
            isReloading = true;

            if (OverGraph != null && string.IsNullOrEmpty(OverGraph.GUID))
            {
                OverGraph.GUID = Guid.NewGuid().ToString();
            }

            HashSet<string> globals = new HashSet<string>();

            if (OverScriptManager.Main != null)
            {
                OverScriptManager.Main.UpdateMappings();
                foreach (OverVariableData variableData in OverScriptManager.Main.Data.variables)
                {
                    globals.Add(variableData.GUID);
                }
            }

            if (OverGraph != null)
            {
                //verify globals
                List<OverGetVariable> getNodes = OverGraph.GetGraphNodes<OverGetVariable>();
                List<OverSetVariable> setNodes = OverGraph.GetGraphNodes<OverSetVariable>();

                foreach (OverGetVariable overGet in getNodes)
                {
                    if (overGet.isGlobal)
                    {
                        OverVariableData data = new OverVariableData()
                        {
                            GUID = overGet.guid,
                            name = overGet._name,
                            type = overGet.Type,
                            isGlobal = overGet.isGlobal
                        };

                        if (!globals.Contains(data.GUID))
                        {
                            OverScriptManager.Main.Data.variables.Add(data);
                        }
                    }
                }

                foreach (OverSetVariable overSet in setNodes)
                {
                    if (overSet.isGlobal)
                    {
                        OverVariableData data = new OverVariableData()
                        {
                            GUID = overSet.guid,
                            name = overSet._name,
                            type = overSet.Type,
                            isGlobal = overSet.isGlobal
                        };

                        if (!globals.Contains(data.GUID))
                        {
                            OverScriptManager.Main.Data.variables.Add(data);
                        }
                    }
                }

                if (OverScriptManager.Main != null && globals.Count < OverScriptManager.Main.Data.variables.Count)
                {
                    OverScriptManager.Main.RefreshGlobals();
                    globals = new HashSet<string>(OverScriptManager.Main.Data.variables.Select(x => x.GUID));
                }

                //verify locals
                List<OverGraphVariableData> variableNodes = OverGraph.Data.variables;

                foreach (OverGraphVariableData variableNode in variableNodes)
                {
                    OverVariableData data = variableNode.ToScriptData();

                    if (!this.Data.VariableDict.ContainsKey(data.GUID) && !globals.Contains(data.GUID))
                    {
                        this.Data.variables.Add(data);
                    }
                }

                ghostVariables = new List<OverGraphVariableData>(Data.variables.Select(x => x.ToGraphData()));

            }

            isReloading = false;
            MarkedAsDirty = true;
        }

        public void OnUpdateGraphData(OverGraphData graphData)
        {
            OverScriptData scriptData = new OverScriptData();

            foreach (var data in graphData.variables)
            {
                if (Data.VariableDict.TryGetValue(data.GUID, out var variableData))
                {
                    variableData.name = data.name;
                    variableData.type = data.type;
                    scriptData.variables.Add(variableData.Clone());
                }
                else
                {
                    scriptData.variables.Add(data.ToScriptData());
                }
            }

            Data.variables = scriptData.variables;
            ghostVariables = Data.variables.Select(x => x.ToGraphData()).ToList();
            MarkedAsDirty = true;
        }

        public void ApplyVariableChangesTo(OverVariableData variable, OverGraph graph)
        {
            if (graph.Data.VariableDict.TryGetValue(variable.GUID, out var graphVariable))
            {
                graph.Data.variables[graph.Data.variables.IndexOf(graphVariable)] = variable.ToGraphData();
            }
            else
            {
                graph.Data.variables.Add(variable.ToGraphData());
            }

            graph.RemoveSurplusLocalVariables(this);
            graph.UpdateDataFromScript(this);

            ghostVariables = Data.variables.Select(x => x.ToGraphData()).ToList();
            MarkedAsDirty = true;
        }

        //da riesumare
        //public static Guid CreateGuidFromString(string input)
        //{
        //    using (SHA256 sha = SHA256.Create())
        //    {
        //        byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        //        byte[] b = new byte[16];
        //        Array.Copy(data, b, 16);
        //        return new Guid(b);
        //    }
        //}
    }
}
