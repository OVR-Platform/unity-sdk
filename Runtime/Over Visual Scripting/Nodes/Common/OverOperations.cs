/**
 * OVER Unity SDK License
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
    [Tags("Common")]
    public abstract class OverMathOperation : OverNode { }
    [Tags("Common")]
    public abstract class OverMathOperationHandlerNode : OverExecutionFlowNode { }

    //Const
    [Node(Path = "Operations/Math/Const", Name = "Pi", Icon = "OPERATIONS/MATH")]
    [Output("Value", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathPI : OverMathOperation
    {
        public override object OnRequestNodeValue(Port port)
        {
            return Mathf.PI;
        }
    }

    [Node(Path = "Operations/Math/Const", Name = "Epsilon", Icon = "OPERATIONS/MATH")]
    [Output("Value", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathEpsilon : OverMathOperation
    {
        public override object OnRequestNodeValue(Port port)
        {
            return Mathf.Epsilon;
        }
    }

    [Node(Path = "Operations/Math/Const", Name = "Infinity", Icon = "OPERATIONS/MATH")]
    [Output("Value", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathInfinity : OverMathOperation
    {
        public override object OnRequestNodeValue(Port port)
        {
            return Mathf.Infinity;
        }
    }

    [Node(Path = "Operations/Math/Const", Name = "Neg. Infinity", Icon = "OPERATIONS/MATH")]
    [Output("Value", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathNegInfinity : OverMathOperation
    {
        public override object OnRequestNodeValue(Port port)
        {
            return Mathf.NegativeInfinity;
        }
    }

    // Unary

    [Node(Path = "Operations/Math/Unary", Name = "Absolute", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathAbs : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return Mathf.Abs(_a);
        }
    }

    [Node(Path = "Operations/Math/Unary", Name = "Exp", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathExp : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return Mathf.Exp(_a);
        }
    }

    [Node(Path = "Operations/Math/Unary", Name = "Deg to Rad", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathDeg2Rad : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return _a * Mathf.Deg2Rad;
        }
    }

    [Node(Path = "Operations/Math/Unary", Name = "Rad to Deg", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathRad2Deg : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return _a * Mathf.Rad2Deg;
        }
    }

    // Binary
    [Node(Path = "Operations/Math/Binary", Name = "Random", Icon = "OPERATIONS/MATH")]
    public class OverMathRandom : OverMathOperationHandlerNode
    {
        [Input("Min")] public System.Single a;
        [Input("Max")] public System.Single b;

        [Output("Result", Multiple = true)] public System.Single result;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            var _a = GetInputValue("Min", a);
            var _b = GetInputValue("Max", b);

            result = Random.Range(_a, _b);

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Result": return result;
            }

            return base.OnRequestNodeValue(port);
        }
    }


    [Node(Path = "Operations/Math/Binary", Name = "Sum", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathSum : OverMathOperation
    {
        [Input("a")] public System.Single a;
        [Input("b")] public System.Single b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            return _a + _b;
        }
    }

    [Node(Path = "Operations/Math/Binary", Name = "Subtraction", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathSub : OverMathOperation
    {
        [Input("a")] public System.Single a;
        [Input("b")] public System.Single b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            return _a - _b;
        }
    }

    [Node(Path = "Operations/Math/Binary", Name = "Multiplication", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathMul : OverMathOperation
    {
        [Input("a")] public System.Single a;
        [Input("b")] public System.Single b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            return _a * _b;
        }
    }

    [Node(Path = "Operations/Math/Binary", Name = "Division", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathDiv : OverMathOperation
    {
        [Input("a")] public System.Single a;
        [Input("b")] public System.Single b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            return _a / _b;
        }
    }

    [Node(Path = "Operations/Math/Binary", Name = "Module", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathMod : OverMathOperation
    {
        [Input("a")] public System.Single a;
        [Input("b")] public System.Single b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            return _a % _b;
        }
    }

    [Node(Path = "Operations/Math/Binary", Name = "Power", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathPow : OverMathOperation
    {
        [Input("a")] public System.Single a;
        [Input("n")] public System.Single n;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _n = GetInputValue("n", n);

            return Mathf.Pow(_a, _n);
        }
    }

    //Clamp

    [Node(Path = "Operations/Math/Clamp", Name = "Min", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathMin : OverMathOperation
    {
        [Input("a")] public System.Single a;
        [Input("b")] public System.Single b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            return Mathf.Min(_a, _b);
        }
    }

    [Node(Path = "Operations/Math/Clamp", Name = "Max", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathMax : OverMathOperation
    {
        [Input("a")] public System.Single a;
        [Input("b")] public System.Single b;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _b = GetInputValue("b", b);

            return Mathf.Max(_a, _b);
        }
    }

    [Node(Path = "Operations/Math/Clamp", Name = "Floor", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathFloor : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return Mathf.Floor(_a);
        }
    }

    [Node(Path = "Operations/Math/Clamp", Name = "Ceil", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathCeil : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return Mathf.Ceil(_a);
        }
    }


    [Node(Path = "Operations/Math/Clamp", Name = "Clamp", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathClamp : OverMathOperation
    {
        [Input("a")] public System.Single a;
        [Input("min")] public System.Single min;
        [Input("max")] public System.Single max;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);
            var _min = GetInputValue("min", min);
            var _max = GetInputValue("max", max);

            return Mathf.Clamp(_a, _min, _max);
        }
    }

    // Trigonometry

    [Node(Path = "Operations/Math/Trigonometry", Name = "Sin", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathSin : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return Mathf.Sin(_a);
        }
    }

    [Node(Path = "Operations/Math/Trigonometry", Name = "Cos", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathCos : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return Mathf.Cos(_a);
        }
    }

    [Node(Path = "Operations/Math/Trigonometry", Name = "Tan", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathTan : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return Mathf.Tan(_a);
        }
    }

    [Node(Path = "Operations/Math/Trigonometry", Name = "ArcSin", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathArcSin : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return Mathf.Asin(_a);
        }
    }

    [Node(Path = "Operations/Math/Trigonometry", Name = "ArcCos", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathArcCos : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return Mathf.Acos(_a);
        }
    }

    [Node(Path = "Operations/Math/Trigonometry", Name = "ArcTan", Icon = "OPERATIONS/MATH")]
    [Output("Result", Type = (typeof(System.Single)), Multiple = true)]
    public class OverMathArcTan : OverMathOperation
    {
        [Input("a")] public System.Single a;

        public override object OnRequestNodeValue(Port port)
        {
            var _a = GetInputValue("a", a);

            return Mathf.Atan(_a);
        }
    }
}
