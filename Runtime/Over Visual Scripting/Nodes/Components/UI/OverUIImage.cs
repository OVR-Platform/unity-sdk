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
    [Node(Path = "Component/UI/Image", Name = "Image Exposer", Icon = "COMPONENT/UI/IMAGE")]
    [Tags("Component")]
    [Output("Ref", typeof(Image), Multiple = true)]
    public class OverUIImage : OverNode
    {
        [Input("Image", Multiple = false)] public Image image;

        [Output("Sprite")] public Sprite sprite;
        [Output("Color")] public Color color;
        [Output("Fill Amount")] public float fillAmount;
        [Output("Material")] public Material material;

        public override object OnRequestNodeValue(Port port)
        {
            Image _image = GetInputValue("Image", image);

            switch (port.Name)
            {
                case "Ref": return _image;
                case "Sprite": sprite = _image.sprite; return sprite;
                case "Color": color = _image.color; return color;
                case "Fill Amount": fillAmount = _image.fillAmount; return fillAmount;
                case "Material": material = _image.material; return material;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverImageHandlerNode : OverExecutionFlowNode { }

    [Node(Path = "Component/UI/Image/Handlers", Name = "Set Image Material", Icon = "COMPONENT/UI/IMAGE")]
    [Output("Output", typeof(Image), Multiple = true)]
    public class OverSetImageMaterial : OverImageHandlerNode
    {
        [Input("Image", Multiple = false)] public Image image;
        [Input("Material")] public Material material;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Image _image = GetInputValue("Image", image);
            Material _material = GetInputValue("Material", material);

            if (_image != null && _material != null)
            {
                _image.material = _material;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            Image _image = GetInputValue("Image", image);

            switch (port.Name)
            {
                case "Output": return _image;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/UI/Image/Handlers", Name = "Set Image Sprite", Icon = "COMPONENT/UI/IMAGE")]
    [Output("Output", typeof(Image), Multiple = true)]
    public class OverSetImageSprite : OverImageHandlerNode
    {
        [Input("Image", Multiple = false)] public Image image;
        [Input("Sprite")] public Sprite sprite;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Image _image = GetInputValue("Image", image);
            Sprite _sprite = GetInputValue("Sprite", sprite);

            if (_image != null && _sprite != null)
            {
                _image.sprite = _sprite;
            }

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            Image _image = GetInputValue("Image", image);

            switch (port.Name)
            {
                case "Output": return _image;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/UI/Image/Handlers", Name = "Set Image Color", Icon = "COMPONENT/UI/IMAGE")]
    [Output("Output", typeof(Image), Multiple = true)]
    public class OverSetImageColor : OverImageHandlerNode
    {
        [Input("Image", Multiple = false)] public Image image;
        [Input("Color")] public Color color;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Image _image = GetInputValue("Image", image);
            Color _color = GetInputValue("Color", color);

            if (_image != null)
            {
                _image.color = _color;
            }

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            Image _image = GetInputValue("Image", image);

            switch (port.Name)
            {
                case "Output": return _image;
            }

            return base.OnRequestNodeValue(port);
        }
    }
    [Node(Path = "Component/UI/Image/Handlers", Name = "Set Image Fill Amount", Icon = "COMPONENT/UI/IMAGE")]
    [Output("Output", typeof(Image), Multiple = true)]
    public class OverSetImageFillAmount : OverImageHandlerNode
    {
        [Input("Image", Multiple = false)] public Image image;
        [Input("Fill Amount")] public float fillAmount;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Image _image = GetInputValue("Image", image);
            float _fillAmount = GetInputValue("Fill Amount", fillAmount);

            if (_image != null)
            {
                _image.fillAmount = _fillAmount;
            }

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            Image _image = GetInputValue("Image", image);

            switch (port.Name)
            {
                case "Output": return _image;
            }

            return base.OnRequestNodeValue(port);
        }
    }
}