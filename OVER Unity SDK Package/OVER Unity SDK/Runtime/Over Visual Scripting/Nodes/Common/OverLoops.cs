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

using BlueGraph;
using System.Collections;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [Node(Path = "Flow/Loops", Name = "Loop", Icon = "FLOW/LOOP")]
    [Tags("Common")]
    [Output("Body", typeof(OverExecutionFlowData), Multiple = false)]
    public class OverLoop : OverExecutionFlowNode
    {
        [Input("Count")] public System.Single count;
        [Output("Current Step")] private int currentStep;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            var count = GetInputValue("Count", this.count);

            // Execution does not leave this node until the loop completes
            IExecutableOverNode next = GetNextExecutableNode("Body");
            for (currentStep = 0; currentStep < (int)count; currentStep++)
            {
                (Graph as OverGraph).Execute(next, data);
            }

            return GetNextExecutableNode();
        }

        public override object OnRequestNodeValue(Port port)
        {
            if (port.Name == "Current Step")
            {
                return currentStep;
            }

            return base.OnRequestNodeValue(port);
        }

    }

    [Node(Path = "Flow/Loops", Name = "While", Icon = "FLOW/LOOP")]
    [Tags("Common")]
    [Output("Body", typeof(OverExecutionFlowData), Multiple = false)]
    public class OverWhile : OverExecutionFlowNode
    {
        [Input("Condition", Multiple = false)] public bool condition;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            var condition = GetInputValue("Condition", this.condition);

            // Execution does not leave this node until the loop completes
            IExecutableOverNode next = GetNextExecutableNode("Body");
            while (condition)
            {
                (Graph as OverGraph).Execute(next, data);
                condition = GetInputValue("Condition", this.condition);
            }

            return GetNextExecutableNode();
        }
    }

    [Node(Path = "Flow/Loops", Name = "List Iterate", Icon = "FLOW/LOOP")]
    [Tags("Common")]
    [Output("Body", typeof(OverExecutionFlowData), Multiple = false)]
    public class OverListIterate : OverExecutionFlowNode
    {
        [Input("List")] public IList list;
        [Output("Current Index")] private int index;
        [Output("Current Element")] private object element;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            var _list = GetInputValue("List", list);

            // Execution does not leave this node until the loop completes
            IExecutableOverNode next = GetNextExecutableNode("Body");
            for (index = 0; index < (int)_list.Count; index++)
            {
                element = _list[index];
                (Graph as OverGraph).Execute(next, data);
            }

            return GetNextExecutableNode();
        }

        public override object OnRequestNodeValue(Port port)
        {
            if (port.Name == "Current Index")
            {
                return index;
            }
            if (port.Name == "Current Element")
            {
                return element;
            }

            return base.OnRequestNodeValue(port);
        }
    }
}