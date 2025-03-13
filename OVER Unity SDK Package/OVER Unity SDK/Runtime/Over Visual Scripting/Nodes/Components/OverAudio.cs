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
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [Node(Path = "Component/Audio", Name = "Audio Exposer", Icon = "COMPONENT/AUDIO")]
    [Tags("Component")]
    [Output("Ref", typeof(AudioSource), Multiple = true)]
    public class OverAudioSource : OverNode
    {
        [Input("Audio Source", Multiple = false)] public AudioSource audioSource;

        [Output("Audio Clip", Multiple = true)] public AudioClip clip;
        [Output("Volume", Multiple = true)] public float volume;
        [Output("Is Playing", Multiple = true)] public bool isPlaying;

        public override object OnRequestNodeValue(Port port)
        {
            AudioSource _audioSource = GetInputValue("Audio Source", audioSource);

            switch (port.Name)
            {
                case "Ref": return _audioSource;
                case "Audio Clip":
                    clip = _audioSource.clip;
                    return clip;
                case "Volume":
                    volume = Mathf.Clamp(_audioSource.volume, 0, 1);
                    return volume;
                case "Is Playing":
                    isPlaying = _audioSource.isPlaying;
                    return isPlaying;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverAudioHandlerNode : OverExecutionFlowNode { }

    [Node(Path = "Component/Audio/Handlers", Name = "Set Audio Clip", Icon = "COMPONENT/AUDIO")]
    [Output("Output", typeof(AudioSource), Multiple = true)]
    public class OverAudioSourceSetClip : OverAudioHandlerNode
    {
        [Input("Audio Source")] public AudioSource source;
        [Input("Audio Clip")] public AudioClip clip;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            AudioClip _clip = GetInputValue("Audio Clip", clip);
            _source.clip = _clip;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            switch (port.Name)
            {
                case "Output": return _source;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Audio/Handlers", Name = "Play", Icon = "COMPONENT/AUDIO")]
    [Output("Output", typeof(AudioSource), Multiple = true)]
    public class OverAudioSourcePlay : OverAudioHandlerNode
    {
        [Input("Audio Source")] public AudioSource source;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            if (_source != null && _source.clip != null)
                _source.Play();
            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            switch (port.Name)
            {
                case "Output": return _source;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Audio/Handlers", Name = "Pause", Icon = "COMPONENT/AUDIO")]
    [Output("Output", typeof(AudioSource), Multiple = true)]
    public class OverAudioSourcePause : OverAudioHandlerNode
    {
        [Input("Audio Source")] public AudioSource source;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            if (_source != null)
                _source.Pause();
            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            switch (port.Name)
            {
                case "Output": return _source;
            }

            return base.OnRequestNodeValue(port);
        }
    }
    [Node(Path = "Component/Audio/Handlers", Name = "UnPause", Icon = "COMPONENT/AUDIO")]
    [Output("Output", typeof(AudioSource), Multiple = true)]
    public class OverAudioSourceUnPause : OverAudioHandlerNode
    {
        [Input("Audio Source")] public AudioSource source;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            if (_source != null)
                _source.UnPause();
            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            switch (port.Name)
            {
                case "Output": return _source;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Audio/Handlers", Name = "Stop", Icon = "COMPONENT/AUDIO")]
    [Output("Output", typeof(AudioSource), Multiple = true)]
    public class OverAudioSourceStop : OverAudioHandlerNode
    {
        [Input("Audio Source")] public AudioSource source;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            if (_source != null)
                _source.Stop();
            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            switch (port.Name)
            {
                case "Output": return _source;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Audio/Handlers", Name = "Play Scheduled", Icon = "COMPONENT/AUDIO")]
    [Output("Output", typeof(AudioSource), Multiple = true)]
    public class OverAudioSourcePlayScheduled : OverAudioHandlerNode
    {
        [Input("Audio Source")] public AudioSource source;
        [Input("Time")] public float time;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            float _time = GetInputValue("Time", time);

            if (_source != null)
                _source.PlayScheduled(_time);

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            switch (port.Name)
            {
                case "Output": return _source;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Audio/Handlers", Name = "Play at Point", Icon = "COMPONENT/AUDIO")]
    public class OverAudioSourcePlayAtPoint : OverAudioHandlerNode
    {
        [Input("Audio Clip")] public AudioClip clip;
        [Input("Position")] public Vector3 posn;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioClip _clip = GetInputValue("Audio Clip", clip);
            Vector3 _position = GetInputValue("Position", posn);

            if (_clip != null)
                AudioSource.PlayClipAtPoint(_clip, _position);

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Audio/Handlers", Name = "Mute", Icon = "COMPONENT/AUDIO")]
    [Output("Output", typeof(AudioSource), Multiple = true)]
    public class OverAudioSourceMute : OverAudioHandlerNode
    {
        [Input("Audio Source")] public AudioSource source;
        [Input("Mute")] public bool mute;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            bool _mute = GetInputValue("Mute", mute);

            if (_source != null)
                _source.mute = _mute;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            AudioSource _source = GetInputValue("Audio Source", source);
            switch (port.Name)
            {
                case "Output": return _source;
            }

            return base.OnRequestNodeValue(port);
        }
    }
}