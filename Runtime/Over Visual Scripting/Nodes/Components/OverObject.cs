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
using UnityEngine;

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

        [Output("Transform", Multiple = true)] public Transform transform;

        public override object OnRequestValue(Port port)
        {
            GameObject _obj = GetInputValue("Object", obj);

            switch (port.Name)
            {
                case "Ref":
                    return _obj;
                case "Transform":
                    transform = _obj.transform;
                    return transform;
                case "Is Active":
                    active = _obj.activeSelf;
                    return active;
                case "Is Active in Scene":
                    activeInScene = _obj.activeInHierarchy;
                    return activeInScene;
            }

            return base.OnRequestValue(port);
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

        public override object OnRequestValue(Port port)
        {
            GameObject _obj = GetInputValue("Object", obj);

            switch (port.Name)
            {
                case "Updated Component":
                    return _obj;
            }

            return base.OnRequestValue(port);
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
            if(parent == null)
            {
                parent = GameObject.FindObjectOfType<OvrAsset>().transform;
            }
            //read inputs
            GameObject prf = GetInputValue("Prefab", prefab);
            Transform trsf = GetInputValue("Parent", parent);

            newObject = GameObject.Instantiate(prf, trsf);
            newObject.layer = parent.gameObject.layer;

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            switch (port.Name)
            {
                case "Instance":
                    return newObject;
            }

            return base.OnRequestValue(port);
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
}