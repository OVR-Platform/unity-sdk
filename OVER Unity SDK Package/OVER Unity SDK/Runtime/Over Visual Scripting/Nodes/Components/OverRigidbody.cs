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
    public enum ApplianceMode { World, Relative }

    [Node(Path = "Component/Rigidbody", Name = "Rigidbody Exposer", Icon = "COMPONENT/RIGIDBODY")]
    [Tags("Component")]
    [Output("Ref", typeof(Rigidbody), Multiple = true)]
    public class OverRigidbody : OverNode
    {
        [Input("Rigidbody", Multiple = false)] public Rigidbody rigidbody;

        [Output("Position", Multiple = true)] public Vector3 posn;
        [Output("Rotation", Multiple = true)] public Quaternion rot;

        [Output("Center of Mass", Multiple = true)] public Vector3 centerOfMass;
        [Output("Velocity", Multiple = true)] public Vector3 velocity;
        [Output("Ang. Velocity", Multiple = true)] public Vector3 angularVelocity;

        [Output("Automatic Center Of Mass", Multiple = true)] public bool automaticCenterOfMass;
        [Output("Automatic Tensor", Multiple = true)] public bool automaticTensor;
        [Output("Use Gravity", Multiple = true)] public bool useGravity;
        [Output("Is Kinematic", Multiple = true)] public bool isKinematic;

        public override object OnRequestNodeValue(Port port)
        {
            Rigidbody _rigidbody = GetInputValue("Rigidbody", rigidbody);

            switch (port.Name)
            {
                case "Ref":
                    return _rigidbody;
                case "Position":
                    posn = _rigidbody.position;
                    return posn;
                case "Rotation":
                    rot = _rigidbody.rotation;
                    return rot;
                case "Velocity":
                    velocity = _rigidbody.velocity;
                    return velocity;
                case "Ang. Velocity":
                    angularVelocity = _rigidbody.angularVelocity;
                    return angularVelocity;
                case "Center of Mass":
                    centerOfMass = _rigidbody.centerOfMass;
                    return centerOfMass;
                case "Automatic Center Of Mass":
                    automaticCenterOfMass = _rigidbody.automaticCenterOfMass;
                    return automaticCenterOfMass;
                case "Automatic Tensor":
                    automaticTensor = _rigidbody.automaticInertiaTensor;
                    return automaticTensor;
                case "Use Gravity":
                    useGravity = _rigidbody.useGravity;
                    return useGravity;
                case "Is Kinematic":
                    isKinematic = _rigidbody.isKinematic;
                    return isKinematic;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverRigidbodyHandlerNode : OverExecutionFlowNode { }


    [Node(Path = "Component/Rigidbody/Handlers", Name = "Add Force", Icon = "COMPONENT/RIGIDBODY")]
    public class OverAddForce : OverRigidbodyHandlerNode
    {
        [Input("Rigidbody")] public Rigidbody rigidbody;

        [Input("Force")] public Vector3 force;

        [Editable("Type")] public ApplianceMode type;
        [Editable("Force Mode")] public ForceMode mode;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody _target = GetInputValue("Rigidbody", rigidbody);
            Vector3 _force = GetInputValue("Force", force);

            switch (type)
            {
                case ApplianceMode.World: _target.AddForce(_force, mode); break;
                case ApplianceMode.Relative: _target.AddRelativeForce(_force, mode); break;
            }

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Rigidbody/Handlers", Name = "Add Explosion Force", Icon = "COMPONENT/RIGIDBODY")]
    public class OverAddExplosionForce : OverRigidbodyHandlerNode
    {
        [Input("Rigidbody")] public Rigidbody rigidbody;

        [Input("Force")] public float explosionForce;
        [Input("Radius")] public float explosionRadius;
        [Input("Upwards Modifier")] public float upwardsModifier;

        [Input("Explosion Position")] public Vector3 explosionPosition;

        [Editable("Force Mode")] public ForceMode mode;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody _target = GetInputValue("Rigidbody", rigidbody);
            Vector3 _explosionPosition = GetInputValue("Explosion Position", explosionPosition);

            float _explosionForce = GetInputValue("Force", explosionForce);
            float _explosionRadius = GetInputValue("Radius", explosionRadius);
            float _upwardsModifier = GetInputValue("Upwards Modifier", upwardsModifier);

            _target.AddExplosionForce(_explosionForce, _explosionPosition, _explosionRadius, _upwardsModifier, mode);
            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Rigidbody/Handlers", Name = "Add Torque", Icon = "COMPONENT/RIGIDBODY")]
    public class OverAddTorque : OverRigidbodyHandlerNode
    {
        [Input("Rigidbody")] public Rigidbody rigidbody;

        [Input("Torque")] public Vector3 torque;

        [Editable("Type")] public ApplianceMode type;
        [Editable("Force Mode")] public ForceMode mode;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody _target = GetInputValue("Rigidbody", rigidbody);
            Vector3 _torque = GetInputValue("Torque", torque);

            switch (type)
            {
                case ApplianceMode.World: _target.AddTorque(_torque, mode); break;
                case ApplianceMode.Relative: _target.AddRelativeTorque(_torque, mode); break;
            }

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Rigidbody/Handlers", Name = "Move Position", Icon = "COMPONENT/RIGIDBODY")]
    public class OverMovePosition : OverRigidbodyHandlerNode
    {
        [Input("Rigidbody")] public Rigidbody rigidbody;
        [Input("Position")] public Vector3 posn;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody _target = GetInputValue("Rigidbody", rigidbody);
            Vector3 _position = GetInputValue("Position", posn);

            _target.MovePosition(_position);

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Rigidbody/Handlers", Name = "Move Rotation", Icon = "COMPONENT/RIGIDBODY")]
    public class OverMoveRotation : OverRigidbodyHandlerNode
    {
        [Input("Rigidbody")] public Rigidbody rigidbody;
        [Input("Rotation")] public Quaternion rot;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody _target = GetInputValue("Rigidbody", rigidbody);
            Quaternion _rotation = GetInputValue("Rotation", rot);

            _target.MoveRotation(_rotation);

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Rigidbody/Handlers", Name = "Set Gravity", Icon = "COMPONENT/RIGIDBODY")]
    [Output("Updated Component", typeof(Rigidbody), Multiple = true)]
    public class OverSetGravity : OverRigidbodyHandlerNode
    {
        [Input("Rigidbody")] public Rigidbody rigidbody;
        [Input("Use Gravity")] public bool useGravity;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody _target = GetInputValue("Rigidbody", rigidbody);
            bool _useGravity = GetInputValue("Use Gravity", useGravity);

            _target.useGravity = _useGravity;

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            Rigidbody _rigidbody = GetInputValue("Rigidbody", rigidbody);

            switch (port.Name)
            {
                case "Updated Component":
                    return _rigidbody;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Rigidbody/Handlers", Name = "Set Is Kinematic", Icon = "COMPONENT/RIGIDBODY")]
    [Output("Updated Component", typeof(Rigidbody), Multiple = true)]
    public class OverSetIsKinematic : OverRigidbodyHandlerNode
    {
        [Input("Rigidbody")] public Rigidbody rigidbody;
        [Input("Is Kinematic")] public bool isKinematic;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody _target = GetInputValue("Rigidbody", rigidbody);
            bool _isKinematic = GetInputValue("Is Kinematic", isKinematic);

            _target.isKinematic = _isKinematic;

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            Rigidbody _rigidbody = GetInputValue("Rigidbody", rigidbody);

            switch (port.Name)
            {
                case "Updated Component":
                    return _rigidbody;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Rigidbody/Handlers", Name = "Set Velocity", Icon = "COMPONENT/RIGIDBODY")]
    [Output("Updated Component", typeof(Rigidbody), Multiple = true)]
    public class OverSetVelocity : OverRigidbodyHandlerNode
    {
        [Input("Rigidbody")] public Rigidbody rigidbody;
        [Input("Velocity")] public Vector3 velocity;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody _target = GetInputValue("Rigidbody", rigidbody);
            Vector3 _velocity = GetInputValue("Velocity", velocity);

            _target.velocity = _velocity;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            Rigidbody _rigidbody = GetInputValue("Rigidbody", rigidbody);

            switch (port.Name)
            {
                case "Updated Component":
                    return _rigidbody;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Rigidbody/Handlers", Name = "Set Angular Velocity", Icon = "COMPONENT/RIGIDBODY")]
    [Output("Updated Component", typeof(Rigidbody), Multiple = true)]
    public class OverSetAngularVelocity : OverRigidbodyHandlerNode
    {
        [Input("Rigidbody")] public Rigidbody rigidbody;
        [Input("Velocity")] public Vector3 angularVelocity;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody _target = GetInputValue("Rigidbody", rigidbody);
            Vector3 _angularVelocity = GetInputValue("Velocity", angularVelocity);

            _target.angularVelocity = _angularVelocity;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            Rigidbody _rigidbody = GetInputValue("Rigidbody", rigidbody);

            switch (port.Name)
            {
                case "Updated Component":
                    return _rigidbody;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    //todo: find ways to better draw bools
    [Node(Path = "Component/Rigidbody/Handlers", Name = "Set Constraints", Icon = "COMPONENT/RIGIDBODY")]
    [Output("Updated Component", typeof(Rigidbody), Multiple = true)]
    public class OverSetConstraints : OverRigidbodyHandlerNode
    {
        [Input("Rigidbody")] public Rigidbody rigidbody;
        [Input("Position")] public Vector3 posn;
        [Input("Rotation")] public Vector3 rot;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Rigidbody _target = GetInputValue("Rigidbody", rigidbody);
            Vector3 _posn = GetInputValue("Position", posn);
            Vector3 _rot = GetInputValue("Rotation", rot);

            RigidbodyConstraints editedConstraints = new RigidbodyConstraints();

            if(_posn.x > 0)
            {
                editedConstraints |= RigidbodyConstraints.FreezePositionX;
            }

            if(_posn.y > 0)
            {
                editedConstraints |= RigidbodyConstraints.FreezePositionY;
            }

            if(_posn.z > 0)
            {
                editedConstraints |= RigidbodyConstraints.FreezePositionZ;
            }

            if (_rot.x > 0)
            {
                editedConstraints |= RigidbodyConstraints.FreezeRotationX;
            }

            if (_rot.y > 0)
            {
                editedConstraints |= RigidbodyConstraints.FreezeRotationY;
            }

            if (_rot.z > 0)
            {
                editedConstraints |= RigidbodyConstraints.FreezeRotationZ;
            }

            _target.constraints = editedConstraints;
            return base.Execute(data);
        }

        public override object OnRequestValue(Port port)
        {
            Rigidbody _rigidbody = GetInputValue("Rigidbody", rigidbody);

            switch (port.Name)
            {
                case "Updated Component":
                    return _rigidbody;
            }

            return base.OnRequestValue(port);
        }
    }
}