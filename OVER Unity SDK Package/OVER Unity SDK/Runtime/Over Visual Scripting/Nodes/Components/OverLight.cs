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
    [Node(Path = "Component/Light", Name = "Light Exposer", Icon = "COMPONENT/LIGHTS")]
    [Tags("Component")]
    [Output("Ref", typeof(Light), Multiple = true)]
    public class OverLight : OverNode
    {
        [Input("Light", Multiple = false)] public Light light;

        [Output("Intensity", Multiple = true)] public float intensity;
        [Output("Color", Multiple = true)] public Color color;
        [Output("Range", Multiple = true)] public float range;

        public override object OnRequestValue(Port port)
        {
            Light _light = GetInputValue("Light", light);

            switch (port.Name)
            {
                case "Ref":
                    return _light;
                case "Intensity":
                    intensity = _light.intensity;
                    return intensity;
                case "Color":
                    color = _light.color;
                    return color;
                case "Range":
                    range = _light.range;
                    return range;
            }

            return base.OnRequestValue(port);
        }
    }
    [Tags("Component")]
    public abstract class OverLightHandlerNode : OverExecutionFlowNode { }

    [Node(Path = "Component/Light/Handlers", Name = "Set Intensity", Icon = "COMPONENT/LIGHTS")]
    [Output("Output", typeof(Light), Multiple = true)]
    public class OverLightSetIntensity : OverLightHandlerNode
    {
        [Input("Light")] public Light light;
        [Input("Intensity")] public float intensity;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Light _light = GetInputValue("Light", light);
            float _intensity = Mathf.Clamp(GetInputValue("Intensity", intensity), 0, 8);

            _light.intensity = _intensity;

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Light _light = GetInputValue("Light", light);

            switch (port.Name)
            {
                case "Output": return _light;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Light/Handlers", Name = "Set Color", Icon = "COMPONENT/LIGHTS")]
    [Output("Output", typeof(Light), Multiple = true)]
    public class OverLightSetColor : OverLightHandlerNode
    {
        [Input("Light")] public Light light;
        [Input("Color")] public Color color;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Light _light = GetInputValue("Light", light);
            Color _color = GetInputValue("Color", color);

            _light.color = _color;

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Light _light = GetInputValue("Light", light);

            switch (port.Name)
            {
                case "Output": return _light;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Light/Handlers", Name = "Set Range", Icon = "COMPONENT/LIGHTS")]
    [Output("Output", typeof(Light), Multiple = true)]
    public class OverLightSetRange : OverLightHandlerNode
    {
        [Input("Light")] public Light light;
        [Input("Range")] public float range;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Light _light = GetInputValue("Light", light);
            float _range = GetInputValue("Range", range);

            _light.range = _range;

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Light _light = GetInputValue("Light", light);

            switch (port.Name)
            {
                case "Output": return _light;
            }

            return base.OnRequestValue(port);
        }
    }
}