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
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [RequireComponent(typeof(Collider))]
    public class OverTouchListener : MonoBehaviour
    {
        public LayerMask layerMask;

        Collider mainCollider;

        public Action<RaycastHit> onClick = delegate { };
        //public Action<RaycastHit> onDoubleClick;

        public Action<RaycastHit> onPress = delegate { };
        public Action<RaycastHit> onLongPress = delegate { };

        RaycastHit hit;

        protected bool touched = false;
        protected float time = 0f;

        OverOnTouch node;

        private void OnDestroy()
        {
            if (node != null)
                node.Deregister();
        }

        public void Initialize(OverOnTouch node)
        {
            this.node = node;
            mainCollider = GetComponent<Collider>();
            if (mainCollider == null)
                mainCollider = gameObject.AddComponent<Collider>();
        }

        private void FixedUpdate()
        {
            if (node != null && Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosn = Camera.main.ScreenToWorldPoint(touch.position);

                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        if (hit.collider == mainCollider)
                        {
                            touched = true;
                            time += Time.fixedDeltaTime;
                        }
                    }
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    if (touched)
                    {
                        if (time < .5f)
                        {
                            if (onClick != null)
                                onClick.Invoke(hit);

                            if (onPress != null)
                                onPress.Invoke(hit);
                        }
                        else
                        {
                            if (onLongPress != null)
                                onLongPress.Invoke(hit);
                        }
                    }

                    touched = false;
                    time = 0f;
                }
            }
        }
    }
}