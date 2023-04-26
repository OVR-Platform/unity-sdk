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
using UnityEngine.Video;

namespace OverSDK.VisualScripting
{
    [Node(Path = "Component/Video", Name = "Video Player Exposer", Icon = "COMPONENT/VIDEO")]
    [Tags("Component")]
    [Output("Ref", typeof(VideoPlayer), Multiple = true)]
    public class OverVideoPlayer : OverNode
    {
        [Input("Video Player", Multiple = false)] public VideoPlayer videoPlayer;

        [Output("Is Playing", Multiple = true)] public bool isPlaying;
        [Output("Is Looping", Multiple = true)] public bool isLooping;

        public override object OnRequestValue(Port port)
        {
            VideoPlayer _videoPlayer = GetInputValue("Video Player", videoPlayer);

            switch (port.Name)
            {
                case "Ref": return _videoPlayer;
                case "Is Playing":
                    isPlaying = _videoPlayer.isPlaying;
                    return isPlaying;
                case "Is Looping":
                    isPlaying = _videoPlayer.isLooping;
                    return isLooping;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverVideoHandlerNode : OverExecutionFlowNode { }

    [Node(Path = "Component/Video/Handlers", Name = "Play", Icon = "COMPONENT/VIDEO")]
    public class OverVideoPlayerPlay : OverVideoHandlerNode
    {
        [Input("Video Player", Multiple = false)] public VideoPlayer videoPlayer;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            VideoPlayer _source = GetInputValue("Video Player", videoPlayer);
            if (_source != null)
                _source.Play();
            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Video/Handlers", Name = "Pause", Icon = "COMPONENT/VIDEO")]
    public class OverVideoPlayerPause : OverVideoHandlerNode
    {
        [Input("Video Player", Multiple = false)] public VideoPlayer videoPlayer;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            VideoPlayer _source = GetInputValue("Video Player", videoPlayer);
            if (_source != null)
                _source.Pause();
            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Video/Handlers", Name = "Stop", Icon = "COMPONENT/VIDEO")]
    public class OverVideoPlayerStop : OverVideoHandlerNode
    {
        [Input("Video Player", Multiple = false)] public VideoPlayer videoPlayer;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            VideoPlayer _source = GetInputValue("Video Player", videoPlayer);
            if (_source != null)
                _source.Stop();
            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Video/Handlers", Name = "Set Video Clip", Icon = "COMPONENT/VIDEO")]
    public class OverVideoPlayerSetClip : OverVideoHandlerNode
    {
        [Input("Video Player", Multiple = false)] public VideoPlayer videoPlayer;
        [Input("Video Clip", Multiple = false)] public VideoClip clip;
        [Output("Player Out")] public VideoPlayer playerOut;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            VideoPlayer _source = GetInputValue("Video Player", videoPlayer);
            VideoClip _clip = GetInputValue("Video Clip", clip);

            if (_source != null)
            {
                _source.source = VideoSource.VideoClip;
                _source.clip = _clip;
                _source.Prepare();      

                videoPlayer = _source;
            }
            return base.Execute(data);
        }
        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Player Out")
            {
                playerOut = videoPlayer;
                return playerOut;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Video/Handlers", Name = "Set Video URL", Icon = "COMPONENT/VIDEO")]
    public class OverVideoPlayerSetUrl : OverVideoHandlerNode
    {
        [Input("Video Player", Multiple = false)] public VideoPlayer videoPlayer;
        [Input("URL", Multiple = false)] public string url;

        [Output("Player Out")] public VideoPlayer playerOut;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            VideoPlayer _source = GetInputValue("Video Player", videoPlayer);
            string _url = GetInputValue("URL", url);

            if (_source != null)
            {
                _source.source = VideoSource.Url;
                _source.url = _url;
                _source.Prepare();   

                videoPlayer = _source;
            }
            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Player Out")
            {
                playerOut = videoPlayer;
                return playerOut;
            }

            return base.OnRequestValue(port);
        }
    }
}