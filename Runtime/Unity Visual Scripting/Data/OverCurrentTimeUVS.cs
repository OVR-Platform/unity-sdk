using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [UnitTitle("Get Current Datetime")]
    [UnitCategory("Over")]
    [TypeIcon(typeof(OverBaseType))]
    public class OverCurrentTimeUVS : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueOutput value;
        protected override void Definition()
        {
            value = ValueOutput<DateTime>("value", (flow) => DateTime.Now);
        }
    }
}
