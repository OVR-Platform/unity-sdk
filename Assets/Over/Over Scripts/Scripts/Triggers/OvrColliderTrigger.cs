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
using System.Collections.Generic;

namespace Over
{
    public enum TriggerState { Enter, Stay, Exit }

    [RequireComponent(typeof(Collider))]
    public class OvrColliderTrigger : OvrNode
    {
        //Tags
        public bool interactWithUserCamera = true;
        public List<string> consideredSceneObjectNames = new List<string>();

        [SerializeField] 
        [ReadOnly]
        protected TriggerState lastTriggerState;

        [OvrNodeList]
        public List<OvrNode> triggerEnterNodes = new List<OvrNode>();
        [OvrNodeList]
        public List<OvrNode> triggerStayNodes = new List<OvrNode>();
        [OvrNodeList]
        public List<OvrNode> triggerExitNodes = new List<OvrNode>();

        public void OnTriggerEnter(Collider other)
        {
            if (other != null && ((interactWithUserCamera && other.tag == OvrConst.PLAYER_CAMERA_TAG) || consideredSceneObjectNames.Contains(other.gameObject.name)))
            {
                lastTriggerState = TriggerState.Enter;
                Execute();
            }
        }

        public void OnTriggerStay(Collider other)
        {
            if (other != null && ((interactWithUserCamera && other.tag == OvrConst.PLAYER_CAMERA_TAG) || consideredSceneObjectNames.Contains(other.gameObject.name)))
            {
                lastTriggerState = TriggerState.Stay;
                Execute();
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other != null && ((interactWithUserCamera && other.tag == OvrConst.PLAYER_CAMERA_TAG) || consideredSceneObjectNames.Contains(other.gameObject.name)))
            {
                lastTriggerState = TriggerState.Exit;
                Execute();
            }
        }

        protected override void Execution()
        {
            switch (lastTriggerState)
            {
                case TriggerState.Enter:
                    foreach (var node in triggerEnterNodes)
                    {
                        if (node != null)
                            node.Execute();
                        else if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }
                    break;
                case TriggerState.Stay:
                    foreach (var node in triggerStayNodes)
                    {
                        if (node != null)
                            node.Execute();
                        else if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }                   
                    break;
                case TriggerState.Exit:
                    foreach (var node in triggerExitNodes)
                    {
                        if (node != null)
                            node.Execute();
                        else if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }
                    break;
                default:
                    break;
            }
        }

        public override void AddNode(OvrNode ovrNode)
        {
            triggerEnterNodes.Add(ovrNode);
        }
    }
}