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
    [Node(Path = "Component/Animator", Name = "Animator Exposer", Icon = "COMPONENT/ANIMATOR")]
    [Tags("Component")]
    [Output("Ref", typeof(Animator), Multiple = true)]
    public class OverAnimator : OverNode
    {
        [Input("Animator", Multiple = false)] public Animator animator;

        [Output("Runtime Controller", Multiple = true)] public RuntimeAnimatorController runtimeAnimatorController;
        [Output("Avatar", Multiple = true)] public Avatar avatar;
        [Output("Playback Time", Multiple = true)] public float playbackTime;
        [Output("Playback Speed", Multiple = true)] public float speed;

        public override object OnRequestValue(Port port)
        {
            Animator _animator = GetInputValue("Animator", animator);
            switch (port.Name)
            {
                case "Ref": return _animator;
                case "Runtime Controller": runtimeAnimatorController = _animator.runtimeAnimatorController; return runtimeAnimatorController;
                case "Avatar": avatar = _animator.avatar; return avatar;
                case "Playback Time": playbackTime = _animator.playbackTime; return playbackTime;
                case "Playback Speed": speed = _animator.speed; return speed;
            }

            return base.OnRequestValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverAnimatorHandler : OverExecutionFlowNode { }


    [Node(Path = "Component/Animator/Handlers", Name = "Play Animator", Icon = "COMPONENT/ANIMATOR")]
    public class OverPlayAnimator : OverAnimatorHandler
    {
        [Input("Animator", Multiple = false)] public Animator animator;
        [Input("State")] public string state;
        [Input("Layer")] public int layer;
        [Input("Normalized Time")] public float normalizedTime;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Animator _animator = GetInputValue("Animator", animator);
            string _state = GetInputValue("State", state);
            int _layer = GetInputValue("Layer", layer);
            float _normalizedTime = Mathf.Clamp(GetInputValue("Normalized Time", normalizedTime), 0, 1);

            if (_animator != null)
            {
                _animator.Play(_state, _layer, _normalizedTime);
            }

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Animator/Handlers", Name = "Set Trigger", Icon = "COMPONENT/ANIMATOR")]
    public class OverSetAnimationTrigger : OverAnimatorHandler
    {
        [Input("Animator", Multiple = false)] public Animator animator;
        [Input("Trigger")] public string trigger;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Animator _animator = GetInputValue("Animator", animator);
            string _trigger = GetInputValue("Trigger", trigger);

            if (_animator != null)
            {
                _animator.SetTrigger(_trigger);
            }

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Animator/Handlers", Name = "Set Bool", Icon = "COMPONENT/ANIMATOR")]
    public class OverSetAnimationBool : OverAnimatorHandler
    {
        [Input("Animator", Multiple = false)] public Animator animator;
        [Input("Name")] public string paramName;
        [Input("Value")] public bool value;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Animator _animator = GetInputValue("Animator", animator);
            string _name = GetInputValue("Name", paramName);
            bool _value = GetInputValue("Value", value);

            if (_animator != null)
            {
                _animator.SetBool(_name, _value);
            }

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Animator/Handlers", Name = "Set Float", Icon = "COMPONENT/ANIMATOR")]
    public class OverSetAnimationFloat : OverAnimatorHandler
    {
        [Input("Animator", Multiple = false)] public Animator animator;
        [Input("Name")] public string paramName;
        [Input("Value")] public float value;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Animator _animator = GetInputValue("Animator", animator);
            string _name = GetInputValue("Name", paramName);
            float _value = GetInputValue("Value", value);

            if (_animator != null)
            {
                _animator.SetFloat(_name, _value);
            }

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Animator/Handlers", Name = "Reset Trigger", Icon = "COMPONENT/ANIMATOR")]
    public class OverResetAnimationTrigger : OverAnimatorHandler
    {
        [Input("Animator", Multiple = false)] public Animator animator;
        [Input("Trigger")] public string trigger;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Animator _animator = GetInputValue("Animator", animator);
            string _trigger = GetInputValue("Trigger", trigger);

            if (_animator != null)
            {
                _animator.ResetTrigger(_trigger);
            }

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Animator/Handlers", Name = "Set Runtime Controller", Icon = "COMPONENT/ANIMATOR")]
    [Output("Output", typeof(Animator), Multiple = true)]
    public class OverSetRuntimeController : OverAnimatorHandler
    {
        [Input("Animator", Multiple = false)] public Animator animator;
        [Input("RuntimeController", Multiple = false)] public RuntimeAnimatorController controller;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Animator _animator = GetInputValue("Animator", animator);
            RuntimeAnimatorController _controller = GetInputValue("RuntimeController", controller);

            if (_animator != null && _animator.avatar != null)
            {
                _animator.runtimeAnimatorController = _controller;
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Animator _animator = GetInputValue("Animator", animator);
            switch (port.Name)
            {
                case "Output": return _animator;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Component/Animator/Handlers", Name = "Set Avatar", Icon = "COMPONENT/ANIMATOR")]
    [Output("Output", typeof(Animator), Multiple = true)]
    public class OverSetAnimationAvatar : OverAnimatorHandler
    {
        [Input("Animator", Multiple = false)] public Animator animator;
        [Input("Avatar", Multiple = false)] public Avatar avatar;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Animator _animator = GetInputValue("Animator", animator);
            Avatar _avatar = GetInputValue("Avatar", avatar);

            if (_animator != null && _animator.avatar != null)
            {
                _animator.avatar = _avatar;
            }

            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Animator _animator = GetInputValue("Animator", animator);
            switch (port.Name)
            {
                case "Output": return _animator;
            }

            return base.OnRequestValue(port);
        }
    }
}