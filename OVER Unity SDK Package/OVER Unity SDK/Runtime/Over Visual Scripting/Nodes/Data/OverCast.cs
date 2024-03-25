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

namespace OverSDK.VisualScripting
{
    [Node(Path = "Utils", Name = "Float to Int", Icon = "DATA/SIMPLE")]
    [Tags("Utils")]
    [Output("Value", typeof(int), Multiple = true)]
    public class OverFloat2Int : OverNode
    {
        [Input("a")] public float a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            return (int)_a;
        }
    }

    [Node(Path = "Utils", Name = "Int to Float", Icon = "DATA/SIMPLE")]
    [Tags("Utils")]
    [Output("Value", typeof(float), Multiple = true)]
    public class OverInt2Float : OverNode
    {
        [Input("a")] public int a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            return (float)_a;
        }
    }

    [Node(Path = "Utils", Name = "Bool to Int", Icon = "DATA/SIMPLE")]
    [Tags("Utils")]
    [Output("Value", typeof(int), Multiple = true)]
    public class OverBool2Int : OverNode
    {
        [Input("a")] public bool a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            return _a ? 1 : 0;
        }
    }

    [Node(Path = "Utils", Name = "Int to Bool", Icon = "DATA/SIMPLE")]
    [Tags("Utils")]
    [Output("Value", typeof(bool), Multiple = true)]
    public class OverInt2Bool : OverNode
    {
        [Input("a")] public int a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            return (_a != 0) ? true : false;
        }
    }


    [Node(Path = "Utils", Name = "To Boolean", Icon = "DATA/SIMPLE")]
    [Tags("Data")]
    [Output("Value", typeof(bool), Multiple = true)]
    public class OverToBool : OverNode
    {
        [Input("entity")] public object entity;

        public override object OnRequestNodeValue(Port port)
        {
            var _entity = GetInputValue("entity", entity);

            try
            {
                return (bool)_entity;
            }
            catch
            {
                Debug.LogError($"ATTENTION! Was unable to cast {entity} to a bool value. Returning false as default Value.");
                return false;
            }
        }
    }


    [Node(Path = "Utils", Name = "To Float", Icon = "DATA/SIMPLE")]
    [Tags("Data")]
    [Output("Value", typeof(float), Multiple = true)]
    public class OverToFloat : OverNode
    {
        [Input("entity")] public object entity;

        public override object OnRequestNodeValue(Port port)
        {
            var _entity = GetInputValue("entity", entity);

            try
            {
                return (float)_entity;
            }
            catch
            {
                Debug.LogError($"ATTENTION! Was unable to cast {entity} to a float value. Returning 0.0f as default Value.");
                return 0.0f;
            }
        }
    }


    [Node(Path = "Utils", Name = "To Int", Icon = "DATA/SIMPLE")]
    [Tags("Data")]
    [Output("Value", typeof(int), Multiple = true)]
    public class OverToInt : OverNode
    {
        [Input("entity")] public object entity;

        public override object OnRequestNodeValue(Port port)
        {
            var _entity = GetInputValue("entity", entity);

            try
            {
                return (int)_entity;
            }
            catch
            {
                Debug.LogError($"ATTENTION! Was unable to cast {entity} to a int value. Returning 0 as default Value.");
                return 0;
            }
        }
    }


    [Node(Path = "Utils", Name = "To Transform", Icon = "DATA/SIMPLE")]
    [Tags("Data")]
    [Output("Value", typeof(Transform), Multiple = true)]
    public class OverToTransform : OverNode
    {
        [Input("entity")] public object entity;

        public override object OnRequestNodeValue(Port port)
        {
            var _entity = GetInputValue("entity", entity);

            // Check if the object is a Component (Transform, BoxCollider, etc.)
            if (_entity is Component component)
            {
                return component.transform;
            }
            else if (_entity is GameObject entityObject)
            {
                return entityObject.transform;
            }

            try
            {
                return (Transform)_entity;
            }
            catch
            {
                Debug.LogError($"ATTENTION! Was unable to cast {_entity.GetType()} to a Transform value.");
                return 0;
            }
        }
    }

    [Node(Path = "Utils", Name = "To List", Icon = "DATA/SIMPLE")]
    [Tags("Data")]
    [Output("Value", typeof(IList), Multiple = true)]
    public class OverToList : OverNode
    {
        [Input("entity")] public object entity;

        public override object OnRequestNodeValue(Port port)
        {
            var _entity = GetInputValue("entity", entity);

            try
            {
                return (IList)_entity;
            }
            catch
            {
                Debug.LogError($"ATTENTION! Was unable to cast {entity} to a List value. Returning null as default Value.");
                return null;
            }
        }
    }

    [Node(Path = "Utils", Name = "To Collider", Icon = "DATA/SIMPLE")]
    [Tags("Data")]
    [Output("Value", typeof(Collider), Multiple = true)]
    public class OverToCollider : OverNode
    {
        [Input("entity")] public object entity;

        public override object OnRequestNodeValue(Port port)
        {
            var _entity = GetInputValue("entity", entity);

            // Check if the object is a Component (Transform, BoxCollider, etc.)
            if (_entity is Component component)
            {
                if (component.TryGetComponent(out Collider collider))
                {
                    return collider;
                }
            }

            try
            {
                return (Collider)_entity;
            }
            catch
            {
                Debug.LogError($"ATTENTION! Was unable to cast {entity} to a Collider value. Returning null as default Value.");
                return null;
            }
        }
    }

    [Node(Path = "Utils", Name = "To Rigidbody", Icon = "DATA/SIMPLE")]
    [Tags("Data")]
    [Output("Value", typeof(Rigidbody), Multiple = true)]
    public class OverToRigidbody : OverNode
    {
        [Input("entity")] public object entity;

        public override object OnRequestNodeValue(Port port)
        {
            var _entity = GetInputValue("entity", entity);

            // Check if the object is a Component
            if (_entity is Component component)
            {
                if (component.TryGetComponent(out Rigidbody rigidbody))
                {
                    return rigidbody;
                }
            }

            try
            {
                return (Rigidbody)_entity;
            }
            catch
            {
                Debug.LogError($"[Over] ATTENTION! Was unable to cast {entity} to a Rigidbody value. Returning null as default Value.");
                return null;
            }
        }
    }

    [Node(Path = "Utils", Name = "To GameObject", Icon = "DATA/SIMPLE")]
    [Tags("Data")]
    [Output("Value", typeof(GameObject), Multiple = true)]
    public class OverToGameObject : OverNode
    {
        [Input("entity")] public object entity;

        public override object OnRequestNodeValue(Port port)
        {
            var _entity = GetInputValue("entity", entity);

            // Check if the object is a Component (Transform, BoxCollider, etc.)
            if (_entity is Component component)
            {
                return component.gameObject;
            }

            try
            {
                return (GameObject)_entity;
            }
            catch
            {
                Debug.LogError($"ATTENTION! Was unable to cast {entity} to a GameObject value. Returning null as default Value.");
                return null;
            }
        }
    }
}




