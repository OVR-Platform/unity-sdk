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

using BlueGraph;
using OverSimpleJSON;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [Tags("Common")]
    public abstract class OverJSONCommon : OverNode { }
    [Tags("Common")]
    public abstract class OverJSONOperation : OverExecutionFlowNode { }

    [Node(Path = "Operations/JSON", Name = "Has Key", Icon = "OPERATIONS/STRING")]
    [Output("Result", Type = (typeof(bool)), Multiple = true)]
    public class OverJSONContainsKey : OverJSONCommon
    {
        [Input("JSON")] public JSONNode json;
        [Input("Key")] public string key;

        public override object OnRequestNodeValue(Port port)
        {
            JSONNode _json = GetInputValue("JSON", json);
            string _key = GetInputValue("Key", key);

            if(_json == null || string.IsNullOrEmpty(_key))
            {
                return false;
            }

            bool result = _json.HasKey(_key);

            if (port.Name == "Result")
            {
                return result;
            }
            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/JSON", Name = "Get JSON Value", Icon = "OPERATIONS/STRING")]
    [Output("Result", Type = (typeof(JSONNode)), Multiple = true)]
    public class OverJSONGetValue : OverJSONCommon
    {
        [Input("JSON")] public JSONNode json;
        [Input("Key")] public string key;

        public override object OnRequestNodeValue(Port port)
        {
            JSONNode node = null;
            JSONNode _json = GetInputValue("JSON", json);
            string _key = GetInputValue("Key", key);
            if(_json != null)
            {
                if(_json.HasKey(_key))
                {
                    node = _json[_key];
                }
                else
                {
                    Debug.LogWarning($"[WARNING] Key \"{_key}\" not present in json. Node {Name}");
                }
            }

            if(port.Name == "Result")
            {
                return node;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/JSON", Name = "Set JSON Entry", Icon = "OPERATIONS/STRING")]
    [Output("Updated JSON", Type = (typeof(JSONNode)), Multiple = true)]
    public class OverJSONSetValue : OverJSONOperation
    {
        [Input("JSON")] public JSONNode json;
        [Input("Key")] public string key;
        [Input("Value")] public string value;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            JSONNode _json = GetInputValue("JSON", new JSONObject());
            if(_json == null)
            {
                _json = new JSONObject();
            }

            var _key = GetInputValue("Key", key);
            var _value = GetInputValue("Value", value);

            if(_json != null && !string.IsNullOrEmpty(_key) && !string.IsNullOrEmpty(_value))
            {
                _json[_key] = _value;
                json = _json;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Updated JSON":
                    return json;
            }

            return base.OnRequestNodeValue(port);
        }
    }
}