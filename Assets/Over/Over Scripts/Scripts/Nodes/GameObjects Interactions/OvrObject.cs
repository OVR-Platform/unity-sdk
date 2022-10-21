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

using System.Collections.Generic;
using UnityEngine;

namespace Over
{
    public enum OvrMonoBehaviourMethod { Awake, OnDestroy, OnDisable, OnEnable, Start }

    public class OvrObject : OvrNode
    {
        [SerializeField]
        [ReadOnly]
        protected OvrMonoBehaviourMethod lastOvrMonoBehaviourMethod;

        [OvrNodeList]
        public List<OvrNode> onAwakeNodes = new List<OvrNode>();
        [OvrNodeList]
        public List<OvrNode> onDestroyNodes = new List<OvrNode>();
        [OvrNodeList]
        public List<OvrNode> onDisableNodes = new List<OvrNode>();
        [OvrNodeList]
        public List<OvrNode> onEnableNodes = new List<OvrNode>();
        [OvrNodeList]
        public List<OvrNode> onStartNodes = new List<OvrNode>();

        protected void Awake()
        {
            lastOvrMonoBehaviourMethod = OvrMonoBehaviourMethod.Awake;
            Execute();
        }

        protected void OnDestroy()
        {
            lastOvrMonoBehaviourMethod = OvrMonoBehaviourMethod.OnDisable;
            Execute();
        }

        protected void OnDisable()
        {
            lastOvrMonoBehaviourMethod = OvrMonoBehaviourMethod.OnDisable;
            Execute();
        }

        protected void OnEnable()
        {
            lastOvrMonoBehaviourMethod = OvrMonoBehaviourMethod.OnEnable;
            Execute();
        }

        protected void Start()
        {
            lastOvrMonoBehaviourMethod = OvrMonoBehaviourMethod.Start;
            Execute();
        }      

        protected override void Execution()
        {
            switch (lastOvrMonoBehaviourMethod)
            {
                case OvrMonoBehaviourMethod.Awake:
                    foreach (var node in onAwakeNodes)
                    {
                        if (node != null)
                            node.Execute();
                        else if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }
                    break;
                case OvrMonoBehaviourMethod.OnDestroy:
                    foreach (var node in onDestroyNodes)
                    {
                        if (node != null)
                            node.Execute();
                        else if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }
                    break;
                case OvrMonoBehaviourMethod.OnDisable:
                    foreach (var node in onDisableNodes)
                    {
                        if (node != null)
                            node.Execute();
                        else if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }
                    break;
                case OvrMonoBehaviourMethod.OnEnable:
                    foreach (var node in onEnableNodes)
                    {
                        if (node != null)
                            node.Execute();
                        else if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }
                    break;
                case OvrMonoBehaviourMethod.Start:
                    foreach (var node in onStartNodes)
                    {
                        if (node != null)
                            node.Execute();
                        else if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }
                    break;
            }
        }
    }
}