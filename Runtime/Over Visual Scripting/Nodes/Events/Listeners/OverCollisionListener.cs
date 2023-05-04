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

using System;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    public class OverCollisionListener : MonoBehaviour
    {
        public Action<Collision> onCollisionEnter = delegate { };
        public Action<Collision> onCollisionExit = delegate { };
        public Action<Collision> onCollisionStay = delegate { };

        OverOnCollision node;

        public void Initialize(OverOnCollision node)
        {
            this.node = node;
        }
        private void OnDestroy()
        {
            if (node != null)
                node.Deregister();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (onCollisionEnter != null)
                onCollisionEnter(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (onCollisionExit != null)
                onCollisionExit(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (onCollisionStay != null)
                onCollisionStay(collision);
        }
    }
}