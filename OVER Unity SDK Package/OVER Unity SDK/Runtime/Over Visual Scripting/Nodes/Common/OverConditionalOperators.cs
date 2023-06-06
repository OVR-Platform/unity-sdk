/**
 * OVR Unity SDK License
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
using System.Collections;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [Tags("Common")]
    public abstract class OverConditionOperation : OverNode
    {
    }

    // operators

    [Node(Path = "Operations/Conditional/Operator/Unary", Name = "Not", Icon = "OPERATIONS/CONDITIONAL")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverConditionNot : OverConditionOperation
    {
        [Input("a")] public bool a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return !_a;
        }
    }

    [Node(Path = "Operations/Conditional/Operator/Binary", Name = "And", Icon = "OPERATIONS/CONDITIONAL")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverConditionAnd : OverConditionOperation
    {
        [Input("a")] public bool a;
        [Input("b")] public bool b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            return _a && _b;
        }
    }

    [Node(Path = "Operations/Conditional/Operator/Binary", Name = "Or", Icon = "OPERATIONS/CONDITIONAL")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverConditionOr : OverConditionOperation
    {
        [Input("a")] public bool a;
        [Input("b")] public bool b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            return _a || _b;
        }
    }
    [Node(Path = "Operations/Conditional/Operator/Binary", Name = "XOr", Icon = "OPERATIONS/CONDITIONAL")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverConditionXOr : OverConditionOperation
    {
        [Input("a")] public bool a;
        [Input("b")] public bool b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            return _a ^ _b;
        }
    }

    // Compare

    [Node(Path = "Operations/Conditional/Comparator", Name = "Equal", Icon = "OPERATIONS/CONDITIONAL")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverConditionEquals : OverConditionOperation
    {
        [Input("a")] public object a;
        [Input("b")] public object b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            if (_a != null && _b != null && _a.GetType().IsAssignableFrom(_b.GetType()))
            {
                if (_a.GetType() == typeof(System.Single))
                    return (System.Single)_a == (System.Single)_b;

                if (_a.GetType() == typeof(bool))
                    return (bool)_a == (bool)_b;

                if (_a.GetType() == typeof(Vector2))
                    return (Vector2)_a == (Vector2)_b;

                if (_a.GetType() == typeof(Vector3))
                    return (Vector3)_a == (Vector3)_b;

                if (_a.GetType() == typeof(Quaternion))
                    return (Quaternion)_a == (Quaternion)_b;

                if (_a.GetType() == typeof(string))
                    return (string)_a == (string)_b;

                //else
                Comparer comparer = new Comparer(new System.Globalization.CultureInfo("en-US"));
                return comparer.Compare(_a, _b) == 0;
            }
            return false;
        }
    }

    [Node(Path = "Operations/Conditional/Comparator", Name = "Not Equal", Icon = "OPERATIONS/CONDITIONAL")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverConditionNotEquals : OverConditionOperation
    {
        [Input("a")] public object a;
        [Input("b")] public object b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            if (_a != null && _b != null && _a.GetType().IsAssignableFrom(_b.GetType()))
            {
                if (_a.GetType() == typeof(System.Single))
                    return (System.Single)_a != (System.Single)_b;

                if (_a.GetType() == typeof(bool))
                    return (bool)_a != (bool)_b;

                if (_a.GetType() == typeof(Vector2))
                    return (Vector2)_a != (Vector2)_b;

                if (_a.GetType() == typeof(Vector3))
                    return (Vector3)_a != (Vector3)_b;

                if (_a.GetType() == typeof(Quaternion))
                    return (Quaternion)_a != (Quaternion)_b;

                if (_a.GetType() == typeof(string))
                    return (string)_a != (string)_b;

                //else
                Comparer comparer = new Comparer(new System.Globalization.CultureInfo("en-US"));
                return comparer.Compare(_a, _b) != 0;
            }
            return false;
        }
    }

    //Numeric

    [Node(Path = "Operations/Conditional/Comparator", Name = "Greater Than", Icon = "OPERATIONS/CONDITIONAL")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverConditionGt : OverConditionOperation
    {
        [Input("a")] public object a;
        [Input("b")] public object b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            if (_a != null && _b != null && _a.GetType().IsAssignableFrom(_b.GetType()))
            {
                if (_a.GetType() == typeof(System.Single))
                    return (System.Single)_a > (System.Single)_b;

                //else
                Comparer comparer = new Comparer(new System.Globalization.CultureInfo("en-US"));
                return comparer.Compare(_a, _b) > 0;
            }

            return false;
        }
    }

    [Node(Path = "Operations/Conditional/Comparator", Name = "Greater Than Equal", Icon = "OPERATIONS/CONDITIONAL")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverConditionGte : OverConditionOperation
    {
        [Input("a")] public object a;
        [Input("b")] public object b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            if (_a != null && _b != null && _a.GetType().IsAssignableFrom(_b.GetType()))
            {
                if (_a.GetType() == typeof(System.Single))
                    return (System.Single)_a >= (System.Single)_b;

                //else
                Comparer comparer = new Comparer(new System.Globalization.CultureInfo("en-US"));
                return comparer.Compare(_a, _b) >= 0;
            }

            return false;
        }
    }

    [Node(Path = "Operations/Conditional/Comparator", Name = "Lesser Than Equal", Icon = "OPERATIONS/CONDITIONAL")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverConditionLte : OverConditionOperation
    {
        [Input("a")] public object a;
        [Input("b")] public object b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            if (_a != null && _b != null && _a.GetType().IsAssignableFrom(_b.GetType()))
            {
                if (_a.GetType() == typeof(System.Single))
                    return (System.Single)_a <= (System.Single)_b;

                //else
                Comparer comparer = new Comparer(new System.Globalization.CultureInfo("en-US"));
                return comparer.Compare(_a, _b) <= 0;
            }
            return false;
        }
    }

    [Node(Path = "Operations/Conditional/Comparator", Name = "Lesser Than", Icon = "OPERATIONS/CONDITIONAL")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverConditionLt : OverConditionOperation
    {
        [Input("a")] public object a;
        [Input("b")] public object b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            if (_a != null && _b != null && _a.GetType().IsAssignableFrom(_b.GetType()))
            {
                if (_a.GetType() == typeof(System.Single))
                    return (System.Single)_a < (System.Single)_b;

                //else
                Comparer comparer = new Comparer(new System.Globalization.CultureInfo("en-US"));
                return comparer.Compare(_a, _b) < 0;
            }

            return false;
        }
    }
}