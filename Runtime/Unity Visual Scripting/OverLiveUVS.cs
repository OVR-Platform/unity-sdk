using System;
using Unity.VisualScripting;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    public enum NetQosLevel
    {
        QOS_LEVEL_AT_MOST_ONCE,
        QOS_LEVEL_AT_LEAST_ONCE,
        QOS_LEVEL_EXACTLY_ONCE
    }

    public static class OverLiveUVS
    {
        public static Action<string, int> SendExtraMessage_Room;
        public static Action<byte[], int> SendExtraBytes_Room;
    }

    // ============================================
    // ROOM SEND ACTIONS
    // ============================================

    [UnitTitle("Send Custom Message")]
    [UnitCategory("OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OverSendExtraMessageRoomUVS : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput message;

        [DoNotSerialize]
        public ValueInput qosLevel;

        protected override void Definition()
        {
            message = ValueInput<string>("Message", "");
            qosLevel = ValueInput<NetQosLevel>("QoS Level", NetQosLevel.QOS_LEVEL_AT_MOST_ONCE);

            inputTrigger = ControlInput("", (flow) =>
            {
                string _message = flow.GetValue<string>(message);
                int _qosLevel = (int)flow.GetValue<NetQosLevel>(qosLevel);

                OverLiveUVS.SendExtraMessage_Room?.Invoke(_message, _qosLevel);

                return outputTrigger;
            });

            outputTrigger = ControlOutput("");
        }
    }

    [UnitTitle("Send Custom Bytes")]
    [UnitCategory("OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OverSendExtraBytesRoomUVS : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput bytes;

        [DoNotSerialize]
        public ValueInput qosLevel;

        protected override void Definition()
        {
            bytes = ValueInput<byte[]>("Bytes", null);
            qosLevel = ValueInput<NetQosLevel>("QoS Level", NetQosLevel.QOS_LEVEL_AT_MOST_ONCE);

            inputTrigger = ControlInput("", (flow) =>
            {
                byte[] _bytes = flow.GetValue<byte[]>(bytes);
                int _qosLevel = (int)flow.GetValue<NetQosLevel>(qosLevel);

                OverLiveUVS.SendExtraBytes_Room?.Invoke(_bytes, _qosLevel);

                return outputTrigger;
            });

            outputTrigger = ControlOutput("");
        }
    }
}
