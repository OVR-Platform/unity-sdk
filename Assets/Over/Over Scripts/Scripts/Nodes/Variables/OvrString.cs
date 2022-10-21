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
    public class OvrString : OvrVariable
    {
        [SerializeField]
        protected string variable;
        public string TypedVariable { get => variable; set => variable = value; }
        public override object Variable { get => variable; set => throw new System.NotImplementedException(); }

        protected override void OnValidate()
        {
            variableType = OvrVariableType.String;
        }
        protected override void Awake()
        {
            variableType = OvrVariableType.String;
        }

        public override bool Equals(OvrVariable OvrVariable)
        {
            OvrString data = OvrVariable as OvrString;
            if (data != null)
                return data.variable == this.variable;
            else
                return false;
        }

        public override bool GreaterThan(OvrVariable ovrVariable)
        {
            return false;
        }

        public override bool LessThan(OvrVariable ovrVariable)
        {
            return false;
        }

        public override bool NotEquals(OvrVariable ovrVariable)
        {
            OvrString data = ovrVariable as OvrString;
            if (data != null)
                return data.variable != this.variable;
            else
                return true;
        }

        public void StringFunction(StringFunctionType mathOperationType, ref OvrString result, OvrVariable ovrVariable2 = null, OvrVariable ovrVariable3 = null, OvrVariable ovrVariable4 = null)
        {
            switch (mathOperationType)
            {
                case StringFunctionType.ToString:
                    result.Variable = ovrVariable2.Variable.ToString();
                    break;
                case StringFunctionType.Addition:
                    result.Variable = ovrVariable2.Variable.ToString() + ovrVariable3.ToString();                
                    break;
            }
        }
    }
}