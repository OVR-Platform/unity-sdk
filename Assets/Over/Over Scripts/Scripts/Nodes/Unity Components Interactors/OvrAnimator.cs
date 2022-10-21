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

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Over
{
    public enum OvrAnimatorActionType { CrossFadeInt, CrossFadeString, SetLayerWeight, SetLookAtPosition, SetTarget, SetInteger, SetBool, SetFloat, UnityAction };

    [Serializable]
    public class OvrAnimator : OvrNode
    {
        public Animator animator;
        public OvrAnimatorActionType actionType;

        [OvrVariable]
        public OvrInt stateHashName;
        [OvrVariable]
        public OvrString stateName;
        [OvrVariable]
        public OvrFloat transitionDuration;
        [OvrVariable]
        public OvrInt layer;
        [OvrVariable]
        public OvrFloat timeOffset;
        [OvrVariable]
        public OvrFloat transitionTime;

        [OvrVariable]
        public OvrInt layerIndex;
        [OvrVariable]
        public OvrFloat weight;

        [OvrVariable]
        public OvrVector3 lookAtPosition;

        public AvatarTarget targetIndex;
        [OvrVariable]
        public OvrFloat tNormalizedTime;

        [OvrVariable]
        public OvrString parameterName;
        [OvrVariable]
        public OvrInt intValue;
        [OvrVariable]
        public OvrBool boolValue;
        [OvrVariable]
        public OvrFloat floatValue;

        public UnityEvent unityAction;

        protected override void Execution()
        {
            int layer = -1;
            float timeOffset = float.NegativeInfinity;

            if (animator == null)
            {
                if (Application.isEditor)
                    Debug.LogError("Null reference at gameObject " + gameObject.name);
                return;
            }

            switch (actionType)
            {
                case OvrAnimatorActionType.CrossFadeInt:

                    if (this.layer != null)
                        layer = this.layer.TypedVariable;
                    if (this.timeOffset != null)
                        timeOffset = this.timeOffset.TypedVariable;

                    if (stateHashName == null || transitionDuration == null || transitionTime == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
                case OvrAnimatorActionType.CrossFadeString:

                    if (this.layer != null)
                        layer = this.layer.TypedVariable;
                    if (this.timeOffset != null)
                        timeOffset = this.timeOffset.TypedVariable;

                    if (stateName == null || transitionDuration == null || transitionTime == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
                case OvrAnimatorActionType.SetLookAtPosition:

                    if (lookAtPosition == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
                case OvrAnimatorActionType.SetTarget:
                    if (tNormalizedTime == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
                case OvrAnimatorActionType.SetInteger:
                    if (parameterName == null || intValue == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
                case OvrAnimatorActionType.SetBool:
                    if (parameterName == null || boolValue == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
                case OvrAnimatorActionType.SetFloat:
                    if (parameterName == null || floatValue == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
            }

            switch (actionType)
            {
                case OvrAnimatorActionType.CrossFadeInt:
                    animator.CrossFade(stateHashName.TypedVariable, transitionDuration.TypedVariable, layer, timeOffset, transitionTime.TypedVariable);
                    break;
                case OvrAnimatorActionType.CrossFadeString:
                    animator.CrossFade(stateName.TypedVariable, transitionDuration.TypedVariable, layer, timeOffset, transitionTime.TypedVariable);
                    break;
                case OvrAnimatorActionType.SetLayerWeight:
                    animator.SetLayerWeight(layerIndex.TypedVariable, weight.TypedVariable);
                    break;
                case OvrAnimatorActionType.SetLookAtPosition:
                    animator.SetLookAtPosition(lookAtPosition.TypedVariable);
                    break;
                case OvrAnimatorActionType.SetTarget:
                    animator.SetTarget(targetIndex, tNormalizedTime.TypedVariable);
                    break;
                case OvrAnimatorActionType.SetInteger:
                    animator.SetInteger(parameterName.TypedVariable, intValue.TypedVariable);
                    break;
                case OvrAnimatorActionType.SetBool:
                    animator.SetBool(parameterName.TypedVariable, boolValue.TypedVariable);
                    break;
                case OvrAnimatorActionType.SetFloat:
                    animator.SetFloat(parameterName.TypedVariable, floatValue.TypedVariable);
                    break;
                case OvrAnimatorActionType.UnityAction:
                    unityAction?.Invoke();
                    break;
            }
        }
    }
}