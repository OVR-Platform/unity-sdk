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
    public abstract class OverVectorOperation : OverNode { }

    [Node(Path = "Operations/Vector", Icon = "OPERATIONS/VECTOR", Name = "Vector Exposer")]
    public class OverVectorExposer : OverVectorOperation
    {
        [Input("Vector")] public Vector3 vec;

        [Output("x")] public float x;
        [Output("y")] public float y;
        [Output("z")] public float z;

        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _vec = GetInputValue("Vector", vec);

            if (port.Name == "x")
            {
                x = _vec.x;
                return x;
            }
            if (port.Name == "y")
            {
                y = _vec.y;
                return y;
            }
            if (port.Name == "z")
            {
                z = _vec.z;
                return z;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    public enum VectorAngleMode { Unsigned, Signed, FullDegrees }

    [Node(Path = "Operations/Vector/Handlers/Base", Name = "Angle", Icon = "OPERATIONS/VECTOR")]
    [Output("Angle", Multiple = true, Type = typeof(float))]
    public class OverVectorAngle : OverVectorOperation
    {
        [Input("From")] public Vector3 from;
        [Input("To")] public Vector3 to;
        [Input("Axis")] public Vector3 axis;

        [Editable("Mode")] public VectorAngleMode vectorAngleMode;
        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _from = GetInputValue("From", from);
            Vector3 _to = GetInputValue("To", to);
            Vector3 _axis = GetInputValue("Axis", axis);

            switch (vectorAngleMode)
            {
                case VectorAngleMode.Unsigned: return Vector3.Angle(_from, _to);
                case VectorAngleMode.Signed: return Vector3.SignedAngle(_from, _to, _axis);
                case VectorAngleMode.FullDegrees: 
                    float angle = Vector3.SignedAngle(_from, _to, _axis);
                    return angle < 0 ? angle + 360f : angle;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Base", Name = "Distance", Icon = "OPERATIONS/VECTOR")]
    [Output("Distance", Multiple = true, Type = typeof(float))]
    public class OverVectorDistance : OverVectorOperation
    {
        [Input("From")] public Vector3 from;
        [Input("To")] public Vector3 to;
        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _from = GetInputValue("From", from);
            Vector3 _to = GetInputValue("To", to);

            return Vector3.Distance(_from, _to);
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Base", Name = "Dot", Icon = "OPERATIONS/VECTOR")]
    [Output("Dot", Multiple = true, Type = typeof(float))]
    public class OverVectorDot : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public Vector3 b;
        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            Vector3 _b = GetInputValue("B", b);

            return Vector3.Dot(_a, _b);
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Base", Name = "Normalize", Icon = "OPERATIONS/VECTOR")]
    [Output("Normalize", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorNormalize : OverVectorOperation
    {
        [Input("Value")] public Vector3 value;
        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _value = GetInputValue("Value", value);

            return Vector3.Normalize(_value);
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Base", Name = "Cross", Icon = "OPERATIONS/VECTOR")]
    [Output("Cross", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorCross : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public Vector3 b;
        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            Vector3 _b = GetInputValue("B", b);

            return Vector3.Cross(_a, _b);
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Base", Name = "Magnitude", Icon = "OPERATIONS/VECTOR")]
    [Output("Magnitude", Multiple = true, Type = typeof(float))]
    public class OverVectorMagnitude : OverVectorOperation
    {
        [Input("Value")] public Vector3 value;
        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _value = GetInputValue("Value", value);

            return Vector3.Magnitude(_value);
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Base", Name = "Max", Icon = "OPERATIONS/VECTOR")]
    [Output("Max", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorMax : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public Vector3 b;
        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            Vector3 _b = GetInputValue("B", b);

            return Vector3.Max(_a, _b);
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Base", Name = "Min", Icon = "OPERATIONS/VECTOR")]
    [Output("Min", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorMin : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public Vector3 b;
        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            Vector3 _b = GetInputValue("B", b);

            return Vector3.Min(_a, _b);
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Base", Name = "Scale", Icon = "OPERATIONS/VECTOR")]
    [Output("Scale", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorScale : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public Vector3 b;
        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            Vector3 _b = GetInputValue("B", b);

            return Vector3.Scale(_a, _b);
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Interpolation", Name = "Lerp", Icon = "OPERATIONS/VECTOR")]
    [Output("Value", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorLerp : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public Vector3 b;

        [Input("T")] public float t;

        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            Vector3 _b = GetInputValue("B", b);
            float _t = GetInputValue("T", t);

            return Vector3.Lerp(_a, _b, Mathf.Clamp(_t, 0, 1));
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Interpolation", Name = "Sphere Lerp", Icon = "OPERATIONS/VECTOR")]
    [Output("Value", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorSlerp : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public Vector3 b;

        [Input("T")] public float t;

        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            Vector3 _b = GetInputValue("B", b);
            float _t = GetInputValue("T", t);

            return Vector3.Slerp(_a, _b, Mathf.Clamp(_t, 0, 1));
        }
    }

    // Math
    [Node(Path = "Operations/Vector/Handlers/Math", Name = "Vector Sum", Icon = "OPERATIONS/VECTOR")]
    [Output("Value", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorSum : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public Vector3 b;

        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            Vector3 _b = GetInputValue("B", b);

            return _a + _b;
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Math", Name = "Vector Subtraction", Icon = "OPERATIONS/VECTOR")]
    [Output("Value", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorSub : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public Vector3 b;

        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            Vector3 _b = GetInputValue("B", b);

            return _a - _b;
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Math", Name = "Vector Multiply", Icon = "OPERATIONS/VECTOR")]
    [Output("Value", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorMul : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public float b;

        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            float _b = GetInputValue("B", b);

            return _a * _b;
        }
    }

    [Node(Path = "Operations/Vector/Handlers/Math", Name = "Vector Divide", Icon = "OPERATIONS/VECTOR")]
    [Output("Value", Multiple = true, Type = typeof(Vector3))]
    public class OverVectorDiv : OverVectorOperation
    {
        [Input("A")] public Vector3 a;
        [Input("B")] public float b;

        public override object OnRequestNodeValue(Port port)
        {
            Vector3 _a = GetInputValue("A", a);
            float _b = GetInputValue("B", b);

            return _a / _b;
        }
    }
}