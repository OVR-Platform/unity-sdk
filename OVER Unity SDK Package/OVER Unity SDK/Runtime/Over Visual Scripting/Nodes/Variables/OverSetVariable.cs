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
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using OverSDK;
using OverSimpleJSON;
using UnityEngine.AI;

namespace OverSDK.VisualScripting
{
    [Node]
    public abstract class OverSetVariable : OverExecutionFlowNode
    {
        public string guid;
        public string _name;

        public bool isGlobal;

        public abstract object Value { get; }
        public abstract OverVariableType Type { get; }
        public override void OnEnable()
        {
            Name = $"{_name}";
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
            int newVariable = (int)GetInputValue("Int", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.integerValue = newVariable;
                _value = variableData.integerValue;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? (int)_value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.floatValue = newVariable;
                _value = variableData.floatValue;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.boolValue = newVariable;
                _value = variableData.boolValue;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.stringValue = newVariable;
                _value = variableData.stringValue;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.vector2Value = newVariable;
                _value = variableData.vector2Value;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.vector3Value = newVariable;
                _value = variableData.vector3Value;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.QuaternionValue = newVariable;
                _value = variableData.QuaternionValue;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.transformValue = newVariable;
                _value = variableData.transformValue;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.rectTransform = newVariable;
                _value = variableData.rectTransform;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.rigidbodyValue = newVariable;
                _value = variableData.rigidbodyValue;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Collider), Multiple = true)]
    public class OverSetVariableCollider : OverSetVariable
    {
        [Input("Collider")] public Collider newVariable;

        protected Collider _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Collider;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Collider newVariable = GetInputValue("Collider", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.colliderValue = newVariable;
                _value = variableData.colliderValue;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(CharacterController), Multiple = true)]
    public class OverSetVariableCharacterController : OverSetVariable
    {
        [Input("CharacterController")] public CharacterController newVariable;

        protected CharacterController _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.CharacterController;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            CharacterController newVariable = GetInputValue("CharacterController", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.characterController = newVariable;
                _value = variableData.characterController;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.gameObject = newVariable;
                _value = variableData.gameObject;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.renderer = newVariable;
                _value = variableData.renderer;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.lineRenderer = newVariable;
                _value = variableData.lineRenderer;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(Material), Multiple = true)]
    public class OverSetVariableMaterial : OverSetVariable
    {
        [Input("Material")] public Material newVariable;

        protected Material _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.Material;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Material newVariable = GetInputValue("Material", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.material = newVariable;
                _value = variableData.material;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(ParticleSystem), Multiple = true)]
    public class OverSetVariableParticleSystem : OverSetVariable
    {
        [Input("ParticleSystem")] public ParticleSystem newVariable;

        protected ParticleSystem _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.ParticleSystem;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            ParticleSystem newVariable = GetInputValue("ParticleSystem", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.particleSystem = newVariable;
                _value = variableData.particleSystem;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(AudioSource), Multiple = true)]
    public class OverSetVariableAudioSource : OverSetVariable
    {
        [Input("AudioSource")] public AudioSource newVariable;
         
        protected AudioSource _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.AudioSource;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioSource newVariable = GetInputValue("AudioSource", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.audioSource = newVariable;
                _value = variableData.audioSource;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(AudioClip), Multiple = true)]
    public class OverSetVariableAudioClip : OverSetVariable
    {
        [Input("AudioClip")] public AudioClip newVariable;

        protected AudioClip _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.AudioClip;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioClip newVariable = GetInputValue("AudioClip", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.audioClip = newVariable;
                _value = variableData.audioClip;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.videoPlayer = newVariable;
                _value = variableData.videoPlayer;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(ImageStreamer), Multiple = true)]
    public class OverSetVariableImageStreamer : OverSetVariable
    {
        [Input("ImageStreamer")] public ImageStreamer newVariable;

        protected ImageStreamer _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.ImageStreamer;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            ImageStreamer newVariable = GetInputValue("ImageStreamer", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.imageStreamer = newVariable;
                _value = variableData.imageStreamer;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.animator = newVariable;
                _value = variableData.animator;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.light = newVariable;
                _value = variableData.light;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(NavMeshAgent), Multiple = true)]
    public class OverSetVariableNavMeshAgent : OverSetVariable
    {
        [Input("NavMeshAgent")] public NavMeshAgent newVariable;

        protected NavMeshAgent _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.NavMeshAgent;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            NavMeshAgent newVariable = GetInputValue("NavMeshAgent", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.navMeshAgent = newVariable;
                _value = variableData.navMeshAgent;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "NavMeshAgent" ? _value : base.OnRequestNodeValue(port);
        }
    }

    //[Tags("Variable")]
    //[Output("Value", Type = typeof(NavMeshObstacle), Multiple = true)]
    //public class OverSetVariableNavMeshObstacle : OverSetVariable
    //{
    //    [Input("NavMeshObstacle")] public NavMeshObstacle newVariable;

    //    protected NavMeshObstacle _value;
    //    public override object Value => _value;
    //    public override OverVariableType Type => OverVariableType.NavMeshAgent;

    //    public override IExecutableOverNode Execute(OverExecutionFlowData data)
    //    {
    //        NavMeshObstacle newVariable = GetInputValue("NavMeshObstacle", this.newVariable);

    //        var variableDict = isGlobal
    //            ? OverScriptManager.Main.Data.VariableDict
    //            : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

    //        if (variableDict.TryGetValue(guid, out var variableData))
    //        {
    //            variableData.navMeshObstacle = newVariable;
    //            _value = variableData.navMeshObstacle;
    //        }

    //        return base.Execute(data);
    //    }

    //    public override object OnRequestNodeValue(Port port)
    //    {
    //        return port.Name == "NavMeshObstacle" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.text = newVariable;
                _value = variableData.text;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(TMPro.TextMeshProUGUI), Multiple = true)]
    public class OverSetVariableTextTMP : OverSetVariable
    {
        [Input("Text (TMP UGUI)")] public TMPro.TextMeshProUGUI newVariable;
         
        protected TMPro.TextMeshProUGUI _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.TextTMP;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            TMPro.TextMeshProUGUI newVariable = GetInputValue("Text (TMP UGUI)", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.textTMP = newVariable;
                _value = variableData.textTMP;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(TMPro.TextMeshPro), Multiple = true)]
    public class OverSetVariableTextTMP_3D : OverSetVariable
    {
        [Input("Text (TMP)")] public TMPro.TextMeshPro newVariable;

        protected TMPro.TextMeshPro _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.TextTMP_3D;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            TMPro.TextMeshPro newVariable = GetInputValue("Text (TMP)", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.textTMP_3D = newVariable;
                _value = variableData.textTMP_3D;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.image = newVariable;
                _value = variableData.image;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.rawImage = newVariable;
                _value = variableData.rawImage;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
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

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.color = newVariable;
                _value = variableData.color;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(IList), Multiple = true)]
    public class OverSetVariableOverDataList : OverSetVariable
    {
        [Input("List")] public IList newVariable;

        protected IList _value;
        public override OverVariableType Type => OverVariableType.List;

        public override object Value => _value;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            IList newVariable = GetInputValue("List", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                //variableData.list.GenericElements = newVariable;

                variableData.list.Elements = new System.Collections.Generic.List<OverListData>();
                foreach( var item in newVariable)
                {
                    OverListData d = new OverListData();
                    d.type = (OverListDataType)variableData.list.Type;
                    d.SetValue(item);
                    variableData.list.Elements.Add(d);
                }


                IList result = OverList.GetList(OverList.ResolveType(variableData.list));
                foreach (var element in variableData.list.Elements)
                {
                    var item = element.GetValue((OverListDataType)variableData.list.Type);
                    if (item != null)
                    {
                        result.Add(item);
                    }
                }
                _value = result;

                //_value = variableData.list.GenericElements;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(OverList), Multiple = true)]
    public class OverSetVariableOverList : OverSetVariable
    {
        [Input("Image")] public OverList newVariable;

        protected OverList _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.List;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            OverList newVariable = GetInputValue("OverList", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.list = newVariable;
                _value = variableData.list;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }

    [Tags("Variable")]
    [Output("Value", Type = typeof(JSONNode), Multiple = true)]
    public class OverSetVariableJSON : OverSetVariable
    {
        [Input("JSON")] public JSONNode newVariable;

        protected JSONNode _value;
        public override OverVariableType Type => OverVariableType.JSON;

        public override object Value => _value;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            JSONNode newVariable = GetInputValue("JSON", this.newVariable);

            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[data.scritpGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                variableData.json = newVariable.ToString();
                _value = newVariable;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return port.Name == "Value" ? _value : base.OnRequestNodeValue(port);
        }
    }
}