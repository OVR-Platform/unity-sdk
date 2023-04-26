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
using UnityEngine.UI;

namespace OverSDK.VisualScripting
{
    [Node(Path = "Component/UI/Text", Name = "Text Exposer", Icon = "COMPONENT/UI/TEXT")]
    [Tags("Component")]
    [Output("Ref", typeof(Text), Multiple = true)]
    public class OverUIText : OverNode
    {
        [Input("Text", Multiple = false)] public Text text;

        [Output("Content", Multiple = true)] public string content;

        public override object OnRequestValue(Port port)
        {
            Text _text = GetInputValue("Text", text);

            switch (port.Name)
            {
                case "Ref": return _text;
                case "Content":
                    content = _text.text;
                    return content;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverTextHandlerNode : OverExecutionFlowNode { }

    [Node(Path = "Component/UI/Text/Handlers", Name = "Set Text", Icon = "COMPONENT/UI/TEXT")]
    [Output("Output", typeof(Text), Multiple = true)]
    public class OverSetText : OverTextHandlerNode
    {
        [Input("Text")] public Text source;
        [Input("Content")] public string content;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Text _source = GetInputValue("Text", source);
            string _content = GetInputValue("Content", content);

            _source.text = _content;

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Text _text = GetInputValue("Text", source);

            switch (port.Name)
            {
                case "Output": return _text;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/UI/Text/Handlers", Name = "Set Color", Icon = "COMPONENT/UI/TEXT")]
    [Output("Output", typeof(Text), Multiple = true)]
    public class OverSetColor : OverTextHandlerNode
    {
        [Input("Text")] public Text source;
        [Input("Color")] public Color color;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Text _source = GetInputValue("Text", source);
            Color _color = GetInputValue("Color", color);

            _source.color = _color;

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Text _text = GetInputValue("Text", source);

            switch (port.Name)
            {
                case "Output": return _text;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/UI/Text/Handlers", Name = "Set Font Size", Icon = "COMPONENT/UI/TEXT")]
    [Output("Output", typeof(Text), Multiple = true)]
    public class OverSetFontSize : OverTextHandlerNode
    {
        [Input("Text")] public Text source;
        [Input("Font Size")] public int fontSize;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Text _source = GetInputValue("Text", source);
            int _fontSize = GetInputValue("Font Size", fontSize);

            _source.fontSize = _fontSize;

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Text _text = GetInputValue("Text", source);

            switch (port.Name)
            {
                case "Output": return _text;
            }

            return base.OnRequestValue(port);
        }
    }
}