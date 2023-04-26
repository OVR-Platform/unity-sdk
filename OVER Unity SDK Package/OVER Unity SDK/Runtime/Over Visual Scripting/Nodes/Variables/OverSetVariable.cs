/**
 * OVR Unity SDK License
 *
 * Copyright 2021 OVR
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

using BlueGraph;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OverSDK.VisualScripting
{
    [Node]
    public abstract class OverSetVariable : OverExecutionFlowNode
    {
        public string _id;
        public abstract object Value { get; }
        public abstract OverVariableType Type { get; }
        public override void OnEnable()
        {
            Name = $"{_id}";
            base.OnEnable();
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(System.Single), Multiple = true)]
    public class OverSetVariableInt : OverSetVariable
    {
        [Input("Int")] public System.Single newVariable;   
        
        protected System.Single _value;
        public override OverVariableType Type => OverVariableType.Int;

        public override object Value => _value;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            System.Single newVariable = GetInputValue("Int", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].integerValue = (int)newVariable;
                _value = localVariableDict[_id].integerValue;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].integerValue = (int)newVariable;
                    _value = globalVariableDict[_id].integerValue;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return (int)_value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(float), Multiple = true)]
    public class OverSetVariableFloat : OverSetVariable
    {
        [Input("Float")] public float newVariable;

        protected float _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Float;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            float newVariable = GetInputValue("Float", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].floatValue = newVariable;
                _value = localVariableDict[_id].floatValue;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].floatValue = newVariable;
                    _value = globalVariableDict[_id].floatValue;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(bool), Multiple = true)]
    public class OverSetVariableBool : OverSetVariable
    {
        [Input("Bool")] public bool newVariable;

        protected bool _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Bool;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            bool newVariable = GetInputValue("Bool", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].boolValue = newVariable;
                _value = localVariableDict[_id].boolValue;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].boolValue = newVariable;
                    _value = globalVariableDict[_id].boolValue;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(string), Multiple = true)]
    public class OverSetVariableString : OverSetVariable
    {
        [Input("String")] public string newVariable;

        protected string _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.String;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            string newVariable = GetInputValue("String", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].stringValue = newVariable;
                _value = localVariableDict[_id].stringValue;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].stringValue = newVariable;
                    _value = globalVariableDict[_id].stringValue;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Vector2), Multiple = true)]
    public class OverSetVariableVector2 : OverSetVariable
    {
        [Input("Vector 2D")] public Vector2 newVariable;

        protected Vector2 _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Vector2;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Vector2 newVariable = GetInputValue("Vector 2D", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].vector2Value = newVariable;
                _value = localVariableDict[_id].vector2Value;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].vector2Value = newVariable;
                    _value = globalVariableDict[_id].vector2Value;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Vector3), Multiple = true)]
    public class OverSetVariableVector3 : OverSetVariable
    {
        [Input("Vector 3D")] public Vector3 newVariable;

        protected Vector3 _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Vector3;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Vector3 newVariable = GetInputValue("Vector 3D", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].vector3Value = newVariable;
                _value = localVariableDict[_id].vector3Value;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].vector3Value = newVariable;
                    _value = globalVariableDict[_id].vector3Value;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Quaternion), Multiple = true)]
    public class OverSetVariableQuaternion : OverSetVariable
    {
        [Input("Quaternion")] public Quaternion newVariable;
         
        protected Quaternion _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Quaternion;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Quaternion newVariable = GetInputValue("Quaternion", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].QuaternionValue = newVariable;
                _value = localVariableDict[_id].QuaternionValue;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].QuaternionValue = newVariable;
                    _value = globalVariableDict[_id].QuaternionValue;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Transform), Multiple = true)]
    public class OverSetVariableTransform : OverSetVariable
    {
        [Input("Transform")] public Transform newVariable;
         
        protected Transform _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Transform;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Transform newVariable = GetInputValue("Transform", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].transformValue = newVariable;
                _value = localVariableDict[_id].transformValue;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].transformValue = newVariable;
                    _value = globalVariableDict[_id].transformValue;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(RectTransform), Multiple = true)]
    public class OverSetVariableRectTransform : OverSetVariable
    {
        [Input("RectTransform")] public RectTransform newVariable;
         
        protected RectTransform _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.RectTransform;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RectTransform newVariable = GetInputValue("RectTransform", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].rectTransform = newVariable;
                _value = localVariableDict[_id].rectTransform;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].rectTransform = newVariable;
                    _value = globalVariableDict[_id].rectTransform;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Rigidbody), Multiple = true)]
    public class OverSetVariableRigidbody : OverSetVariable
    {
        [Input("Rigidbody")] public Rigidbody newVariable;
         
        protected Rigidbody _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Rigidbody;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody newVariable = GetInputValue("Rigidbody", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].rigidbodyValue = newVariable;
                _value = localVariableDict[_id].rigidbodyValue;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].rigidbodyValue = newVariable;
                    _value = globalVariableDict[_id].rigidbodyValue;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(GameObject), Multiple = true)]
    public class OverSetVariableGameObject : OverSetVariable
    {
        [Input("GameObject")] public GameObject newVariable;
         
        protected GameObject _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Object;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            GameObject newVariable = GetInputValue("GameObject", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].gameObject = newVariable;
                _value = localVariableDict[_id].gameObject;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].gameObject = newVariable;
                    _value = globalVariableDict[_id].gameObject;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Renderer), Multiple = true)]
    public class OverSetVariableRenderer : OverSetVariable
    {
        [Input("Renderer")] public Renderer newVariable;
         
        protected Renderer _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Renderer;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Renderer newVariable = GetInputValue("Renderer", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].renderer = newVariable;
                _value = localVariableDict[_id].renderer;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].renderer = newVariable;
                    _value = globalVariableDict[_id].renderer;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(LineRenderer), Multiple = true)]
    public class OverSetVariableLineRenderer : OverSetVariable
    {
        [Input("LineRenderer")] public LineRenderer newVariable;
         
        protected LineRenderer _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.LineRenderer;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            LineRenderer newVariable = GetInputValue("LineRenderer", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].lineRenderer = newVariable;
                _value = localVariableDict[_id].lineRenderer;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].lineRenderer = newVariable;
                    _value = globalVariableDict[_id].lineRenderer;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(AudioSource), Multiple = true)]
    public class OverSetVariableAudioSource : OverSetVariable
    {
        [Input("AudioSource")] public AudioSource newVariable;
         
        protected AudioSource _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Audio;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioSource newVariable = GetInputValue("AudioSource", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].audioSource = newVariable;
                _value = localVariableDict[_id].audioSource;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].audioSource = newVariable;
                    _value = globalVariableDict[_id].audioSource;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(VideoPlayer), Multiple = true)]
    public class OverSetVariableVideoPlayer : OverSetVariable
    {
        [Input("VideoPlayer")] public VideoPlayer newVariable;
         
        protected VideoPlayer _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Video;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            VideoPlayer newVariable = GetInputValue("VideoPlayer", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].videoPlayer = newVariable;
                _value = localVariableDict[_id].videoPlayer;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].videoPlayer = newVariable;
                    _value = globalVariableDict[_id].videoPlayer;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Animator), Multiple = true)]
    public class OverSetVariableAnimator : OverSetVariable
    {
        [Input("Animator")] public Animator newVariable;
         
        protected Animator _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Animator;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Animator newVariable = GetInputValue("Animator", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].animator = newVariable;
                _value = localVariableDict[_id].animator;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].animator = newVariable;
                    _value = globalVariableDict[_id].animator;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }


    [Tags("Variable")]
    [Output("Value", Type = typeof(Light), Multiple = true)]
    public class OverSetVariableLight : OverSetVariable
    {
        [Input("Light")] public Light newVariable;
         
        protected Light _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Light;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Light newVariable = GetInputValue("Light", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].light = newVariable;
                _value = localVariableDict[_id].light;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].light = newVariable;
                    _value = globalVariableDict[_id].light;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    //[Tags("Variable")]
    //[Output("Value", Type = typeof(Button), Multiple = true)]
    //public class OverSetVariableButton : OverSetVariable
    //{
    //    [Input("Button")] public Button newVariable;
    //     
    //    protected Button _value;
    //    public override object Value => _value;
    //    public override OverVariableType Type => OverVariableType.Button;

    //    public override IExecutableOverNode Execute(OverExecutionFlowData data)
    //    {
    //        Button newVariable = GetInputValue("Button", this.newVariable);

    //        OverGraph overGraph = Graph as OverGraph;

    //        Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
    //        Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

    //        if (localVariableDict.ContainsKey(_id))
    //        {
    //            localVariableDict[_id].button = newVariable;
    //            _value = localVariableDict[_id].button;
    //        }
    //        else
    //        {
    //            if (globalVariableDict.ContainsKey(_id))
    //            {
    //                globalVariableDict[_id].button = newVariable;
    //                _value = globalVariableDict[_id].button;
    //            }
    //        }

    //        return base.Execute(data);
    //    }

    //    public override object OnRequestValue(Port port)
    //    {
    //        if (port.Name == "Value")
    //        {
    //            return _value;
    //        }

    //        return base.OnRequestValue(port);
    //    }
    //}

    [Tags("Variable")]
    [Output("Value", Type = typeof(Text), Multiple = true)]
    public class OverSetVariableText : OverSetVariable
    {
        [Input("Text")] public Text newVariable;
         
        protected Text _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Text;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Text newVariable = GetInputValue("Text", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].text = newVariable;
                _value = localVariableDict[_id].text;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].text = newVariable;
                    _value = globalVariableDict[_id].text;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(TMPro.TextMeshProUGUI), Multiple = true)]
    public class OverSetVariableTextTMP : OverSetVariable
    {
        [Input("Text (TMP)")] public TMPro.TextMeshProUGUI newVariable;
         
        protected TMPro.TextMeshProUGUI _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.TextTMP;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            TMPro.TextMeshProUGUI newVariable = GetInputValue("Text (TMP)", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].textTMP = newVariable;
                _value = localVariableDict[_id].textTMP;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].textTMP = newVariable;
                    _value = globalVariableDict[_id].textTMP;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Image), Multiple = true)]
    public class OverSetVariableImage : OverSetVariable
    {
        [Input("Image")] public Image newVariable;
         
        protected Image _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Image;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Image newVariable = GetInputValue("Image", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].image = newVariable;
                _value = localVariableDict[_id].image;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].image = newVariable;
                    _value = globalVariableDict[_id].image;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(RawImage), Multiple = true)]
    public class OverSetVariableRawImage : OverSetVariable
    {
        [Input("RawImage")] public RawImage newVariable;
         
        protected RawImage _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.RawImage;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RawImage newVariable = GetInputValue("RawImage", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].rawImage = newVariable;
                _value = localVariableDict[_id].rawImage;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].rawImage = newVariable;
                    _value = globalVariableDict[_id].rawImage;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Color), Multiple = true)]
    public class OverSetVariableColor : OverSetVariable
    {
        [Input("Color")] public Color newVariable;
         
        protected Color _value;
        public override OverVariableType Type => OverVariableType.Color;

        public override object Value => _value;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Color newVariable = GetInputValue("Color", this.newVariable);

            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                localVariableDict[_id].color = newVariable;
                _value = localVariableDict[_id].color;
            }
            else
            {
                if (globalVariableDict.ContainsKey(_id))
                {
                    globalVariableDict[_id].color = newVariable;
                    _value = globalVariableDict[_id].color;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Value")
            {
                return _value;
            }

            return base.OnRequestValue(port);
        }
    }
}