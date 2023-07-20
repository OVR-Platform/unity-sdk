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

namespace OverSDK.VisualScripting
{
    public enum OverTimeType { DeltaTime, Time, Fixed, FixedDeltaTime }

    [Node(Path = "Utils/Time", Name = "Time", Icon = "Utils/TIME")]
    [Tags("Utils")]
    [Output("Value", typeof(float), Multiple = true)]
    public class OverTime : OverNode
    {
        [Editable("Time")] public OverTimeType timeType = OverTimeType.DeltaTime;

        public override object OnRequestNodeValue(Port port)
        {
            switch (timeType)
            {
                case OverTimeType.DeltaTime: return Time.deltaTime;
                case OverTimeType.FixedDeltaTime: return Time.fixedDeltaTime;
                case OverTimeType.Time: return Time.time;
                case OverTimeType.Fixed: return Time.fixedTime;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Tags("Utils")]
    public class OverTimeHandler : OverExecutionFlowNode { }

    [Node(Path = "Utils/Time", Name = "Countdown Timer", Icon = "Utils/TIME")]
    [Output("On Time Still Remaining", typeof(OverExecutionFlowData), Multiple = false)]
    public class OverCountdown : OverTimeHandler
    {
        [Input("Seconds")] public float seconds;

        [Output("Time Remaining")] private float timeRemaining;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            float _seconds = GetInputValue("Seconds", seconds);
            IExecutableOverNode timeStillRemaining = GetNextExecutableNode("On Time Still Remaining");

            float _secondsUpdated = _seconds - Time.deltaTime;
            if (_secondsUpdated > 0)
            {
                timeRemaining = _secondsUpdated;
                (Graph as OverGraph).Execute(timeStillRemaining, data);
                return null;
            }
            
            _secondsUpdated = 0;
            timeRemaining = _secondsUpdated;
            (Graph as OverGraph).Execute(timeStillRemaining, data);
            return GetNextExecutableNode();
        }

        public override object OnRequestValue(Port port)
        {
            if (port.Name == "Time Remaining")
            {
                return timeRemaining;
            }

            return base.OnRequestValue(port);
        }
    }
}