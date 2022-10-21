/**
 * OVER Unity SDK License
 *
 * Copyright 2021 Over The Realty
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

using UnityEngine;
using UnityEngine.Events;
using System.ComponentModel;

namespace Over
{
    public enum OvrRigidbodyActionType { ExplosiveForce, Force, RelativeForce, RelativeTorque, Torque, MovePosition, MoveRotation, ResetVelocity, UnityAction };
    public enum OvrDirection { Forward, Back, Up, Down, Left, Right, Custom };

    public class OvrRigidBody : OvrNode
    {
        public Rigidbody rigidBody;
        public OvrRigidbodyActionType actionType;

        //All
        [OvrVariable]
        public OvrFloat forceValue;

        public OvrSpace space;
        public OvrDirection direction;

        //Custom Direction
        [OvrVariable]
        public OvrVector3 customDirection;

        public ForceMode forceMode;

        //Explosive Force
        [OvrVariable]
        public OvrFloat explosionRadius;
        [OvrVariable]
        public OvrFloat upwardsModifier;

        //Move Rotation
        [OvrVariable]
        public OvrQuaternion quaternion;

        //UnityEvent
        public UnityEvent unityAction;

        protected override void Execution()
        {
            switch (actionType)
            {
                case OvrRigidbodyActionType.UnityAction:
                    break;
                default:
                    if (rigidBody == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
            }

            switch (actionType)
            {
                case OvrRigidbodyActionType.ExplosiveForce:

                    if (forceValue != null && explosionRadius != null && upwardsModifier != null)
                        rigidBody.AddExplosionForce(forceValue.TypedVariable, GetDirection(), explosionRadius.TypedVariable, upwardsModifier.TypedVariable, forceMode);
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    break;
                case OvrRigidbodyActionType.Force:

                    if (forceValue != null)
                        rigidBody.AddForce(GetDirection() * forceValue.TypedVariable, forceMode);
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    break;
                case OvrRigidbodyActionType.RelativeForce:

                    if (forceValue != null)
                        rigidBody.AddRelativeForce(GetDirection() * forceValue.TypedVariable, forceMode);
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    break;
                case OvrRigidbodyActionType.RelativeTorque:

                    if (forceValue != null)
                        rigidBody.AddRelativeTorque(GetDirection() * forceValue.TypedVariable, forceMode);
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    break;
                case OvrRigidbodyActionType.Torque:

                    if (forceValue != null)
                        rigidBody.AddTorque(GetDirection() * forceValue.TypedVariable, forceMode);
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    break;
                case OvrRigidbodyActionType.MovePosition:

                    rigidBody.MovePosition(GetDirection());

                    break;
                case OvrRigidbodyActionType.MoveRotation:

                    if (quaternion != null)
                        rigidBody.MoveRotation(quaternion.TypedVariable);
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    break;
                case OvrRigidbodyActionType.ResetVelocity:

                    rigidBody.velocity = new Vector3(0f, 0f, 0f);
                    rigidBody.angularVelocity = new Vector3(0f, 0f, 0f);

                    break;
                case OvrRigidbodyActionType.UnityAction:
                    unityAction?.Invoke();
                    break;
            }
        }

        private Vector3 GetDirection()
        {
            Vector3 customDirection = Vector3.zero;

            switch (direction)
            {
                case OvrDirection.Custom:
                    if (this.customDirection != null)
                        customDirection = this.customDirection.TypedVariable;
                    break;
            }

            if (space == OvrSpace.Local)
            {
                return direction switch
                {
                    OvrDirection.Forward => this.transform.forward,
                    OvrDirection.Back => -(this.transform.forward),
                    OvrDirection.Right => this.transform.right,
                    OvrDirection.Left => -(this.transform.right),
                    OvrDirection.Up => this.transform.up,
                    OvrDirection.Down => -(this.transform.up),
                    OvrDirection.Custom => customDirection,
                    _ => this.transform.forward,
                };
            }
            else if (space == OvrSpace.World)
            {
                return direction switch
                {
                    OvrDirection.Forward => Vector3.forward,
                    OvrDirection.Back => -(Vector3.forward),
                    OvrDirection.Right => Vector3.right,
                    OvrDirection.Left => -(Vector3.right),
                    OvrDirection.Up => Vector3.up,
                    OvrDirection.Down => -(Vector3.up),
                    OvrDirection.Custom => customDirection,
                    _ => Vector3.forward,
                };
            }
            else
                return Vector3.forward;
        }

        //public void ClosestPointOnBounds(Vector3 position) { rigidBody.ClosestPointOnBounds(position); }
        //public void IsSleeping() { rigidBody.IsSleeping(); }
        //public void SweepTest(Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
        //{ rigidBody.SweepTest(direction, out hitInfo, maxDistance, queryTriggerInteraction); }
        //public void SweepTestAll(Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
        //{ rigidBody.SweepTestAll(direction, maxDistance, queryTriggerInteraction); }
    }
}