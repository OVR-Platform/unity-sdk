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
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [Tags("Data")]
    [Node(Path = "Data/Save", Name = "Save Value", Icon = "DATA/SAVE")]
    public class OverSaveInteranlValue : OverExecutionFlowNode
    {
        [Input("Key")] public string key;
        [Input("Value")] public string value;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            var _key = GetInputValue("Key", key);
            var _value = GetInputValue("Value", value);

            if(OverScriptManager.Main != null)
            {
                bool allOk = OverScriptManager.Main.SaveInternalSaveFile(_key, _value);
                if(!allOk)
                {
                    Debug.LogError($"Unable to save {_key}! File is not available");
                }
            }
            else
            {
                Debug.LogError($"OverScriptManager not available! Ensure you have ath least one instance of it in Scene in order to be able to save localy.");
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            return base.OnRequestNodeValue(port);
        }
    }

    [Tags("Data")]
    [Node(Path = "Data/Save", Name = "Get Saved Value", Icon = "DATA/SAVE")]
    [Output("Value", Type = (typeof(string)), Multiple = true)]
    public class OverGetInteranlValue : OverNode
    {
        [Input("Key")] public string key;

        public override object OnRequestNodeValue(Port port)
        {
            if (OverScriptManager.Main != null)
            {
                string _key = GetInputValue("Key", key);
                if (OverScriptManager.Main.SaveFileJSON.HasKey(_key) && port.Name == "Value")
                {
                    string s = OverScriptManager.Main.SaveFileJSON[_key].ToString();
                    if (s.Length > 2)
                    {
                        return s.Substring(1, s.Length - 2);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    Debug.LogError($"Key {_key} was not found...");
                }
            }
            else
            {
                Debug.LogError($"OverScriptManager not available! Ensure you have ath least one instance of it in Scene in order to be able to save localy.");
            }

            return null;
        }
    }

    [Tags("Data")]
    [Node(Path = "Data/Save", Name = "Has Saved Value", Icon = "DATA/SAVE")]
    [Output("Has Value", Type = (typeof(bool)), Multiple = true)]
    public class OverHasInteranlValue : OverNode
    {
        [Input("Key")] public string key;

        public override object OnRequestNodeValue(Port port)
        {
            if (OverScriptManager.Main != null)
            {
                string _key = GetInputValue("Key", key);
                return OverScriptManager.Main.SaveFileJSON.HasKey(_key);
            }
            else
            {
                Debug.LogError($"OverScriptManager not available! Ensure you have ath least one instance of it in Scene in order to be able to save localy.");
            }

            return null;
        }
    }
}
