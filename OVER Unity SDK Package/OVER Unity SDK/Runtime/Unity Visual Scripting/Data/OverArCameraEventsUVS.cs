using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace OverSDK.VisualScripting
{
    public partial class EventNames
    {
        //Camera Events
        public const string ArCameraTeleportedEvent = "OverArCameraTeleportedEvent";
    }

    public class OverArCameraEventsUVS
    {

    }

    [UnitTitle("AR Camera Teleported")]
    [UnitCategory("Events/OVER")]   
    [TypeIcon(typeof(OverBaseType))]
    public class ARCameraTeleportedEventUVS : EventUnit<EmptyEventArgs>
    {
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.ArCameraTeleportedEvent);
        }

        protected override void Definition()
        {
            base.Definition();
        }
    }
}
