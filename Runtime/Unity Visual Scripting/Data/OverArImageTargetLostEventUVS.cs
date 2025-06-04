
using Unity.VisualScripting;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    //Registering a string name for your custom event to hook it to an event. You can save this class in a separated file and add multiple events to it as public static strings.
    public partial class EventNames
    {
        public static string OverArImageTargetLostEvent = "OverArImageTargetLostEvent";
    }

    [UnitTitle("On Ar Image Target Lost")]//Custom EventUnit to receive the event. Adding On to the unit title as an event naming convention.
    [UnitCategory("Over")]//Setting the path to find the unit in the fuzzy finder in Events > My Events.

    public class OverArImageTargetLostEventUVS : EventUnit<string>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput id { get; private set; }// The event output data to return when the event is triggered.
        protected override bool register => true;

        // Adding an EventHook with the name of the event to the list of visual scripting events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OverArImageTargetLostEvent);
        }
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            id = ValueOutput<string>(nameof(id));
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, string data)
        {
            flow.SetValue(id, data);
        }
    }
}
