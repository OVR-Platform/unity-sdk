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

using BlueGraph;
using UnityEngine;
using UnityEngine.UIElements;

namespace OverSDK.VisualScripting.Editor
{
    [CustomNodeView(typeof(OverGroup))]
    [CustomNodeView(typeof(OverLoop))]
    [CustomNodeView(typeof(OverWhile))]
    [CustomNodeView(typeof(OverIfElse))]

    [CustomNodeView(typeof(OverAwake))]
    [CustomNodeView(typeof(OverStart))]
    [CustomNodeView(typeof(OverEnable))]
    [CustomNodeView(typeof(OverDisable))]
    [CustomNodeView(typeof(OverDestroy))]

    [CustomNodeView(typeof(OverUpdate))]
    [CustomNodeView(typeof(OverFixedUpdate))]
    [CustomNodeView(typeof(OverLateUpdate))]

    [CustomNodeView(typeof(OverCollider))]
    [CustomNodeView(typeof(OverEventNode))]
    [CustomNodeView(typeof(OverCustomEventTrigger))]
    public class OverFlowNodeView : OverExecutableNodeView
    {
        protected override void OnInitialize()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("OverFlowNodeView"));

            base.OnInitialize();

            AddToClassList("overFlowNodeView");
        }
    }
}