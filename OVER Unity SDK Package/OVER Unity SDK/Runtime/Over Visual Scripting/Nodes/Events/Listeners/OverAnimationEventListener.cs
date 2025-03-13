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

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    public class OverAnimationEvent : MonoBehaviour
    {
        public event Action<string> OnEventFired = delegate { };

        private Animator animator;

        private Dictionary<string, OverOnAnimationEvent> animationEvents = new Dictionary<string, OverOnAnimationEvent>();


        // TestFunction

        private void Awake()
        {
            if (!gameObject.TryGetComponent(out animator))
                animator = gameObject.AddComponent<Animator>();
        }

        private void OnDestroy()
        {
            foreach (var node in animationEvents)
            {
                node.Value.Deregister();
            }
        }

        public void Initialize(OverOnAnimationEvent node, string eventName, out string newGuid)
        {
            newGuid = Guid.NewGuid().ToString();

            animationEvents.Add(newGuid, node);

            ModifyAnimationEvent(eventName, newGuid);
        }

        void ModifyAnimationEvent(string eventName, string eventGuid)
        {
            RuntimeAnimatorController controller = animator.runtimeAnimatorController;

            foreach (AnimationClip clip in controller.animationClips)
            {
                if (EditEvent(clip, eventName, "InternalEventHandler", eventGuid))
                {
                    break;
                }
            }
        }

        public void InternalEventHandler(string eventId)
        {
            OnEventFired?.Invoke(eventId);
        }

        bool EditEvent(AnimationClip clip, string eventName, string newFunctionName, string newGuid)
        {
            // Get the current events of the clip
            AnimationEvent[] events = clip.events;
            List<AnimationEvent> modifiedEvents = new List<AnimationEvent>(events);

            bool hasModified = false;
            foreach (AnimationEvent evt in events)
            {
                if (evt.functionName == eventName)
                {
                    // Create a copy of the event
                    AnimationEvent newEvent = new AnimationEvent()
                    {
                        functionName = newFunctionName,
                        time = evt.time,
                        stringParameter = newGuid,
                        messageOptions = SendMessageOptions.RequireReceiver
                    };

                    // Add the new event to the clip
                    modifiedEvents.Add(newEvent);

                    // Silence the original event
                    evt.messageOptions = SendMessageOptions.DontRequireReceiver;

                    hasModified = true;
                }
            }

            // Set the modified events array back to the clip
            clip.events = modifiedEvents.ToArray();

            return hasModified;
        }
    }
}
