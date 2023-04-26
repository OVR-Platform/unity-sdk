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
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace OverSDK.VisualScripting
{
    [Tags("Event")]
    public abstract class OverEventNode : OverExecutionTriggerNode
    {
        public abstract void Register(Action specificEventToRegister = null);
        public abstract void Deregister();
    }

    //Buttons
    [Node(Path = "Flow/Event", Name = "Button Clicked", Icon = "FLOW/EVENT")]
    public class OverOnButtonClicked : OverEventNode
    {
        [Input("Button", Multiple = false)] public GameObject button;

        public override void Deregister()
        {
        }

        public override void Register(Action specificEventToRegister = null)
        {
            GameObject _obj = GetInputValue("Button", button);

            if (_obj != null)
            {
                Button _button = _obj.GetComponent<Button>();
                if (_button == null)
                    _button = _obj.AddComponent<Button>();

                _button.onClick.AddListener(() => {
                    (Graph as OverGraph).Execute(GetNextExecutableNode(), new OverExecutionFlowData() { });
                });
            }
        }
    }

    //Colliders
    public enum ColliderInteractionMode { Enter, Exit, Stay }
    [Node(Path = "Flow/Event", Name = "Trigger", Icon = "FLOW/EVENT")]
    public class OverOnTrigger : OverEventNode
    {

        [Input("Target", Multiple = false)] public GameObject target;

        [Output("Other")] public Collider other;

        [Editable("Mode")] public ColliderInteractionMode triggerMode;

        OverTriggerListener _triggerListener;

        public override void Register(Action specificEventToRegister = null)
        {
            GameObject _target = GetInputValue("Target", target);

            Collider _targetCollider = _target.GetComponent<Collider>();
            if (_targetCollider == null) _targetCollider = _target.AddComponent<Collider>();


            _triggerListener = _target.GetComponent<OverTriggerListener>();
            if (_triggerListener == null) _triggerListener = _target.AddComponent<OverTriggerListener>();

            _triggerListener.Initialize(this);

            switch (triggerMode)
            {
                case ColliderInteractionMode.Enter: _triggerListener.onTriggerEnter += OnTriggerFired; break;
                case ColliderInteractionMode.Exit: _triggerListener.onTriggerExit += OnTriggerFired; break;
                case ColliderInteractionMode.Stay: _triggerListener.onTriggerStay += OnTriggerFired; break;
            }
        }

        public override void Deregister()
        {
            if (_triggerListener != null)
            {
                _triggerListener.onTriggerEnter -= OnTriggerFired;
                _triggerListener.onTriggerExit -= OnTriggerFired;
                _triggerListener.onTriggerStay -= OnTriggerFired;
            }
        }

        public void OnTriggerFired(Collider collider)
        {
            other = collider;
            (Graph as OverGraph).Execute(GetNextExecutableNode(), new OverExecutionFlowData { });
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Other")
            {
                return other;
            }

            return base.OnRequestValue(port);
        }
    }


    [Node(Path = "Flow/Event", Name = "Collider Exposer", Icon = "FLOW/EVENT")]
    [Tags("Event")]
    [Output("Ref", typeof(Collider), Multiple = true)]
    public class OverCollider : OverNode
    {
        [Input("Collider", Multiple = false)] public Collider collider;

        [Output("Object", Multiple = true)] public GameObject gmObject;

        public override object OnRequestValue(Port port)
        {
            Collider _collider = GetInputValue("Collider", collider);

            switch (port.Name)
            {
                case "Ref": return _collider;
                case "Object":
                    gmObject = _collider.gameObject;
                    return gmObject;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Flow/Event", Name = "Collision", Icon = "FLOW/EVENT")]
    public class OverOnCollision : OverEventNode
    {

        [Input("Target", Multiple = false)] public GameObject target;

        [Output("Other")] public Collider other;
        [Output("Contacts")] public ContactPoint[] contacts;
        [Output("Impulse")] public Vector3 impulse;
        [Output("Relative Velocity")] public Vector3 relativeVelocity;

        [Editable("Mode")] public ColliderInteractionMode collisionMode;

        OverCollisionListener _collisionListener;

        Rigidbody _rigidbody;

        public override void Register(Action specificEventToRegister = null)
        {
            GameObject _target = GetInputValue("Target", target);
            Collider _targetCollider = _target.GetComponent<Collider>();
            if (_targetCollider == null) _targetCollider = _target.AddComponent<Collider>();

            _rigidbody = _target.GetComponent<Rigidbody>();
            if (_rigidbody == null)
            {
                _rigidbody = _target.AddComponent<Rigidbody>();
                _rigidbody.useGravity = false;
            }

            _collisionListener = _target.GetComponent<OverCollisionListener>();
            if (_collisionListener == null) _collisionListener = _target.AddComponent<OverCollisionListener>();

            _collisionListener.Initialize(this);

            switch (collisionMode)
            {
                case ColliderInteractionMode.Enter: _collisionListener.onCollisionEnter += OnTriggerFired; break;
                case ColliderInteractionMode.Exit: _collisionListener.onCollisionExit += OnTriggerFired; break;
                case ColliderInteractionMode.Stay: _collisionListener.onCollisionStay += OnTriggerFired; break;
            }
        }

        public override void Deregister()
        {
            if (_collisionListener != null)
            {
                _collisionListener.onCollisionEnter -= OnTriggerFired;
                _collisionListener.onCollisionExit -= OnTriggerFired;
                _collisionListener.onCollisionStay -= OnTriggerFired;
            }
        }

        public void OnTriggerFired(Collision collision)
        {
            other = collision.collider;
            contacts = collision.contacts;
            impulse = collision.impulse;
            relativeVelocity = collision.relativeVelocity;

            (Graph as OverGraph).Execute(GetNextExecutableNode(), new OverExecutionFlowData { });
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Other")
            {
                return other;
            }
            if (port.Name == "Contacts")
            {
                return contacts;
            }
            if (port.Name == "Relative Velocity")
            {
                return relativeVelocity;
            }
            if (port.Name == "Impulse")
            {
                return impulse;
            }

            return base.OnRequestValue(port);
        }
    }

    //Touch
    [Node(Path = "Flow/Event", Name = "Touch", Icon = "FLOW/EVENT")]
    public class OverOnTouch : OverEventNode
    {
        public enum OverTouchMode { Click, Press, LongPress }

        [Input("Target", Multiple = false)] public GameObject target;

        [Output("Other")] public Collider other;

        [Editable("Touch Mode")] public OverTouchMode mode;
        OverTouchListener _touchListener;

        public override void Deregister()
        {
            if (_touchListener != null)
            {
                _touchListener.onClick -= OnTriggerFired;
                _touchListener.onPress -= OnTriggerFired;
                _touchListener.onLongPress -= OnTriggerFired;
            }
        }

        public override void Register(Action specificEventToRegister = null)
        {
            GameObject _target = GetInputValue("Target", target);
            Collider _targetCollider = _target.GetComponent<Collider>();
            if (_targetCollider == null) _targetCollider = _target.AddComponent<Collider>();

            _touchListener = _target.GetComponent<OverTouchListener>();
            if (_touchListener == null) _touchListener = _target.AddComponent<OverTouchListener>();

            _touchListener.Initialize(this);

            switch (mode)
            {
                case OverTouchMode.Click: _touchListener.onClick += OnTriggerFired; break;
                case OverTouchMode.Press: _touchListener.onPress += OnTriggerFired; break;
                case OverTouchMode.LongPress: _touchListener.onLongPress += OnTriggerFired; break;
            }
        }

        public void OnTriggerFired(RaycastHit hit)
        {
            other = hit.collider;

            (Graph as OverGraph).Execute(GetNextExecutableNode(), new OverExecutionFlowData { });
        }

    }

    //custom
    [Node(Path = "Flow/Event/Custom", Name = "Custom Event", Icon = "FLOW/EVENT")]
    public class OverCustomEvent : OverEventNode
    {
        [Input("Name", Multiple = false)] public string eventName;
        public override void Deregister()
        {
        }

        public override void Register(Action specificEventToRegister = null)
        {
        }

        public void TriggerExecution(OverExecutionFlowData data)
        {
            (Graph as OverGraph).Execute(GetNextExecutableNode(), data);
        }
    }

    [Node(Path = "Flow/Event/Custom", Name = "Custom Event Trigger", Icon = "FLOW/EVENT")]
    [Tags("Event")]
    public class OverCustomEventTrigger : OverExecutionFlowNode
    {
        [Input("Name", Multiple = false)] public string eventName;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            OverCustomEvent _event = (Graph as OverGraph).GetNodes<OverCustomEvent>().Where(x => x.eventName == eventName).FirstOrDefault();

            if (_event != null)
                _event.TriggerExecution(data);

            return base.Execute(data);
        }
    }
}