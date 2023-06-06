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
    public enum AnchorType { Position, Min, Max }

    [Node(Path = "Component/UI/RectTransform", Name = "RectTransform Exposer", Icon = "COMPONENT/TRANSFORM")]
    [Tags("Component")]
    [Output("Ref", typeof(RectTransform), Multiple = true)]
    public class OverRectTransform : OverNode
    {
        [Input("RectTransform", Multiple = false)] public RectTransform rectTransform;

        [Output("Anchor Position", Multiple = true)] public Vector2 anchorPosition;
        [Output("Anchor Min", Multiple = true)] public Vector2 anchorMin;
        [Output("Anchor Max", Multiple = true)] public Vector2 anchorMax;
        [Output("Pivot", Multiple = true)] public Vector2 pivot;
        [Output("Size", Multiple = true)] public Vector2 sizeDelta;

        [Output("Position", Multiple = true)] public Vector3 posn;
        [Output("Rotation", Multiple = true)] public Quaternion rot;
        [Output("Scale", Multiple = true)] public Vector3 scale;

        [Output("Local Position", Multiple = true)] public Vector3 localPosition;
        [Output("Local Rotation", Multiple = true)] public Quaternion localRotation;
        [Output("Lossy Scale", Multiple = true)] public Vector3 lossyScale;

        public override object OnRequestNodeValue(Port port)
        {
            RectTransform _rectTransform = GetInputValue("RectTransform", rectTransform);
            switch (port.Name)
            {
                case "Ref": return _rectTransform;
                case "Anchor Position": anchorPosition = _rectTransform.anchoredPosition; return anchorPosition;
                case "Anchor Min": anchorMin = _rectTransform.anchorMin; return anchorMin;
                case "Anchor Max": anchorMax = _rectTransform.anchorMax; return anchorMax;
                case "Size": sizeDelta = _rectTransform.sizeDelta; return sizeDelta;
                case "Pivot": pivot = _rectTransform.pivot; return pivot;
                case "Position": posn = _rectTransform.position; return posn;
                case "Rotation": rot = _rectTransform.rotation; return rot;
                case "Scale": scale = _rectTransform.localScale; return scale;
                case "Local Position": localPosition = _rectTransform.localPosition; return localPosition;
                case "Local Rotation": localRotation = _rectTransform.localRotation; return localRotation;
                case "Lossy Scale": lossyScale = _rectTransform.lossyScale; return lossyScale;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    //setters
    [Node(Path = "Component/UI/RectTransform/Handlers", Name = "Set Rect Position", Icon = "COMPONENT/TRANSFORM")]
    [Output("Updated Component", typeof(RectTransform), Multiple = true)]
    public class OverSetRectTransformPosition : OverTransformHandlerNode
    {
        [Input("RectTransform")] public RectTransform target;
        [Input("Position")] public Vector3 posn;

        [Editable("Space")] public Space space;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RectTransform _target = GetInputValue("RectTransform", target);
            Vector3 _position = GetInputValue("Position", posn);

            switch (space)
            {
                case Space.World: _target.position = _position; break;
                case Space.Self: _target.localPosition = _position; break;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            RectTransform _rectTransform = GetInputValue("RectTransform", target);
            switch (port.Name)
            {
                case "Updated Component": return _rectTransform;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/UI/RectTransform/Handlers", Name = "Set Rect Rotation", Icon = "COMPONENT/TRANSFORM")]
    [Output("Updated Component", typeof(RectTransform), Multiple = true)]
    public class OverSetRectTransformRotation : OverTransformHandlerNode
    {
        [Input("RectTransform")] public RectTransform target;
        [Input("Rotation")] public Quaternion rot;

        [Editable("Space")] public Space space;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RectTransform _target = GetInputValue("RectTransform", target);
            Quaternion _rotation = GetInputValue("Rotation", rot);

            switch (space)
            {
                case Space.World: _target.rotation = _rotation; break;
                case Space.Self: _target.localRotation = _rotation; break;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            RectTransform _rectTransform = GetInputValue("RectTransform", target);
            switch (port.Name)
            {
                case "Updated Component": return _rectTransform;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/UI/RectTransform/Handlers", Name = "Set Rect Scale", Icon = "COMPONENT/TRANSFORM")]
    [Output("Updated Component", typeof(RectTransform), Multiple = true)]
    public class OverSetRectTransformScale : OverTransformHandlerNode
    {
        [Input("RectTransform")] public RectTransform target;
        [Input("Scale")] public Vector3 scale;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RectTransform _target = GetInputValue("RectTransform", target);
            Vector3 _scale = GetInputValue("Scale", scale);

            _target.localScale = _scale;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            RectTransform _rectTransform = GetInputValue("RectTransform", target);
            switch (port.Name)
            {
                case "Updated Component": return _rectTransform;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/UI/RectTransform/Handlers", Name = "Set Rect Anchor", Icon = "COMPONENT/TRANSFORM")]
    [Output("Updated Component", typeof(RectTransform), Multiple = true)]
    public class OverSetRectTransformAnchor : OverTransformHandlerNode
    {
        [Input("RectTransform")] public RectTransform target;
        [Input("Anchor")] public Vector2 anchor;

        [Editable("Anchor Type")] public AnchorType anchorType;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RectTransform _target = GetInputValue("RectTransform", target);
            Vector2 _anchor = GetInputValue("Anchor", anchor);

            switch (anchorType)
            {
                case AnchorType.Position: _target.anchoredPosition = _anchor; break;
                case AnchorType.Min: _target.anchorMin = _anchor; break;
                case AnchorType.Max: _target.anchorMax = _anchor; break;
            }
            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            RectTransform _rectTransform = GetInputValue("RectTransform", target);
            switch (port.Name)
            {
                case "Updated Component": return _rectTransform;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/UI/RectTransform/Handlers", Name = "Set Rect Pivot", Icon = "COMPONENT/TRANSFORM")]
    [Output("Updated Component", typeof(RectTransform), Multiple = true)]
    public class OverSetRectTransformPivot : OverTransformHandlerNode
    {
        [Input("RectTransform")] public RectTransform target;
        [Input("Pivot")] public Vector2 pivot;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RectTransform _target = GetInputValue("RectTransform", target);
            Vector2 _pivot = GetInputValue("Pivot", pivot);

            _target.pivot = _pivot;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            RectTransform _rectTransform = GetInputValue("RectTransform", target);
            switch (port.Name)
            {
                case "Updated Component": return _rectTransform;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/UI/RectTransform/Handlers", Name = "Set Rect Size", Icon = "COMPONENT/TRANSFORM")]
    [Output("Updated Component", typeof(RectTransform), Multiple = true)]
    public class OverSetRectTransformSize : OverTransformHandlerNode
    {
        [Input("RectTransform")] public RectTransform target;
        [Input("Size")] public Vector2 sizeDelta;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RectTransform _sizeDelta = GetInputValue("RectTransform", target);
            Vector2 _pivot = GetInputValue("Size", sizeDelta);

            _sizeDelta.sizeDelta = _pivot;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            RectTransform _rectTransform = GetInputValue("RectTransform", target);
            switch (port.Name)
            {
                case "Updated Component": return _rectTransform;
            }

            return base.OnRequestNodeValue(port);
        }
    }
}