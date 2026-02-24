using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace OverSDK.VisualScripting
{
    public partial class EventNames
    {
        //One Touch
        public const string OneTouchEnterEvent = "OverOneTouchEnterEvent";
        public const string OneTouchEnterOverUIEvent = "OverOneTouchEnterOverUIEvent";
        public const string OneTouchEnterNotOverUIEvent = "OverOneTouchEnterNotOverUIEvent";
        public const string OneTouchExitEvent = "OverOneTouchExitEvent";
        public const string OneTouchInEvent = "OverOneTouchInEvent";
        public const string OneTouchInNotOverUIEvent = "OverOneTouchInNotOverUIEvent";
        public const string OneTouchInOverUIEvent = "OverOneTouchInOverUIEvent";

        //Two Touches
        public const string TwoTouchesEnterEvent = "OverTwoTouchesEnterEvent";
        public const string TwoTouchesEnterNotOverUIEvent = "OverTwoTouchesEnterNotOverUIEvent";
        public const string TwoTouchesEnterOverUIEvent = "OverTwoTouchesEnterOverUIEvent";
        public const string TwoTouchesExitEvent = "OverTwoTouchesExitEvent";
        public const string TwoTouchesInEvent = "OverTwoTouchesInEvent";
        public const string TwoTouchesInNotOverUIEvent = "OverTwoTouchesInNotOverUIEvent";
        public const string TwoTouchesInOverUIEvent = "OverTwoTouchesInOverUIEvent";

        //Click
        public const string ClickOnScreenEvent = "OverClickOnScreenEvent";
        public const string ClickOnNotUIScreenEvent = "OverClickOnNotUIScreenEvent";

        //Double Click
        public const string DoubleClickOnScreenEvent = "OverDoubleClickOnScreenEvent";
        public const string DoubleClickOnNotUIScreenEvent = "OverDoubleClickOnNotUIScreenEvent";
    }

    public class OverTouchEventsUVS 
    {
      
    }

    // ============================================
    // ONE TOUCH EVENTS
    // ============================================

    [UnitTitle("One Touch Enter Event")]
    [UnitCategory("Events/OVER")]   
    [TypeIcon(typeof(OverBaseType))]
    public class OneTouchEnterEventUVS : EventUnit<Vector2>
    {
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OneTouchEnterEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            new0 = ValueOutput<Vector2>(nameof(new0));
        }

        protected override void AssignArguments(Flow flow, Vector2 data)
        {
            flow.SetValue(new0, data);
        }
    }

    [UnitTitle("One Touch Enter Over UI Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OneTouchEnterOverUIEventUVS : EventUnit<Vector2>
    {
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OneTouchEnterOverUIEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            new0 = ValueOutput<Vector2>(nameof(new0));
        }

        protected override void AssignArguments(Flow flow, Vector2 data)
        {
            flow.SetValue(new0, data);
        }
    }

    [UnitTitle("One Touch Enter Not Over UI Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OneTouchEnterNotOverUIEventUVS : EventUnit<Vector2>
    {
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OneTouchEnterNotOverUIEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            new0 = ValueOutput<Vector2>(nameof(new0));
        }

        protected override void AssignArguments(Flow flow, Vector2 data)
        {
            flow.SetValue(new0, data);
        }
    }

    [UnitTitle("One Touch Exit Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OneTouchExitEventUVS : EventUnit<Vector2>
    {
        [DoNotSerialize]
        public ValueOutput old0 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OneTouchExitEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            old0 = ValueOutput<Vector2>(nameof(old0));
        }

        protected override void AssignArguments(Flow flow, Vector2 data)
        {
            flow.SetValue(old0, data);
        }
    }

    [UnitTitle("One Touch In Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OneTouchInEventUVS : EventUnit<(Vector2, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput old0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OneTouchInEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            old0 = ValueOutput<Vector2>(nameof(old0));
            new0 = ValueOutput<Vector2>(nameof(new0));
        }

        protected override void AssignArguments(Flow flow, (Vector2, Vector2) data)
        {
            flow.SetValue(old0, data.Item1);
            flow.SetValue(new0, data.Item2);
        }
    }

    [UnitTitle("One Touch In Not Over UI Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OneTouchInNotOverUIEventUVS : EventUnit<(Vector2, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput old0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OneTouchInNotOverUIEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            old0 = ValueOutput<Vector2>(nameof(old0));
            new0 = ValueOutput<Vector2>(nameof(new0));
        }

        protected override void AssignArguments(Flow flow, (Vector2, Vector2) data)
        {
            flow.SetValue(old0, data.Item1);
            flow.SetValue(new0, data.Item2);
        }
    }

    [UnitTitle("One Touch In Over UI Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class OneTouchInOverUIEventUVS : EventUnit<(Vector2, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput old0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OneTouchInOverUIEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            old0 = ValueOutput<Vector2>(nameof(old0));
            new0 = ValueOutput<Vector2>(nameof(new0));
        }

        protected override void AssignArguments(Flow flow, (Vector2, Vector2) data)
        {
            flow.SetValue(old0, data.Item1);
            flow.SetValue(new0, data.Item2);
        }
    }

    // ============================================
    // TWO TOUCHES EVENTS
    // ============================================

    [UnitTitle("Two Touches Enter Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class TwoTouchesEnterEventUVS : EventUnit<(Vector2, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new1 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.TwoTouchesEnterEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            new0 = ValueOutput<Vector2>(nameof(new0));
            new1 = ValueOutput<Vector2>(nameof(new1));
        }

        protected override void AssignArguments(Flow flow, (Vector2, Vector2) data)
        {
            flow.SetValue(new0, data.Item1);
            flow.SetValue(new1, data.Item2);
        }
    }

    [UnitTitle("Two Touches Enter Not Over UI Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class TwoTouchesEnterNotOverUIEventUVS : EventUnit<(Vector2, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new1 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.TwoTouchesEnterNotOverUIEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            new0 = ValueOutput<Vector2>(nameof(new0));
            new1 = ValueOutput<Vector2>(nameof(new1));
        }

        protected override void AssignArguments(Flow flow, (Vector2, Vector2) data)
        {
            flow.SetValue(new0, data.Item1);
            flow.SetValue(new1, data.Item2);
        }
    }

    [UnitTitle("Two Touches Enter Over UI Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class TwoTouchesEnterOverUIEventUVS : EventUnit<(Vector2, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new1 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.TwoTouchesEnterOverUIEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            new0 = ValueOutput<Vector2>(nameof(new0));
            new1 = ValueOutput<Vector2>(nameof(new1));
        }

        protected override void AssignArguments(Flow flow, (Vector2, Vector2) data)
        {
            flow.SetValue(new0, data.Item1);
            flow.SetValue(new1, data.Item2);
        }
    }

    [UnitTitle("Two Touches Exit Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class TwoTouchesExitEventUVS : EventUnit<(Vector2, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput old0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput old1 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.TwoTouchesExitEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            old0 = ValueOutput<Vector2>(nameof(old0));
            old1 = ValueOutput<Vector2>(nameof(old1));
        }

        protected override void AssignArguments(Flow flow, (Vector2, Vector2) data)
        {
            flow.SetValue(old0, data.Item1);
            flow.SetValue(old1, data.Item2);
        }
    }

    [UnitTitle("Two Touches In Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class TwoTouchesInEventUVS : EventUnit<(Vector2, Vector2, Vector2, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput old0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput old1 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new1 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.TwoTouchesInEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            old0 = ValueOutput<Vector2>(nameof(old0));
            old1 = ValueOutput<Vector2>(nameof(old1));
            new0 = ValueOutput<Vector2>(nameof(new0));
            new1 = ValueOutput<Vector2>(nameof(new1));
        }

        protected override void AssignArguments(Flow flow, (Vector2, Vector2, Vector2, Vector2) data)
        {
            flow.SetValue(old0, data.Item1);
            flow.SetValue(old1, data.Item2);
            flow.SetValue(new0, data.Item3);
            flow.SetValue(new1, data.Item4);
        }
    }

    [UnitTitle("Two Touches In Not Over UI Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class TwoTouchesInNotOverUIEventUVS : EventUnit<(Vector2, Vector2, Vector2, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput old0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput old1 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new1 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.TwoTouchesInNotOverUIEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            old0 = ValueOutput<Vector2>(nameof(old0));
            old1 = ValueOutput<Vector2>(nameof(old1));
            new0 = ValueOutput<Vector2>(nameof(new0));
            new1 = ValueOutput<Vector2>(nameof(new1));
        }

        protected override void AssignArguments(Flow flow, (Vector2, Vector2, Vector2, Vector2) data)
        {
            flow.SetValue(old0, data.Item1);
            flow.SetValue(old1, data.Item2);
            flow.SetValue(new0, data.Item3);
            flow.SetValue(new1, data.Item4);
        }
    }

    [UnitTitle("Two Touches In Over UI Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class TwoTouchesInOverUIEventUVS : EventUnit<(Vector2, Vector2, Vector2, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput old0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput old1 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new0 { get; private set; }
        [DoNotSerialize]
        public ValueOutput new1 { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.TwoTouchesInOverUIEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            old0 = ValueOutput<Vector2>(nameof(old0));
            old1 = ValueOutput<Vector2>(nameof(old1));
            new0 = ValueOutput<Vector2>(nameof(new0));
            new1 = ValueOutput<Vector2>(nameof(new1));
        }

        protected override void AssignArguments(Flow flow, (Vector2, Vector2, Vector2, Vector2) data)
        {
            flow.SetValue(old0, data.Item1);
            flow.SetValue(old1, data.Item2);
            flow.SetValue(new0, data.Item3);
            flow.SetValue(new1, data.Item4);
        }
    }

    // ============================================
    // CLICK EVENTS
    // ============================================

    [UnitTitle("Click On Screen Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class ClickOnScreenEventUVS : EventUnit<(RaycastHit, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput raycastHit { get; private set; }
        [DoNotSerialize]
        public ValueOutput position { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.ClickOnScreenEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            raycastHit = ValueOutput<RaycastHit>(nameof(raycastHit));
            position = ValueOutput<Vector2>(nameof(position));
        }

        protected override void AssignArguments(Flow flow, (RaycastHit, Vector2) data)
        {
            flow.SetValue(raycastHit, data.Item1);
            flow.SetValue(position, data.Item2);
        }
    }

    [UnitTitle("Click On Not UI Screen Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class ClickOnNotUIScreenEventUVS : EventUnit<(RaycastHit, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput raycastHit { get; private set; }
        [DoNotSerialize]
        public ValueOutput position { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.ClickOnNotUIScreenEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            raycastHit = ValueOutput<RaycastHit>(nameof(raycastHit));
            position = ValueOutput<Vector2>(nameof(position));
        }

        protected override void AssignArguments(Flow flow, (RaycastHit, Vector2) data)
        {
            flow.SetValue(raycastHit, data.Item1);
            flow.SetValue(position, data.Item2);
        }
    }

    // ============================================
    // DOUBLE CLICK EVENTS
    // ============================================

    [UnitTitle("Double Click On Screen Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class DoubleClickOnScreenEventUVS : EventUnit<(RaycastHit, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput raycastHit { get; private set; }
        [DoNotSerialize]
        public ValueOutput position { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.DoubleClickOnScreenEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            raycastHit = ValueOutput<RaycastHit>(nameof(raycastHit));
            position = ValueOutput<Vector2>(nameof(position));
        }

        protected override void AssignArguments(Flow flow, (RaycastHit, Vector2) data)
        {
            flow.SetValue(raycastHit, data.Item1);
            flow.SetValue(position, data.Item2);
        }
    }

    [UnitTitle("Double Click On Not UI Screen Event")]
    [UnitCategory("Events/OVER")]
    [TypeIcon(typeof(OverBaseType))]
    public class DoubleClickOnNotUIScreenEventUVS : EventUnit<(RaycastHit, Vector2)>
    {
        [DoNotSerialize]
        public ValueOutput raycastHit { get; private set; }
        [DoNotSerialize]
        public ValueOutput position { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.DoubleClickOnNotUIScreenEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            raycastHit = ValueOutput<RaycastHit>(nameof(raycastHit));
            position = ValueOutput<Vector2>(nameof(position));
        }

        protected override void AssignArguments(Flow flow, (RaycastHit, Vector2) data)
        {
            flow.SetValue(raycastHit, data.Item1);
            flow.SetValue(position, data.Item2);
        }
    }
}
