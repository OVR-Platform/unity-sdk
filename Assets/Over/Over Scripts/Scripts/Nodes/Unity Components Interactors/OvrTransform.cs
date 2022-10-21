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

namespace Over
{
    public enum OvrTransformActionType
    {
        SetLocalPosition = 0,
        SetWorldPosition = 1,
        SetLocalRotation = 2,
        SetWorldRotation = 3,
        SetLocalScale = 4,

        GetPosition = 100,
        GetLocalPosition = 101,
        GetRotation = 102,
        GetLocalRotation = 103,
        GetLocalScale = 104,

        GetForward = 200,
        GetUp = 201,
        GetRight = 202,
        GetLocalForward = 210,
        GetLocalUp = 211,
        GetLocalRight = 212,


        UnityAction = 999
    };

    public class OvrTransform : OvrNode   //DA RIVADERE
    {
        public Transform target;

        public OvrTransformActionType actionType;

        //Fixed
        [OvrVariable]
        public OvrVector3 targetPosition;
        [OvrVariable]
        public OvrQuaternion targetRotation;
        [OvrVariable]
        public OvrVector3 targetScale;

        [OvrVariable]
        public OvrVector3 targetDir;

        //UnityEvent
        public UnityEvent unityAction;

        protected override void Execution()
        {
            if (target == null)
            {
                Debug.LogError("Transform Missing");
                return;
            }

            switch (actionType)
            {
                case OvrTransformActionType.SetLocalPosition:
                case OvrTransformActionType.SetWorldPosition:
                    if (targetPosition == null)
                    {
                        Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;              
                case OvrTransformActionType.SetLocalRotation:
                case OvrTransformActionType.SetWorldRotation:
                    if (targetRotation == null)
                    {
                        Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
                case OvrTransformActionType.SetLocalScale:
                    if (targetScale == null)
                    {
                        Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
            }

            switch (actionType)
            {
                case OvrTransformActionType.SetLocalPosition:
                    target.localPosition = targetPosition.TypedVariable;
                    break;
                case OvrTransformActionType.SetWorldPosition:
                    target.position = targetPosition.TypedVariable;
                    break;
                case OvrTransformActionType.SetLocalRotation:
                    target.localRotation = targetRotation.TypedVariable;
                    break;
                case OvrTransformActionType.SetWorldRotation:
                    target.rotation = targetRotation.TypedVariable;
                    break;
                case OvrTransformActionType.SetLocalScale:
                    target.localScale = targetScale.TypedVariable;
                    break;
                case OvrTransformActionType.GetPosition:
                    targetPosition.TypedVariable = target.position;
                    break;
                case OvrTransformActionType.GetLocalPosition:
                    targetPosition.TypedVariable = target.localPosition;
                    break;
                case OvrTransformActionType.GetRotation:
                    targetRotation.TypedVariable = target.rotation;
                    break;
                case OvrTransformActionType.GetLocalRotation:
                    targetRotation.TypedVariable = target.localRotation;
                    break;
                case OvrTransformActionType.GetLocalScale:
                    targetScale.TypedVariable = target.localScale;
                    break;
                case OvrTransformActionType.GetForward:
                    targetDir.TypedVariable = target.forward;
                    break;
                case OvrTransformActionType.GetUp:
                    targetDir.TypedVariable = target.up;
                    break;
                case OvrTransformActionType.GetRight:
                    targetDir.TypedVariable = target.right;
                    break;
                case OvrTransformActionType.GetLocalForward:
                    if (target.parent != null)
                        targetDir.TypedVariable = target.parent.InverseTransformDirection(target.forward);
                    else
                        targetDir.TypedVariable = target.forward;
                    break;
                case OvrTransformActionType.GetLocalUp:
                    if (target.parent != null)
                        targetDir.TypedVariable = target.parent.InverseTransformDirection(target.up);
                    else
                        targetDir.TypedVariable = target.up;
                    break;
                case OvrTransformActionType.GetLocalRight:
                    if (target.parent != null)
                        targetDir.TypedVariable = target.parent.InverseTransformDirection(target.right);
                    else
                        targetDir.TypedVariable = target.right;
                    break;
                case OvrTransformActionType.UnityAction:
                    unityAction?.Invoke();
                    break;
            }
        }
    }
}