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
    public abstract class OverStringOperation : OverNode { }

    //string

    [Node(Path = "Operations/String", Name = "Concat", Icon = "OPERATIONS/STRING")]
    [Output("Result", Type = (typeof(string)), Multiple = true)]
    public class OverStringConcat : OverStringOperation
    {
        [Input("A")] public string s;
        [Input("B")] public string s2;

        public override object OnRequestValue(Port port)
        {
            var _s = GetInputValue("A", s);
            var _s2 = GetInputValue("B", s2);

            return string.Concat(_s, _s2);
        }
    }

    // numeric

    [Node(Path = "Operations/String", Name = "Length", Icon = "OPERATIONS/STRING")]
    [Output("Result", Type = (typeof(int)), Multiple = true)]
    public class OverStringLength : OverStringOperation
    {
        [Input("String")] public string s;

        public override object OnRequestValue(Port port)
        {
            var _string = GetInputValue("String", s);

            return _string.Length;
        }
    }

    //bools

    [Node(Path = "Operations/String", Name = "Contains", Icon = "OPERATIONS/STRING")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverStringContains : OverStringOperation
    {
        [Input("String")] public string s;
        [Input("Substring")] public string substring;

        public override object OnRequestValue(Port port)
        {
            var _string = GetInputValue("String", s);
            var _substring = GetInputValue("Substring", substring);

            return _string.Contains(_substring);
        }
    }

    [Node(Path = "Operations/String", Name = "Is Null or Empty", Icon = "OPERATIONS/STRING")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverStringNullOdEmpty : OverStringOperation
    {
        [Input("String")] public string s;

        public override object OnRequestValue(Port port)
        {
            var _s = GetInputValue("String", s);

            return string.IsNullOrEmpty(_s);
        }
    }

    //tostring
    [Node(Path = "Operations/String", Name = "To String", Icon = "OPERATIONS/STRING")]
    [Output("Result", Type = (typeof(string)), Multiple = true)]
    public class OverToString : OverStringOperation
    {
        [Input("entity")] public object entity;

        public override object OnRequestValue(Port port)
        {
            var _entity = GetInputValue("entity", entity);

            if (_entity == null)
            {
                return "";
            }

            return _entity.ToString();
        }
    }
}