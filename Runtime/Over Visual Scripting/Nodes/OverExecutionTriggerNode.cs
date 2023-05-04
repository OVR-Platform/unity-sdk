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
using System.Linq;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [Output("ExecOut", typeof(OverExecutionFlowData), Multiple = false)]
    public abstract class OverExecutionTriggerNode : OverNode, IExecutableOverNode
    {
        /// <summary>
        /// Execute this node and return the next node to be executed.
        /// Override with your custom execution logic. 
        /// </summary>
        /// <returns></returns>
        public virtual IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            // noop.
            return GetNextExecutableNode();
        }

        /// <summary>
        /// Get the next node that should be executed along the edge
        /// </summary>
        /// <returns></returns>
        public IExecutableOverNode GetNextExecutableNode(string portName = "ExecOut")
        {
            var port = GetPort(portName);

            if (port == null)
                return null;

            if (port.ConnectionCount < 1)
            {
                return null;
            }

            //? get the first port 
            var node = port.ConnectedPorts.First()?.Node;

            if (node is IExecutableOverNode execNode)
            {
                return execNode;
            }

            Debug.LogWarning(
                $"<b>[{Name}]</b> Connected output node {node.Name} to port {port.Name} is not an ICanExec. " +
                $"Cannot execute past this point."
            );

            return null;

        }
    }
}