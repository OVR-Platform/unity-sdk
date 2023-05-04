/**
 * OVR Unity SDK License
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
    public abstract class OverQuaternionOperation : OverNode { }

    [Node(Path = "Operations/Quaternion", Icon = "OPERATIONS/QUATERNION", Name = "Quaternion Exposer")]
    public class OverQuaternionExposer : OverQuaternionOperation
    {
        [Input("Quaternion")] public Quaternion quaternion;

        [Output("x")] public float x;
        [Output("y")] public float y;
        [Output("z")] public float z;
        [Output("w")] public float w;

        public override object OnRequestValue(Port port)
        {
            Quaternion _quat = GetInputValue("Quaternion", quaternion);

            if (port.Name == "x")
            {
                x = _quat.x;
                return x;
            }
            if (port.Name == "y")
            {
                y = _quat.y;
                return y;
            }
            if (port.Name == "z")
            {
                z = _quat.z;
                return z;
            }
            if (port.Name == "w")
            {
                w = _quat.w;
                return w;
            }

            return base.OnRequestValue(port);
        }
    }

    [Node(Path = "Operations/Quaternion/Handlers/Base", Name = "Angle", Icon = "OPERATIONS/QUATERNION")]
    [Output("Angle", Multiple = true, Type = typeof(float))]
    public class OverQuaternionAngle : OverQuaternionOperation
    {
        [Input("From")] public Quaternion from;
        [Input("To")] public Quaternion to;
        public override object OnRequestValue(Port port)
        {
            Quaternion _from = GetInputValue("From", from);
            Quaternion _to = GetInputValue("To", to);

            return Quaternion.Angle(_from, _to);
        }
    }

    [Node(Path = "Operations/Quaternion/Handlers/Base", Name = "Angle Axis", Icon = "OPERATIONS/QUATERNION")]
    [Output("Rotation", Multiple = true, Type = typeof(Quaternion))]
    public class OverQuaternionAngleAxis : OverQuaternionOperation
    {
        [Input("Angle")] public float angle;
        [Input("Axis")] public Vector3 axis;
        public override object OnRequestValue(Port port)
        {
            float _angle = GetInputValue("Angle", angle);
            Vector3 _axis = GetInputValue("Axis", axis);

            return Quaternion.AngleAxis(_angle, _axis);
        }
    }

    [Node(Path = "Operations/Quaternion/Handlers/Base", Name = "Dot", Icon = "OPERATIONS/QUATERNION")]
    [Output("Dot", Multiple = true, Type = typeof(float))]
    public class OverQuaternionDot : OverQuaternionOperation
    {
        [Input("A")] public Quaternion a;
        [Input("B")] public Quaternion b;
        public override object OnRequestValue(Port port)
        {
            Quaternion _a = GetInputValue("A", a);
            Quaternion _b = GetInputValue("B", b);

            return Quaternion.Dot(_a, _b);
        }
    }

    [Node(Path = "Operations/Quaternion/Handlers/Base", Name = "Euler", Icon = "OPERATIONS/QUATERNION")]
    [Output("Rotation", Multiple = true, Type = typeof(Quaternion))]
    public class OverQuaternionEuler : OverQuaternionOperation
    {
        [Input("Euler")] public Vector3 a;
        public override object OnRequestValue(Port port)
        {
            Vector3 _a = GetInputValue("Euler", a);

            return Quaternion.Euler(_a);
        }
    }

    [Node(Path = "Operations/Quaternion/Handlers/Interpolation", Name = "Lerp", Icon = "OPERATIONS/QUATERNION")]
    [Output("Value", Multiple = true, Type = typeof(Quaternion))]
    public class OverQuaternionLerp : OverQuaternionOperation
    {
        [Input("Starting Rotation")] public Quaternion a;
        [Input("Target Rotation")] public Quaternion b;

        [Input("T")] public float t;

        public override object OnRequestValue(Port port)
        {
            Quaternion _a = GetInputValue("Starting Rotation", a);
            Quaternion _b = GetInputValue("Target Rotation", b);
            float _t = GetInputValue("T", t);

            return Quaternion.Lerp(_a, _b, Mathf.Clamp(_t, 0, 1));
        }
    }

    [Node(Path = "Operations/Quaternion/Handlers/Interpolation", Name = "Sphere Lerp", Icon = "OPERATIONS/QUATERNION")]
    [Output("Value", Multiple = true, Type = typeof(Quaternion))]
    public class OverQuaternionSlerp : OverQuaternionOperation
    {
        [Input("Starting Rotation")] public Quaternion a;
        [Input("Target Rotation")] public Quaternion b;

        [Input("T")] public float t;

        public override object OnRequestValue(Port port)
        {
            Quaternion _a = GetInputValue("Starting Rotation", a);
            Quaternion _b = GetInputValue("Target Rotation", b);
            float _t = GetInputValue("T", t);

            return Quaternion.Slerp(_a, _b, Mathf.Clamp(_t, 0, 1));
        }
    }

    // Math
    [Node(Path = "Operations/Quaternion/Handlers/Math", Name = "Quaternion Combine", Icon = "OPERATIONS/QUATERNION")]
    [Output("Value", Multiple = true, Type = typeof(Quaternion))]
    public class OverQuaternionCombine : OverQuaternionOperation
    {
        [Input("A")] public Quaternion a;
        [Input("B")] public Quaternion b;

        public override object OnRequestValue(Port port)
        {
            Quaternion _a = GetInputValue("A", a);
            Quaternion _b = GetInputValue("B", b);

            return _a * _b;
        }
    }
}