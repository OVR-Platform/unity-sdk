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
using Unity.VisualScripting;
using System;

namespace OverSDK.VisualScripting
{
    public partial class EventNames
    {
        //VPS Events
        public const string VPSStatusChangedEvent = "OverVPSStatusChangedEvent";
    }

    [UnitTitle("Over VPS")]
    [UnitCategory("OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OverVPSUVS : Unit
    {
        // ============================================
        // PORTS
        // ============================================

        [DoNotSerialize]
        [PortLabel("Restart")]
        public ControlInput RestartInput;

        [DoNotSerialize]
        [PortLabel("Output")]
        public ControlOutput RestartOutput;

        // ============================================
        // DELEGATES
        // ============================================

        public static Action RestartVPS = null;

        // ============================================
        // DEFINITION
        // ============================================

        protected override void Definition()
        {
            RestartInput = ControlInput(nameof(RestartInput), Restart);
            RestartOutput = ControlOutput(nameof(RestartOutput));

            Succession(RestartInput, RestartOutput);
        }

        // ============================================
        // METHODS
        // ============================================

        private ControlOutput Restart(Flow flow)
        {
#if !APP_MAIN
            Debug.LogWarning("VPS Restart is only available in the main app");
#else
            if (RestartVPS != null)
            {
                RestartVPS();
            }
            else
            {
                Debug.LogWarning("RestartVPS delegate is not assigned");
            }
#endif
            return RestartOutput;
        }
    }

    public enum VPSStatus
    {
        Disabled = 0,
        PreparingVPS = 1,
        RelocatingFirst = 2,
        Relocating = 3,
        Relocated = 4,
        Failed = 5,
        NoRelocData = 6,
        DataReady = 7,
    }

    [UnitTitle("VPS Status Changed")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class VPSStatusChangedEventUVS : EventUnit<int>
    {
        [DoNotSerialize]
        public ValueOutput statusInt { get; private set; }

        [DoNotSerialize]
        public ValueOutput statusEnum { get; private set; }

        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.VPSStatusChangedEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            statusInt = ValueOutput<int>(nameof(statusInt));
            statusEnum = ValueOutput<VPSStatus>(nameof(statusEnum));
        }

        protected override void AssignArguments(Flow flow, int data)
        {
            flow.SetValue(statusInt, data);
            flow.SetValue(statusEnum, (VPSStatus)data);
        }
    }
}

