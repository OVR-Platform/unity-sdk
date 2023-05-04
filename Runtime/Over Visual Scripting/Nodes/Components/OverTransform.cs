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
    [Node(Path = "Component/Transform", Name = "Transform Exposer", Icon = "COMPONENT/TRANSFORM")]
    [Tags("Component")]
    [Output("Ref", typeof(Transform), Multiple = true)]
    public class OverTransform : OverNode
    {
        [Input("Transform", Multiple = false)] public Transform transform;

        [Output("Position", Multiple = true)] public Vector3 posn;
        [Output("Rotation", Multiple = true)] public Quaternion rot;
        [Output("Scale", Multiple = true)] public Vector3 scale;

        [Output("Local Position", Multiple = true)] public Vector3 localPosition;
        [Output("Local Rotation", Multiple = true)] public Quaternion localRotation;
        [Output("Lossy Scale", Multiple = true)] public Vector3 lossyScale;

        [Output("Forward", Multiple = true)] public Vector3 forward;
        [Output("Right", Multiple = true)] public Vector3 right;
        [Output("Up", Multiple = true)] public Vector3 up;

        public override object OnRequestValue(Port port)
        {
            Transform _transform = GetInputValue("Transform", transform);

            switch (port.Name)
            {
                case "Ref":
                    return _transform;
                case "Position":
                    posn = _transform.position;
                    return posn;
                case "Rotation":
                    rot = _transform.rotation;
                    return rot;
                case "Scale":
                    scale = _transform.localScale;
                    return scale;
                case "Local Position":
                    localPosition = _transform.localPosition;
                    return localPosition;
                case "Local Rotation":
                    localRotation = _transform.localRotation;
                    return localRotation;
                case "Lossy Scale":
                    lossyScale = _transform.lossyScale;
                    return lossyScale;
                case "Forward":
                    forward = _transform.forward;
                    return forward;
                case "Right":
                    right = _transform.right;
                    return right;
                case "Up":
                    up = _transform.up;
                    return up;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverTransformHandlerNode : OverExecutionFlowNode { }

    //setters
    [Node(Path = "Component/Transform/Handlers", Name = "Set Position", Icon = "COMPONENT/TRANSFORM")]
    [Output("Updated Component", typeof(Transform), Multiple = true)]
    public class OverSetTransformPosition : OverTransformHandlerNode
    {
        [Input("Transform")] public Transform target;
        [Input("Position")] public Vector3 posn;

        [Editable("Space")] public Space space;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Transform _target = GetInputValue("Transform", target);
            Vector3 _position = GetInputValue("Position", posn);

            switch (space)
            {
                case Space.World: _target.position = _position; break;
                case Space.Self: _target.localPosition = _position; break;
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Transform _rectTransform = GetInputValue("Transform", target);
            switch (port.Name)
            {
                case "Updated Component": return _rectTransform;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Transform/Handlers", Name = "Set Rotation", Icon = "COMPONENT/TRANSFORM")]
    [Output("Updated Component", typeof(Transform), Multiple = true)]
    public class OverSetTransformRotation : OverTransformHandlerNode
    {
        [Input("Transform")] public Transform target;
        [Input("Rotation")] public Quaternion rot;

        [Editable("Space")] public Space space;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Transform _target = GetInputValue("Transform", target);
            Quaternion _rotation = GetInputValue("Rotation", rot);

            switch (space)
            {
                case Space.World: _target.rotation = _rotation; break;
                case Space.Self: _target.localRotation = _rotation; break;
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Transform _rectTransform = GetInputValue("Transform", target);
            switch (port.Name)
            {
                case "Updated Component": return _rectTransform;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Transform/Handlers", Name = "Set Scale", Icon = "COMPONENT/TRANSFORM")]
    [Output("Updated Component", typeof(Transform), Multiple = true)]
    public class OverSetTransformScale : OverTransformHandlerNode
    {
        [Input("Transform")] public Transform target;
        [Input("Scale")] public Vector3 scale;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Transform _target = GetInputValue("Transform", target);
            Vector3 _scale = GetInputValue("Scale", scale);

            _target.localScale = _scale;

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Transform _rectTransform = GetInputValue("Transform", target);
            switch (port.Name)
            {
                case "Updated Component": return _rectTransform;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Transform/Handlers", Name = "Set Parent", Icon = "COMPONENT/TRANSFORM")]
    [Output("Updated Component", typeof(Transform), Multiple = true)]
    public class OverSetTransformParent : OverTransformHandlerNode
    {
        [Input("Transform")] public Transform target;
        [Input("Parent")] public Transform parent;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Transform _target = GetInputValue("Transform", target);
            Transform _parent = GetInputValue("Parent", parent);

            _target.SetParent(_parent);

            return base.Execute(data);
        }
        public override object OnRequestValue(Port port)
        {
            Transform _rectTransform = GetInputValue("Transform", target);
            switch (port.Name)
            {
                case "Updated Component": return _rectTransform;
            }

            return base.OnRequestValue(port);
        }

    }

    [Node(Path = "Component/Transform/Handlers", Name = "Look At", Icon = "COMPONENT/TRANSFORM")]
    public class OverLookAt : OverTransformHandlerNode
    {
        [Input("Transform")] public Transform transform;

        [Input("Target")] public object target;

        [Input("Up")] public Vector3 up = Vector3.up;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Transform _transform = GetInputValue("Transform", transform);
            Vector3 _up = GetInputValue("Up", up);

            object _target = GetInputValue("Target", target);
            if (_target.GetType().IsAssignableFrom(typeof(Vector3)))
            {
                _transform.LookAt((Vector3)_target, _up.normalized);
            }

            if (_target.GetType().IsAssignableFrom(typeof(Transform)))
            {
                _transform.LookAt((Transform)_target, _up.normalized);
            }

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Transform/Handlers", Name = "Translate", Icon = "COMPONENT/TRANSFORM")]
    public class OverTranslate : OverTransformHandlerNode
    {
        [Input("Transform")] public Transform transform;

        [Input("Translation")] public Vector3 translation;
        [Editable("Space")] public Space space;
        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Transform _transform = GetInputValue("Transform", transform);
            Vector3 _translation = GetInputValue("Translation", translation);

            _transform.Translate(_translation, space);
            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Transform/Handlers", Name = "Rotate", Icon = "COMPONENT/TRANSFORM")]
    public class OverRotate : OverTransformHandlerNode
    {
        [Input("Transform")] public Transform transform;

        [Input("Axis"), Range(0, 1)] public Vector3 axis = Vector3.up;
        [Input("Angle")] public float angle;

        [Editable("Space")] public Space space;
        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Transform _transform = GetInputValue("Transform", transform);
            Vector3 _axis = GetInputValue("Axis", axis);
            float _angle = GetInputValue("Angle", angle);

            _transform.Rotate(_axis, _angle, space);
            return base.Execute(data);
        }
    }
}