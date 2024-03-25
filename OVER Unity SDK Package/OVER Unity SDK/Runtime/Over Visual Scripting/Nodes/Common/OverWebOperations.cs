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
using OverSimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace OverSDK.VisualScripting 
{
    [Tags("Web")]
    public abstract class OverWebHandlerNode : OverExecutionFlowNode { }
    [Tags("Web")]
    public abstract class OverWebServiceOperation : OverExecutionFlowNode { }
    [Tags("Web")]
    public abstract class OverWebServiceCommon : OverNode { }

    [Node(Path = "Operations/Web", Name = "Form Field", Icon = "OPERATIONS/WEB")]
    [Output("Resulting Form", Multiple = true, Type = typeof(Dictionary<string, string>))]
    public class OverAddToForm : OverWebServiceCommon
    {
        [Input("Form")] public Dictionary<string, string> form = new Dictionary<string, string>();
        [Input("Key")] public string key;
        [Input("Value")] public string value;

        public override object OnRequestNodeValue(Port port)
        {
            var _form = GetInputValue("Form", form);
            var _key = GetInputValue("Key", key);
            var _value = GetInputValue("Value", value);

            if (_form.ContainsKey(_key))
            {
                _form[_key] = _value;
            }
            else
            {
                _form.Add(_key, _value);
            }

            switch (port.Name)
            {
                case "Resulting Form":
                    return _form;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/Web", Name = "Web Request", Icon = "OPERATIONS/WEB")]
    [Output("On Complete", typeof(OverExecutionFlowData), Multiple = false)]
    public class OverWebServiceNode : OverWebServiceOperation
    {
        public enum OverWebServiceNodeRequestType { GET, POST }

        [Input("Header")] public Dictionary<string, string> header = new Dictionary<string, string>();
        [Input("Body")] public Dictionary<string, string> body = new Dictionary<string, string>();

        [Editable("Endpoint")] public string url;
        [Editable("Type")] public OverWebServiceNodeRequestType type;

        [Output("Success", Type = typeof(bool))] public bool success;
        [Output("Text", Type = typeof(JSONNode))] public JSONNode text;
        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            var _header = GetInputValue("Header", header);
            var _body = GetInputValue("Body", body);

            string getEndpointParams = "";
            if (_body != null && _body.Count > 0)
            {
                getEndpointParams = "?";
                foreach (KeyValuePair<string, string> kvp in _body)
                {
                    getEndpointParams += $"{kvp.Key}={kvp.Value}&";
                }
                getEndpointParams = getEndpointParams.Substring(0, getEndpointParams.Length - 1);
            }

            if (!string.IsNullOrEmpty(sharedContext.scriptGUID))
            {
                OverScript overScript = OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript;
                switch (type)
                {
                    case OverWebServiceNodeRequestType.GET:
                        overScript.SendWebRequest(GetRequestAsync(url + getEndpointParams, _header, () => {
                            IExecutableOverNode next = GetNextExecutableNode("On Complete");
                            (Graph as OverGraph).Execute(next, data);
                        }));
                        break;
                    case OverWebServiceNodeRequestType.POST:
                        overScript.SendWebRequest(PostRequestAsync(url, _header, _body, () => {
                            IExecutableOverNode next = GetNextExecutableNode("On Complete");
                            (Graph as OverGraph).Execute(next, data);
                        }));
                        break;
                }

            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Text":
                    return text;
                case "Success":
                    return success;
            }

            return base.OnRequestNodeValue(port);
        }

        private IEnumerator GetRequestAsync(string url, Dictionary<string, string> header, UnityAction onComplete)
        {
            Debug.Log(url);
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                if (header != null)
                {
                    foreach (KeyValuePair<string, string> kvp in header)
                    {
                        webRequest.SetRequestHeader(kvp.Key, kvp.Value);
                    }
                }

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                    case UnityWebRequest.Result.ProtocolError:
                        text = JSONNode.Parse(webRequest.error);
                        success = false;
                        break;
                    case UnityWebRequest.Result.Success:
                        text = JSONNode.Parse(webRequest.downloadHandler.text);
                        success = true;
                        break;
                }
                onComplete?.Invoke();
            }
        }

        private IEnumerator PostRequestAsync(string url, Dictionary<string, string> header, Dictionary<string, string> body, UnityAction onComplete)
        {
            WWWForm form = new WWWForm();
            if (body != null && body.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in body)
                {
                    form.AddField(kvp.Key, kvp.Value);
                }
            }

            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
            {
                if (header != null)
                {
                    foreach (KeyValuePair<string, string> kvp in header)
                    {
                        webRequest.SetRequestHeader(kvp.Key, kvp.Value);
                    }
                }

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                    case UnityWebRequest.Result.ProtocolError:
                        text = JSONNode.Parse(webRequest.error);
                        success = false;
                        break;
                    case UnityWebRequest.Result.Success:
                        text = JSONNode.Parse(webRequest.downloadHandler.text);
                        success = true;
                        break;
                }

                onComplete?.Invoke();
            }
        }
    }

    [Node(Path = "Operations/Web", Icon = "OPERATIONS/WEB", Name = "Open Url")]
    public class OverOpenURL : OverWebHandlerNode
    {
        [Input("URL")] public string url;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            string _url = GetInputValue("URL", url);

            if(!string.IsNullOrEmpty(_url))
            {
                Application.OpenURL(_url);
            }

            return base.Execute(data);
        }
    }

}
