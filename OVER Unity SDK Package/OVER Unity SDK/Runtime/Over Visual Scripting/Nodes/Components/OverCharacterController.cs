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
    [Tags("Component")]
    public abstract class OverCharacterControllerCommon : OverNode { }

    [Tags("Component")]
    public abstract class OverCharacterControllerHandlerNode : OverExecutionFlowNode { }


    [Node(Path = "Component/CharacterController", Name = "CharacterController Exposer", Icon = "COMPONENT/CHARACTERCONTROLLER")]
    [Tags("Component")]
    [Output("Ref", typeof(CharacterController), Multiple = true)]
    public class OverCharacterController : OverCharacterControllerCommon
    {
        [Input("CharacterController", Multiple = false)] public CharacterController characterController;

        [Output("Position", Multiple = true)] public Vector3 position;
        [Output("Rotation", Multiple = true)] public Quaternion rotation;
        [Output("Center", Multiple = true)] public Vector3 center;
        [Output("Collision Flags", Multiple = true)] public CollisionFlags collisionFlags;
        [Output("Detect Collisions", Multiple = true)] public bool detectCollisions;
        [Output("Enable Overlap Recovery", Multiple = true)] public bool enableOverlapRecovery;
        [Output("Height", Multiple = true)] public float height;
        [Output("Is Grounded", Multiple = true)] public bool isGrounded;
        [Output("Min Move Distance", Multiple = true)] public float minMoveDistance;
        [Output("Radius", Multiple = true)] public float radius;
        [Output("Skin Width", Multiple = true)] public float skinWidth;
        [Output("Slope Limit", Multiple = true)] public float slopeLimit;
        [Output("Step Offset", Multiple = true)] public float stepOffset;
        [Output("Velocity", Multiple = true)] public Vector3 velocity;

        public override object OnRequestNodeValue(Port port)
        {
            CharacterController _characterController = GetInputValue("CharacterController", characterController);

            switch (port.Name)
            {
                case "Ref":
                    return _characterController;
                case "Position":
                    position = _characterController.transform.position;
                    return position;
                case "Rotation":
                    rotation = _characterController.transform.rotation;
                    return rotation;
                case "Center":
                    center = _characterController.center;
                    return center;
                case "Collision Flags":
                    collisionFlags = _characterController.collisionFlags;
                    return collisionFlags;
                case "Detect Collisions":
                    detectCollisions = _characterController.detectCollisions;
                    return detectCollisions;
                case "Enable Overlap Recovery":
                    enableOverlapRecovery = _characterController.enableOverlapRecovery;
                    return enableOverlapRecovery;
                case "Height":
                    height = _characterController.height;
                    return height;
                case "Is Grounded":
                    isGrounded = _characterController.isGrounded;
                    return isGrounded;
                case "Min Move Distance":
                    minMoveDistance = _characterController.minMoveDistance;
                    return minMoveDistance;
                case "Radius":
                    radius = _characterController.radius;
                    return radius;
                case "Skin Width":
                    skinWidth = _characterController.skinWidth;
                    return skinWidth;
                case "Slope Limit":
                    slopeLimit = _characterController.slopeLimit;
                    return slopeLimit;
                case "Step Offset":
                    stepOffset = _characterController.stepOffset;
                    return stepOffset;
                case "Velocity":
                    velocity = _characterController.velocity;
                    return velocity;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/CharacterController/Handlers", Name = "Move", Icon = "COMPONENT/RIGIDBODY")]
    public class OverCharacterControllerMove : OverCharacterControllerHandlerNode
    {
        [Input("CharacterController")] public CharacterController characterController;

        [Input("Motion")] public Vector3 motion;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            CharacterController _target = GetInputValue("CharacterController", characterController);
            Vector3 _motion = GetInputValue("Motion", motion);

            _target.Move(_motion);

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/CharacterController/Handlers", Name = "Simple Move", Icon = "COMPONENT/RIGIDBODY")]
    public class OverCharacterControllerSimpleMove : OverCharacterControllerHandlerNode
    {
        [Input("CharacterController")] public CharacterController characterController;
        [Input("Motion")] public Vector3 speed;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            CharacterController _target = GetInputValue("CharacterController", characterController);
            Vector3 _motion = GetInputValue("Motion", speed);

            _target.SimpleMove(_motion);

            return base.Execute(data);
        }
    }
}
