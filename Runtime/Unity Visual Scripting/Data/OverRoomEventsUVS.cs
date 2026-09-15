using Unity.VisualScripting;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    public partial class EventNames
    {
        //Room Events
        public const string ExtraMessageReceivedRoomEvent = "ExtraMessageReceivedRoomEvent";
        public const string ExtraBytesReceivedRoomEvent = "ExtraBytesReceivedRoomEvent";
    }

    public class OverRoomEventsUVS
    {

    }

    // ============================================
    // ROOM EVENTS
    // ============================================

    [UnitTitle("On Extra Message Received Room")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OverExtraMessageReceivedRoomEventUVS : EventUnit<(string senderId, string message)>
    {
        [DoNotSerialize]
        public ValueOutput senderId { get; private set; }

        [DoNotSerialize]
        public ValueOutput message { get; private set; }

        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.ExtraMessageReceivedRoomEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            senderId = ValueOutput<string>(nameof(senderId));
            message = ValueOutput<string>(nameof(message));
        }

        protected override void AssignArguments(Flow flow, (string senderId, string message) data)
        {
            flow.SetValue(senderId, data.senderId);
            flow.SetValue(message, data.message);
        }
    }

    [UnitTitle("On Extra Bytes Received Room")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OverExtraBytesReceivedRoomEventUVS : EventUnit<(string senderId, byte[] bytes)>
    {
        [DoNotSerialize]
        public ValueOutput senderId { get; private set; }

        [DoNotSerialize]
        public ValueOutput bytes { get; private set; }

        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.ExtraBytesReceivedRoomEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            senderId = ValueOutput<string>(nameof(senderId));
            bytes = ValueOutput<byte[]>(nameof(bytes));
        }

        protected override void AssignArguments(Flow flow, (string senderId, byte[] bytes) data)
        {
            flow.SetValue(senderId, data.senderId);
            flow.SetValue(bytes, data.bytes);
        }
    }
}
