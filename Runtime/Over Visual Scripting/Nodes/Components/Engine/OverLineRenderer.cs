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
    [Node(Path = "Component/Engine/LineRenderer", Name = "LineRenderer Exposer", Icon = "COMPONENT/ENGINE/LINERENDER")]
    [Tags("Component")]
    [Output("Ref", typeof(LineRenderer), Multiple = true)]
    public class OverLineRenderer : OverNode
    {
        [Input("LineRenderer", Multiple = false)] public LineRenderer lineRenderer;

        [Output("Start Color", Multiple = true)] public Color startColor;
        [Output("End Color", Multiple = true)] public Color endColor;

        [Output("Start Width", Multiple = true)] public float startWidth;
        [Output("End Width", Multiple = true)] public float endWidth;

        [Output("Loop", Multiple = true)] public bool loop;
        [Output("Position Count", Multiple = true)] public int positionCount;

        [Output("Material", Multiple = true)] public Material material;
        [Output("Shared Material", Multiple = true)] public Material sharedMaterial;

        public override object OnRequestValue(Port port)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            switch (port.Name)
            {
                case "Ref": return _lineRenderer;
                case "Start Color": startColor = _lineRenderer.startColor; return startColor;
                case "End Color": endColor = _lineRenderer.endColor; return endColor;
                case "Start Width": startWidth = _lineRenderer.startWidth; return startWidth;
                case "End Width": endWidth = _lineRenderer.endWidth; return endWidth;
                case "Loop": loop = _lineRenderer.loop; return loop;
                case "Position Count": positionCount = _lineRenderer.positionCount; return positionCount;
                case "Material": material = _lineRenderer.material; return material;
                case "Shared Material": sharedMaterial = _lineRenderer.sharedMaterial; return sharedMaterial;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverLineRendererHandlerNode : OverExecutionFlowNode { }

    public enum LineRendererPosition { Start, End }

    [Node(Path = "Component/Engine/LineRenderer/Handlers", Name = "Set Color", Icon = "COMPONENT/ENGINE/LINERENDER")]
    [Output("Output", typeof(LineRenderer), Multiple = true)]
    public class OverSetLineRendererColor : OverLineRendererHandlerNode
    {
        [Input("LineRenderer")] public LineRenderer lineRenderer;
        [Input("Color")] public Color color;

        [Editable("Position")] public LineRendererPosition lineRendererPosition;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            Color _color = GetInputValue("Color", color);
            if (_lineRenderer != null)
            {
                switch (lineRendererPosition)
                {
                    case LineRendererPosition.Start: _lineRenderer.startColor = _color; break;
                    case LineRendererPosition.End: _lineRenderer.endColor = _color; break;
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            switch (port.Name)
            {
                case "Output": return _lineRenderer;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Engine/LineRenderer/Handlers", Name = "Set Width", Icon = "COMPONENT/ENGINE/LINERENDER")]
    [Output("Output", typeof(LineRenderer), Multiple = true)]
    public class OverSetLineRendererWidth : OverLineRendererHandlerNode
    {
        [Input("LineRenderer")] public LineRenderer lineRenderer;
        [Input("Width")] public float width;

        [Editable("Position")] public LineRendererPosition lineRendererPosition;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            float _width = GetInputValue("Width", width);

            if (_lineRenderer != null)
            {
                switch (lineRendererPosition)
                {
                    case LineRendererPosition.Start: _lineRenderer.startWidth = _width; break;
                    case LineRendererPosition.End: _lineRenderer.endWidth = _width; break;
                }
            }

            return base.Execute(data);
        }
        public override object OnRequestValue(Port port)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            switch (port.Name)
            {
                case "Output": return _lineRenderer;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Engine/LineRenderer/Handlers", Name = "Set Loop", Icon = "COMPONENT/ENGINE/LINERENDER")]
    [Output("Output", typeof(LineRenderer), Multiple = true)]
    public class OverSetLineRendererLoop : OverLineRendererHandlerNode
    {
        [Input("LineRenderer")] public LineRenderer lineRenderer;
        [Input("Loop")] public bool loop;


        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            bool _loop = GetInputValue("Loop", loop);

            if (_lineRenderer != null)
                _lineRenderer.loop = _loop;

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            switch (port.Name)
            {
                case "Output": return _lineRenderer;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Engine/LineRenderer/Handlers", Name = "Set Position Count", Icon = "COMPONENT/ENGINE/LINERENDER")]
    [Output("Output", typeof(LineRenderer), Multiple = true)]
    public class OverSetLineRendererPositionCount : OverLineRendererHandlerNode
    {
        [Input("LineRenderer")] public LineRenderer lineRenderer;
        [Input("Position Count")] public int positionCount;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            int _positionCount = GetInputValue("Position Count", positionCount);

            if (_lineRenderer != null)
                _lineRenderer.positionCount = _positionCount;

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            switch (port.Name)
            {
                case "Output": return _lineRenderer;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Engine/LineRenderer/Handlers", Name = "Set Material", Icon = "COMPONENT/ENGINE/LINERENDER")]
    [Output("Output", typeof(LineRenderer), Multiple = true)]
    public class OverSetLineRendererMaterial : OverLineRendererHandlerNode
    {
        [Input("LineRenderer", Multiple = false)] public LineRenderer lineRenderer;
        [Input("Material")] public Material material;

        [Editable("Material Type")] public RendererInteractionType type;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            LineRenderer _renderer = GetInputValue("LineRenderer", lineRenderer);
            Material _material = GetInputValue("Material", material);


            if (_renderer != null && _material != null)
            {
                if (type == RendererInteractionType.New)
                    _renderer.material = _material;

                if (type == RendererInteractionType.Shared)
                    _renderer.sharedMaterial = _material;
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            switch (port.Name)
            {
                case "Output": return _lineRenderer;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Engine/LineRenderer/Handlers", Name = "Set Position", Icon = "COMPONENT/ENGINE/LINERENDER")]
    [Output("Output", typeof(LineRenderer), Multiple = true)]
    public class OverSetLineRendererPosition : OverLineRendererHandlerNode
    {
        [Input("LineRenderer", Multiple = false)] public LineRenderer lineRenderer;
        [Input("Index")] public int index;
        [Input("Position")] public Vector3 posn;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            LineRenderer _renderer = GetInputValue("LineRenderer", lineRenderer);
            Vector3 _position = GetInputValue("Position", posn);
            int _index = GetInputValue("Index", index);

            if (_renderer != null)
            {
                _renderer.SetPosition(_index, _position);
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            LineRenderer _lineRenderer = GetInputValue("LineRenderer", lineRenderer);
            switch (port.Name)
            {
                case "Output": return _lineRenderer;
            }

            return base.OnRequestValue(port);
        }
    }
}