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

        public int sublistIndex;

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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.integerValue = newVariable;
                _value = varaibleFound.integerValue;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.floatValue = newVariable;
                _value = varaibleFound.floatValue;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.boolValue = newVariable;
                _value = varaibleFound.boolValue;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.stringValue = newVariable;
                _value = varaibleFound.stringValue;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.vector2Value = newVariable;
                _value = varaibleFound.vector2Value;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.vector3Value = newVariable;
                _value = varaibleFound.vector3Value;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.QuaternionValue = newVariable;
                _value = varaibleFound.QuaternionValue;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.transformValue = newVariable;
                _value = varaibleFound.transformValue;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.rectTransform = newVariable;
                _value = varaibleFound.rectTransform;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.rigidbodyValue = newVariable;
                _value = varaibleFound.rigidbodyValue;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.colliderValue = newVariable;
                _value = varaibleFound.colliderValue;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.characterController = newVariable;
                _value = varaibleFound.characterController;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.gameObject = newVariable;
                _value = varaibleFound.gameObject;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.renderer = newVariable;
                _value = varaibleFound.renderer;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.lineRenderer = newVariable;
                _value = varaibleFound.lineRenderer;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.material = newVariable;
                _value = varaibleFound.material;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.particleSystem = newVariable;
                _value = varaibleFound.particleSystem;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.audioSource = newVariable;
                _value = varaibleFound.audioSource;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.audioClip = newVariable;
                _value = varaibleFound.audioClip;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.videoPlayer = newVariable;
                _value = varaibleFound.videoPlayer;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.imageStreamer = newVariable;
                _value = varaibleFound.imageStreamer;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.animator = newVariable;
                _value = varaibleFound.animator;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.light = newVariable;
                _value = varaibleFound.light;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.navMeshAgent = newVariable;
                _value = varaibleFound.navMeshAgent;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.text = newVariable;
                _value = varaibleFound.text;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.textTMP = newVariable;
                _value = varaibleFound.textTMP;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.textTMP_3D = newVariable;
                _value = varaibleFound.textTMP_3D;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.image = newVariable;
                _value = varaibleFound.image;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.rawImage = newVariable;
                _value = varaibleFound.rawImage;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.color = newVariable;
                _value = varaibleFound.color;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.list.Elements = new System.Collections.Generic.List<OverListData>();
                foreach (var item in newVariable)
                {
                    OverListData d = new OverListData();
                    d.type = (OverListDataType)varaibleFound.list.Type;
                    d.SetValue(item);
                    varaibleFound.list.Elements.Add(d);
                }


                IList result = OverList.GetList(OverList.ResolveType(varaibleFound.list));
                foreach (var element in varaibleFound.list.Elements)
                {
                    var item = element.GetValue((OverListDataType)varaibleFound.list.Type);
                    if (item != null)
                    {
                        result.Add(item);
                    }
                }
                _value = result;
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
        [Input("List")] public OverList newVariable;

        protected OverList _value;
        public override object Value => _value;
        public override OverVariableType Type => OverVariableType.List;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            OverList newVariable = GetInputValue("OverList", this.newVariable);

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                varaibleFound.list = newVariable;
                _value = varaibleFound.list;
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

            OverScriptData scriptData = isGlobal ?
                           OverScriptManager.Main.Data :
                           OverScriptManager.Main.overScriptsReferences[data.scritpGUID].overScript.Data;

            if (scriptData.TryGetVariable(guid, out OverVariableData variableFound))
            {
                variableFound.json = newVariable.ToString();
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