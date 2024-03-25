/**
 * OVER Unity SDK License
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
    [Node(Path = "Data/Multimedia", Name = "Color Value", Icon = "DATA/MEDIA")]
    [Tags("Data")]

    [Output("Value", typeof(Color), Multiple = true)]

    public class OverColor : OverNode
    {
        [Input("")] public Color value;

        public override object OnRequestNodeValue(Port port)
        {
            var _value = GetInputValue("", value);
            return _value;
        }
    }

    public abstract class OverColorOperationNode : OverNode { }

    [Node(Path = "Data/Multimedia", Name = "Merge Color", Icon = "DATA/MEDIA")]
    [Tags("Data")]

    [Output("Value", typeof(Color), Multiple = true)]

    public class OverMergeColor : OverColorOperationNode
    {
        [Input("r")] public float r = 0;
        [Input("g")] public float g = 0;
        [Input("b")] public float b = 0;
        [Input("a")] public float a = 1;

        public override object OnRequestNodeValue(Port port)
        {
            float _r = GetInputValue("r", r);
            float _g = GetInputValue("g", g);
            float _b = GetInputValue("b", b);
            float _a = GetInputValue("a", a);

            return new Color(_r, _g, _b, _a);
        }
    }

    [Node(Path = "Data/Multimedia", Name = "Split Color", Icon = "DATA/MEDIA")]
    [Tags("Data")]

    public class OverSplitColor : OverColorOperationNode
    {
        [Input("Color")] public Color color;

        [Output("r", Multiple = true)] public float r = 0;
        [Output("g", Multiple = true)] public float g = 0;
        [Output("b", Multiple = true)] public float b = 0;
        [Output("a", Multiple = true)] public float a = 1;

        public override object OnRequestNodeValue(Port port)
        {
            Color _color = GetInputValue("Color", color);

            switch (port.Name)
            {
                case "r": return _color.r;
                case "g": return _color.g;
                case "b": return _color.b;
                case "a": return _color.a;
            }

            return base.OnRequestNodeValue(port);
        }
    }
}