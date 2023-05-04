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

using UnityEngine;
using BlueGraph;
using UnityEngine.UI;

namespace OverSDK.VisualScripting
{
    [Node(Path = "Component/UI/RawImage", Name = "RawImage Exposer", Icon = "COMPONENT/UI/RAWIMAGE")]
    [Tags("Component")]
    [Output("Ref", typeof(RawImage), Multiple = true)]
    public class OverUIRawImage : OverNode
    {
        [Input("Image", Multiple = false)] public RawImage image;

        [Output("Texture")] public Texture2D texture;
        [Output("Color")] public Color color;
        [Output("Material")] public Material material;

        public override object OnRequestValue(Port port)
        {
            RawImage _image = GetInputValue("Image", image);

            switch (port.Name)
            {
                case "Ref": return _image;
                case "Texture": texture = _image.texture as Texture2D; return texture;
                case "Color": color = _image.color; return color;
                case "Material": material = _image.material; return material;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/UI/RawImage/Handlers", Name = "Set RawImage Material", Icon = "COMPONENT/UI/RAWIMAGE")]
    [Output("Output", typeof(RawImage), Multiple = true)]
    public class OverSetRawImageMaterial : OverImageHandlerNode
    {
        [Input("Image", Multiple = false)] public RawImage image;
        [Input("Material")] public Material material;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RawImage _image = GetInputValue("Image", image);
            Material _material = GetInputValue("Material", material);

            if (_image != null && _material != null)
            {
                _image.material = _material;
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            RawImage _image = GetInputValue("Image", image);

            switch (port.Name)
            {
                case "Output": return _image;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/UI/RawImage/Handlers", Name = "Set RawImage Texture", Icon = "COMPONENT/UI/RAWIMAGE")]
    [Output("Output", typeof(RawImage), Multiple = true)]
    public class OverSetRawImageTexture : OverImageHandlerNode
    {
        [Input("Image", Multiple = false)] public RawImage image;
        [Input("Texture")] public Texture texture;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RawImage _image = GetInputValue("Image", image);
            Texture _texture = GetInputValue("Texture", texture);

            if (_image != null && _texture != null)
            {
                _image.texture = _texture;
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            RawImage _image = GetInputValue("Image", image);

            switch (port.Name)
            {
                case "Output": return _image;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/UI/RawImage/Handlers", Name = "Set RawImage Color", Icon = "COMPONENT/UI/RAWIMAGE")]
    [Output("Output", typeof(RawImage), Multiple = true)]
    public class OverSetRawImageColor : OverImageHandlerNode
    {
        [Input("Image", Multiple = false)] public RawImage image;
        [Input("Color")] public Color color;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            RawImage _image = GetInputValue("Image", image);
            Color _color = GetInputValue("Color", color);

            if (_image != null)
            {
                _image.color = _color;
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            RawImage _image = GetInputValue("Image", image);

            switch (port.Name)
            {
                case "Output": return _image;
            }

            return base.OnRequestValue(port);
        }
    }
}
