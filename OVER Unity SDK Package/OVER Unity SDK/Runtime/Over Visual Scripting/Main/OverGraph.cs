/**
 * OVR Unity SDK License
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

using BlueGraph;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace OverSDK.VisualScripting
{
    public class OverExecutionFlowData
    {

    }

    [CreateAssetMenu(
        menuName = "Over SDK/OverGraph",
        fileName = "New OverGraph"
    )]
    [IncludeTags("Executable", "Monobehaviour", "Component", "Variable", "Data", "Common", "Event", "Utils", "Log")]
    public class OverGraph : Graph, IExecutableOverGraph
    {
        [SerializeField, HideInInspector] public string GUID;
        [SerializeField] public string GraphName => name;

        OverUpdate overUpdate;
        OverFixedUpdate overFixedUpdate;
        OverLateUpdate overLateUpdate;

        // OVERRIDE
        public override string Title => GraphName;

        // MonoBehaviour Nodes
        public void OnBehaviourAwake()
        {
            overUpdate = GetNode<OverUpdate>();
            overLateUpdate = GetNode<OverLateUpdate>();
            overFixedUpdate = GetNode<OverFixedUpdate>();
            List<OverEventNode> nodes = GetGraphNodes<OverEventNode>();
            foreach (var node in nodes)
            {
                node.Register();
            }

            Execute(GetNode<OverAwake>(), new OverExecutionFlowData
            {

            });
        }

        public List<T> GetGraphNodes<T>() where T : Node
        {
            return GetNodes<T>().ToList();
        }

        public void OnBehaviourStart()
        {
            Execute(GetNode<OverStart>(), new OverExecutionFlowData
            {

            });
        }

        public void OnBehaviourEnable()
        {
            Execute(GetNode<OverEnable>(), new OverExecutionFlowData
            {

            });
        }

        public void OnBehaviourDisable()
        {
            Execute(GetNode<OverDisable>(), new OverExecutionFlowData
            {

            });
        }

        public void OnBehaviourDestroy()
        {
            Execute(GetNode<OverDestroy>(), new OverExecutionFlowData
            {

            });
        }

        public void OnBehaviourUpdate()
        {
            Execute(overUpdate, new OverExecutionFlowData
            {

            });
        }
        public void OnBehaviourLateUpdate()
        {
            Execute(overLateUpdate, new OverExecutionFlowData
            {

            });
        }
        public void OnBehaviourFixedUpdate()
        {
            Execute(overFixedUpdate, new OverExecutionFlowData
            {

            });
        }

        //Interface

        public void Execute(IExecutableOverNode root, OverExecutionFlowData data)
        {
            IExecutableOverNode next = root;
            int iterations = 0;
            while (next != null)
            {
                next = next.Execute(data);

                iterations++;
                if (iterations > 2000)
                {
                    Debug.LogError("Potential infinite loop detected. Stopping early.", this);
                    break;
                }
            }
        }
    }
}