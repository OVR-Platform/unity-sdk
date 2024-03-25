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
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OverSDK.VisualScripting
{
    [Node(Path = "Component/Object", Name = "Object Exposer", Icon = "COMPONENT/OBJECT")]
    [Tags("Component")]
    [Output("Ref", typeof(GameObject), Multiple = true)]
    public class OverObject : OverNode
    {
        [Input("Object", Multiple = false)] public GameObject obj;

        [Output("Is Active", Multiple = true)] public bool active;
        [Output("Is Active in Scene", Multiple = true)] public bool activeInScene;

        [Output("Name", Multiple = true)] public string objectName;
        [Output("Transform", Multiple = true)] public Transform objectTransform;

        public override object OnRequestNodeValue(Port port)
        {
            GameObject _obj = GetInputValue("Object", obj);

            switch (port.Name)
            {
                case "Ref":
                    return _obj;
                case "Transform":
                    objectTransform = _obj.transform;
                    return objectTransform;
                case "Name":
                    objectName = _obj.name;
                    return objectName;
                case "Is Active":
                    active = _obj.activeSelf;
                    return active;
                case "Is Active in Scene":
                    activeInScene = _obj.activeInHierarchy;
                    return activeInScene;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverObjectHandlerNode : OverExecutionFlowNode { }

    [Node(Path = "Component/Object/Handlers", Name = "Set Active", Icon = "COMPONENT/OBJECT")]
    [Output("Updated Component", typeof(GameObject), Multiple = true)]
    public class OverSetActiveObject : OverObjectHandlerNode
    {
        [Input("Object")] public GameObject obj;
        [Input("Active")] public bool active;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            GameObject _obj = GetInputValue("Object", obj);
            bool _active = GetInputValue("Active", active);

            _obj.SetActive(_active);

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            GameObject _obj = GetInputValue("Object", obj);

            switch (port.Name)
            {
                case "Updated Component":
                    return _obj;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Object/Handlers", Name = "Create", Icon = "COMPONENT/OBJECT")]
    public class OverInstantiateObject : OverObjectHandlerNode
    {
        [Input("Prefab")] public GameObject prefab;
        [Input("Parent")] public Transform parent = null;

        [Output("Instance")] public GameObject newObject;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            GameObject prf = GetInputValue("Prefab", prefab);
            Transform trsf = GetInputValue("Parent", parent);
            if (trsf == null)
            {
                trsf = GameObject.FindObjectOfType<OvrAsset>().transform;
            }

            newObject = GameObject.Instantiate(prf, trsf);
            newObject.layer = trsf.gameObject.layer;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Instance":
                    return newObject;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Object/Handlers", Name = "Destroy", Icon = "COMPONENT/OBJECT")]
    public class OverDestroyObject : OverObjectHandlerNode
    {
        [Input("Object")] public GameObject obj;
        [Input("T")] public float t;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            GameObject prf = GetInputValue("Object", obj);
            float _t = GetInputValue("T", t);

            GameObject.Destroy(prf, _t);

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Object/Handlers", Name = "Get Component", Icon = "COMPONENT/OBJECT")]
    public class OverObjectGetComponent : OverObjectHandlerNode
    {
        [Input("Object")] public GameObject obj;

        [Output("Component", Multiple = true)] public Component component;

        [Editable("Type")] public OverComponentType type;

        public override void OnAddedToGraph()
        {
            base.OnAddedToGraph();
            ResolveOutputPortType();
        }

        public override void OnValidate()
        {
            base.OnValidate();

            ResolveOutputPortType();
        }

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            GameObject prf = GetInputValue("Object", obj);

            switch (type)
            {
                case OverComponentType.None: component = null; break;
                case OverComponentType.Transform: component = prf.GetComponent<Transform>(); break;
                case OverComponentType.Renderer: component = prf.GetComponent<Renderer>(); break;
                case OverComponentType.RectTransform: component = prf.GetComponent<RectTransform>(); break;
                case OverComponentType.LineRenderer: component = prf.GetComponent<LineRenderer>(); break;
                case OverComponentType.ParticleSystem: component = prf.GetComponent<ParticleSystem>(); break;
                case OverComponentType.Rigidbody: component = prf.GetComponent<Rigidbody>(); break;
                case OverComponentType.Collider: component = prf.GetComponent<Collider>(); break;
                case OverComponentType.Light: component = prf.GetComponent<Light>(); break;
                case OverComponentType.AudioSource: component = prf.GetComponent<AudioSource>(); break;
                case OverComponentType.Video: component = prf.GetComponent<VideoPlayer>(); break;
                case OverComponentType.Animator: component = prf.GetComponent<Animator>(); break;
                case OverComponentType.Text: component = prf.GetComponent<Text>(); break;
                case OverComponentType.TextTMP: component = prf.GetComponent<TextMeshProUGUI>(); break;
                case OverComponentType.Image: component = prf.GetComponent<Image>(); break;
                case OverComponentType.RawImage: component = prf.GetComponent<RawImage>(); break;
            }

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            if (port.Name == "Component")
            {
                switch (type)
                {
                    case OverComponentType.None: return null;
                    case OverComponentType.Transform: return (Transform)component;
                    case OverComponentType.Renderer: return (Renderer)component;
                    case OverComponentType.RectTransform: return (RectTransform)component;
                    case OverComponentType.LineRenderer: return (LineRenderer)component;
                    case OverComponentType.ParticleSystem: return (ParticleSystem)component;
                    case OverComponentType.Rigidbody: return (Rigidbody)component;
                    case OverComponentType.Collider: return (Collider)component;
                    case OverComponentType.Light: return (Light)component;
                    case OverComponentType.AudioSource: return (AudioSource)component;
                    case OverComponentType.Video: return (Transform)component;
                    case OverComponentType.Animator: return (Animator)component;
                    case OverComponentType.Text: return (Text)component;
                    case OverComponentType.TextTMP: return (TextMeshProUGUI)component;
                    case OverComponentType.Image: return (Image)component;
                    case OverComponentType.RawImage: return (RawImage)component;
                }
            }

            return base.OnRequestNodeValue(port);
        }

        //

        private void ResolveOutputPortType()
        {
            Port output = GetPort("Component");

            switch (type)
            {
                case OverComponentType.None: break;
                case OverComponentType.Transform: output.Type = typeof(Transform); break;
                case OverComponentType.Renderer: output.Type = typeof(Renderer); break;
                case OverComponentType.RectTransform: output.Type = typeof(RectTransform); break;
                case OverComponentType.LineRenderer: output.Type = typeof(LineRenderer); break;
                case OverComponentType.ParticleSystem: output.Type = typeof(ParticleSystem); break;
                case OverComponentType.Rigidbody: output.Type = typeof(Rigidbody); break;
                case OverComponentType.Collider: output.Type = typeof(Collider); break;
                case OverComponentType.Light: output.Type = typeof(Light); break;
                case OverComponentType.AudioSource: output.Type = typeof(AudioSource); break;
                case OverComponentType.Video: output.Type = typeof(VideoPlayer); break;
                case OverComponentType.Animator: output.Type = typeof(Animator); break;
                case OverComponentType.Text: output.Type = typeof(Text); break;
                case OverComponentType.TextTMP: output.Type = typeof(TextMeshProUGUI); break;
                case OverComponentType.Image: output.Type = typeof(Image); break;
                case OverComponentType.RawImage: output.Type = typeof(RawImage); break;
            }

            //todo: forzare un refresh della porta per le connessioni

        }
    }

    public enum OverComponentType
    {
        None,

        Transform,
        Renderer,
        RectTransform,
        LineRenderer,
        ParticleSystem,

        Rigidbody,
        Collider,

        Light,

        AudioSource,
        Video,
        Animator,

        Text,
        TextTMP,
        Image,
        RawImage
    }


    public enum OverCompareObjectNameType { Equal, NotEqual }

    [Node(Path = "Component/Object/Handlers", Name = "Compare Name", Icon = "COMPONENT/OBJECT")]
    public class OverCompareObjectName : OverObjectHandlerNode
    {
        [Input("Object A")] public GameObject obj;
        [Input("Name")] public string objectName;

        [Editable("Mode")] public OverCompareObjectNameType type;

        [Output("Result")] public bool t;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            GameObject prf = GetInputValue("Object A", obj);
            string _name = GetInputValue("Name", objectName);

            switch (type)
            {
                case OverCompareObjectNameType.Equal: t = prf.name == _name; break;
                case OverCompareObjectNameType.NotEqual: t = prf.name != _name; break;
            }

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Result":
                    return t;
            }

            return base.OnRequestNodeValue(port);
        }

    }
}