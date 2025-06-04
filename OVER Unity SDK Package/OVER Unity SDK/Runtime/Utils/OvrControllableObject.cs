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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverSDK
{
    public enum OvrControllableObjectType { Idle, Walk };
    [RequireComponent(typeof(Animation))]

    public class OvrControllableObject : MonoBehaviour
    {
        public OvrControllableObjectType BehaviourType;


        private string[] IDLE_ANIMATION_NAMES = { "idle", "01 idle", "idle 01" };
        private string[] WALK_ANIMATION_NAMES = { "walk", "01 walk", "walk 01" };

        [Space]
        [SerializeField] private bool isControllable = true;
        public bool IsControllable { get => isControllable; set => isControllable = value; }

        [Space]
        [SerializeField] private AnimationClip animationIdle = null;
        
        public AnimationClip AnimationIdle
        {
            get
            {
                if (animationIdle == null)
                {
                    Animation ObjectAnimation = gameObject.GetComponentInChildren<Animation>();
                    if (ObjectAnimation != null)
                    {
                        foreach (AnimationState state in ObjectAnimation)
                        {
                            string animNameToCheck = state.name.ToLower();
                            foreach (string IDLE_ANIMATION_NAME in IDLE_ANIMATION_NAMES)
                            {
                                if (animNameToCheck.Contains(IDLE_ANIMATION_NAME))
                                {
                                    animationIdle = state.clip;
                                    break;
                                }
                            }
                        }
                    }

                }

                return animationIdle;
            }
            set
            {
                animationIdle = value;
                gameObject.GetComponent<Animation>().AddClip(animationIdle, animationIdle.name);

            }
        }
        
        [Space]
        [SerializeField] private AnimationClip animationWalk = null;
        
        public AnimationClip AnimationWalk
        {
            get
            {
                //find walk animation

                if (animationWalk == null)
                {
                    Animation ObjectAnimation = gameObject.GetComponentInChildren<Animation>();
                    if (ObjectAnimation != null)
                    {
                        foreach (AnimationState state in ObjectAnimation)
                        {
                            string animNameToCheck = state.name.ToLower();
                            foreach (string WALK_ANIMATION_NAME in WALK_ANIMATION_NAMES)
                            {
                                if (animNameToCheck.Contains(WALK_ANIMATION_NAME))
                                {
                                    animationWalk = state.clip;
                                    break;
                                }
                            }
                        }
                    }
                }

                return animationWalk;
            }
            set { 
                animationWalk = value;
                gameObject.GetComponent<Animation>().AddClip(AnimationWalk, AnimationWalk.name);
            }
        }
        

        [Space]
        [SerializeField] private int walkMoveSpeed = -1;
        public int WalkMoveSpeed { get => walkMoveSpeed; set => walkMoveSpeed = value; }

        [Space]
        [SerializeField] private int walkAnimationSpeedMultiplier = -1;
        public int WalkAnimationSpeedMultiplier { get => walkAnimationSpeedMultiplier; set => walkAnimationSpeedMultiplier = value; }


        [Space]
        [SerializeField] private bool isNetObject = true;
        public bool IsNetObject { get => isNetObject; set => isNetObject = value; }

        [Space]
        [SerializeField] private string objectID;
        public string ObjectID { get => objectID; set => objectID = value; }

        private void OnValidate()
        {
            Guid instanceID = GenerateGuidFromInt(GetInstanceID());

            if (string.IsNullOrEmpty(ObjectID) || objectID != instanceID.ToString())
            {
                //Genera guid
                ObjectID = instanceID.ToString();
            }

            _ = AnimationIdle;
            _ = AnimationWalk;
        }

        public static Guid GenerateGuidFromInt(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}
