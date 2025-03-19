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
 * with services provided by OVER.
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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OverSDK.VisualScripting
{
    [Tags("Component")]
    public abstract class OverImageStreamerCommon : OverNode { }
    [Tags("Component")]
    public abstract class OverImageStreamerHandler : OverExecutionFlowNode { }

    [Node(Path = "Component/ImageStreamer", Name = "ImageStreamer Exposer", Icon = "COMPONENT/IMAGESTREAMER")]
    [Tags("Component")]
    [Output("Ref", typeof(ImageStreamer), Multiple = true)]
    public class OverImageStreamer : OverImageStreamerCommon
    {
        [Input("Image Streamer", Multiple = false)] public ImageStreamer streamer;
        [Output("URL", Multiple = true)] public string url;
        [Output("RawImage", Multiple = true)] public RawImage rawImage;
        [Output("Renderer", Multiple = true)] public Renderer renderer;
        [Output("Texture", Multiple = true)] public Texture2D texture;


        public override object OnRequestNodeValue(Port port)
        {
            ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);

            switch (port.Name)
            {
                case "Ref":
                    return _streamer;
                case "URL":
                    url = _streamer.url;
                    return url;
                case "RawImage":
                    rawImage = _streamer.targetRawImage;
                    return rawImage;
                case "Renderer":
                    renderer = _streamer.targetRenderer;
                    return renderer;
                case "Texture":
                    texture = _streamer.Texture;
                    return texture;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/ImageStreamer/Handlers", Name = "Set URL", Icon = "COMPONENT/IMAGESTREAMER")]
    [Output("Updated Component", typeof(ImageStreamer), Multiple = true)]
    public class OverSetURLImageStreamer : OverImageStreamerHandler
    {
        [Input("Image Streamer")] public ImageStreamer streamer;
        [Input("URL")] public string url;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);
            string _url = GetInputValue("URL", url);
            _streamer.url = _url;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);

            switch (port.Name)
            {
                case "Updated Component":
                    return _streamer;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/ImageStreamer/Handlers", Name = "Set Raw Image", Icon = "COMPONENT/IMAGESTREAMER")]
    [Output("Updated Component", typeof(ImageStreamer), Multiple = true)]
    public class OverSetRawImageImageStreamer : OverImageStreamerHandler
    {
        [Input("Image Streamer")] public ImageStreamer streamer;
        [Input("RawImage")] public RawImage rawImage;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);
            RawImage _rawImage = GetInputValue("RawImage", rawImage);
            _streamer.targetRawImage = _rawImage;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);

            switch (port.Name)
            {
                case "Updated Component":
                    return _streamer;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/ImageStreamer/Handlers", Name = "Set Renderer", Icon = "COMPONENT/IMAGESTREAMER")]
    [Output("Updated Component", typeof(ImageStreamer), Multiple = true)]
    public class OverSetRendererImageStreamer : OverImageStreamerHandler
    {
        [Input("Image Streamer")] public ImageStreamer streamer;
        [Input("Renderer")] public Renderer renderer;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);
            Renderer _renderer = GetInputValue("Renderer", renderer);
            _streamer.targetRenderer = _renderer;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);

            switch (port.Name)
            {
                case "Updated Component":
                    return _streamer;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/ImageStreamer/Handlers", Name = "Play", Icon = "COMPONENT/IMAGESTREAMER")]
    [Output("Updated Component", typeof(ImageStreamer), Multiple = true)]
    public class OverPlayImageStreamer : OverImageStreamerHandler
    {
        [Input("Image Streamer")] public ImageStreamer streamer;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);
            _streamer.Play();

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);

            switch (port.Name)
            {
                case "Updated Component":
                    return _streamer;
            }

            return base.OnRequestNodeValue(port);
        }
    }
    //[Node(Path = "Component/ImageStreamer/Handlers", Name = "Stop", Icon = "COMPONENT/IMAGESTREAMER")]
    //[Output("Updated Component", typeof(ImageStreamer), Multiple = true)]
    //public class OverStopImageStreamer : OverObjectHandlerNode
    //{
    //    [Input("Image Streamer")] public ImageStreamer streamer;

    //    public override IExecutableOverNode Execute(OverExecutionFlowData data)
    //    {
    //        ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);
    //        _streamer.Stop();

    //        return base.Execute(data);
    //    }

    //    public override object OnRequestNodeValue(Port port)
    //    {
    //        ImageStreamer _streamer = GetInputValue("Image Streamer", streamer);

    //        switch (port.Name)
    //        {
    //            case "Updated Component":
    //                return _streamer;
    //        }

    //        return base.OnRequestNodeValue(port);
    //    }
    //}

}
