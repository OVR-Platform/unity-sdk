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
using UnityEngine.Rendering;

namespace Over
{
    public enum OvrLightActionType { ChangeLightColor, UnityAction };

    public class OvrLight : OvrNode
    {
        public Light lightObject;
        public OvrLightActionType actionType;
        public Color lightColor = Color.white;

        //UnityEvent
        public UnityEvent unityAction;

        protected override void Execution()
        {
            switch (actionType)
            {
                case OvrLightActionType.ChangeLightColor:
                    if (lightObject == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
            }

            switch (actionType)
            {
                case OvrLightActionType.ChangeLightColor:
                    lightObject.color = lightColor;
                    break;
                case OvrLightActionType.UnityAction:
                    unityAction?.Invoke();
                    break;
            }
        }

        //public void AddCommandBuffer(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask) { lightObject.AddCommandBuffer(evt, buffer, shadowPassMask); }
        //public void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ComputeQueueType queueType) { lightObject.AddCommandBufferAsync(evt, buffer, queueType); }
        //public void GetCommandBuffers(LightEvent evt) { lightObject.GetCommandBuffers(evt); }
        //public void RemoveCommandBuffer(LightEvent evt, CommandBuffer buffer) { lightObject.RemoveCommandBuffer(evt, buffer); }
        //public void RemoveCommandBuffers(LightEvent evt) { lightObject.RemoveCommandBuffers(evt); }
    }
}