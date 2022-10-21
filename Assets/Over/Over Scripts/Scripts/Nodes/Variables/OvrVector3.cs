/**
 * OVER Unity SDK License
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
 * with services provided by OVER.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

namespace Over
{
    public class OvrVector3 : OvrNumericVariable
    {
        [SerializeField]
        protected Vector3 variable;
        public Vector3 TypedVariable { get => variable; set => variable = value; }
        public override object Variable { get => variable; set => variable = (Vector3)value; }

        protected override void OnValidate()
        {
            variableType = OvrVariableType.Vector3;
        }
        protected override void Awake()
        {
            variableType = OvrVariableType.Vector3;
        }

        public override bool Equals(OvrVariable ovrVariable)
        {
            OvrVector3 data = ovrVariable as OvrVector3;
            if (data != null)
                return data.variable == this.variable;
            else
                return false;
        }

        public override bool GreaterThan(OvrVariable ovrVariable)
        {
            OvrVector3 data = ovrVariable as OvrVector3;
            if (data != null)
                return data.TypedVariable.magnitude < this.variable.magnitude;
            else
                return false;
        }

        public override bool LessThan(OvrVariable ovrVariable)
        {
            OvrVector3 data = ovrVariable as OvrVector3;
            if (data != null)
                return data.TypedVariable.magnitude > this.variable.magnitude;
            else
                return false;
        }

        public override bool NotEquals(OvrVariable ovrVariable)
        {
            OvrVector3 data = ovrVariable as OvrVector3;
            if (data != null)
                return data.variable != this.variable;
            else
                return true;
        }

        public override void MathfFunction(MathFunctionType mathFunctionType, ref OvrNumericVariable result, OvrVariable ovrVariable2 = null, OvrVariable ovrVariable3 = null, OvrVariable ovrVariable4 = null)
        {
            if (!result.IsA<OvrVector3>())
            {
                Debug.Log("Operation Error: " + id + " in " + gameObject.name);
                return;
            }

            switch (mathFunctionType)
            {
                case MathFunctionType.Addition:
                    switch (ovrVariable2.variableType)
                    {
                        case OvrVariableType.Vector3:
                            result.Variable = variable + (Vector3)ovrVariable2.Variable;
                            break;
                        default:
                            Debug.Log("Operation Error: " + id + " in " + gameObject.name);
                            break;
                    }
                    break;
                case MathFunctionType.Subtraction:
                    switch (ovrVariable2.variableType)
                    {
                        case OvrVariableType.Vector3:
                            result.Variable = variable - (Vector3)ovrVariable2.Variable;
                            break;
                        default:
                            Debug.Log("Operation Error: " + id + " in " + gameObject.name);
                            break;
                    }
                    break;
                case MathFunctionType.Multiplication:
                    switch (ovrVariable2.variableType)
                    {
                        case OvrVariableType.Int:
                            result.Variable = variable * (int)ovrVariable2.Variable;
                            break;
                        case OvrVariableType.Float:
                            result.Variable = variable * (float)ovrVariable2.Variable;
                            break;
                        case OvrVariableType.Vector3:
                            Vector3 value2Vector3 = (Vector3)ovrVariable2.Variable;
                            result.Variable = new Vector3(variable.x * value2Vector3.x, variable.y * value2Vector3.y, variable.z * value2Vector3.z);
                            break;
                        default:
                            Debug.Log("Operation Error: " + id + " in " + gameObject.name);
                            break;
                    }
                    break;
                case MathFunctionType.Division:
                    switch (ovrVariable2.variableType)
                    {
                        case OvrVariableType.Int:
                            result.Variable = variable / (int)ovrVariable2.Variable;
                            break;
                        case OvrVariableType.Float:
                            result.Variable = variable / (float)ovrVariable2.Variable;
                            break;
                        case OvrVariableType.Vector3:
                            Vector3 value2Vector3 = (Vector3)ovrVariable2.Variable;
                            if (value2Vector3.x != 0 && value2Vector3.y != 0 && value2Vector3.z != 0)
                                result.Variable = new Vector3(variable.x / value2Vector3.x, variable.y / value2Vector3.y, variable.z / value2Vector3.z);
                            else
                                result.Variable = Vector2.zero;
                            break;
                        default:
                            Debug.Log("Operation Error: " + id + " in " + gameObject.name);
                            break;
                    }
                    break;
            }
        }


        public void Vector3Function(Vector3FunctionType vector3FunctionType, ref OvrVariable result, OvrVariable ovrVariable2 = null, OvrVariable ovrVariable3 = null, OvrVariable ovrVariable4 = null)
        {
            switch (vector3FunctionType)
            {
                case Vector3FunctionType.GetDistance:

                    if (!result.IsA<OvrFloat>() || !ovrVariable2.IsA<OvrVector3>())
                    {
                        Debug.Log("Operation Error: " + id + " in " + gameObject.name);
                        return;
                    }

                    result.Variable = (variable - (Vector3)ovrVariable2.Variable).magnitude;
                    break;
            }
        }
    }
}