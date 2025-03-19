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
using OverSDK.VisualScripting;
using OverSimpleJSON;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


namespace OverSDK.VisualScripting
{
    [UnitTitle("Web Request")]
    [UnitCategory("Over")]
    [TypeIcon(typeof(OverBaseType))]
    public class OverWebServiceNodeUVS : Unit
    {
        public enum OverWebServiceNodeRequestType { GET, POST }

        [DoNotSerialize]
        public ControlInput inputTrigger;
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        [AllowsNull]
        public ValueInput header;
        public ValueInput body;

        [DoNotSerialize]
        public ValueInput url;
        public ValueInput type;

        [DoNotSerialize]
        public ValueOutput success;
        public ValueOutput text;

        private string response = "";
        private bool requestSuccess = false;
        protected override void Definition()
        {
            header = ValueInput<AotDictionary>("Header", null);
            header = header.AllowsNull();

            body = ValueInput<AotDictionary>("Body", null);
            body = body.AllowsNull();

            url = ValueInput<string>("Url", "");
            type = ValueInput<OverWebServiceNodeRequestType>("Type", OverWebServiceNodeRequestType.GET);
            success = ValueOutput<bool>("Success");
            text = ValueOutput<string>("Text");

            inputTrigger = ControlInputCoroutine("", requestAsync);
            outputTrigger = ControlOutput("");
        }


        private IEnumerator requestAsync(Flow flow)
        {
            string _url = flow.GetValue<string>(url);
            Dictionary<string, string> _header = null;
            Dictionary<string, string> _body = null;
            AotDictionary aotHeader = null;
            AotDictionary aotBody = null;
            if (header.hasAnyConnection)
            {
                aotHeader = flow.GetValue<AotDictionary>(header);
                _header = OverUtilityUVS.AotDictionaryToDictionary<string>(aotHeader);
            }
            
            if(body.hasAnyConnection)
            {
                aotBody = flow.GetValue<AotDictionary>(body);
                _body = OverUtilityUVS.AotDictionaryToDictionary<string>(aotBody);

            }

            OverWebServiceNodeRequestType _type = flow.GetValue<OverWebServiceNodeRequestType>(type);


            if (_type == OverWebServiceNodeRequestType.GET)
            {
                yield return GetRequestAsync(_url, _header);
            }
            else
            {
                yield return PostRequestAsync(_url, _header, _body);
            }

            flow.SetValue(success, requestSuccess);
            flow.SetValue(text, response);

            yield return outputTrigger;
        }


        private IEnumerator GetRequestAsync(string url, Dictionary<string, string> header = null)
        {
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
                        response = webRequest.error;
                        requestSuccess = false;
                        break;
                    case UnityWebRequest.Result.Success:
                        response = webRequest.downloadHandler.text;
                        requestSuccess = true;
                        break;
                }
            }

            yield return null;
        }

        private IEnumerator PostRequestAsync(string url, Dictionary<string, string> header = null, Dictionary<string, string> body = null)
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
                        response = webRequest.error;
                        requestSuccess = false;
                        break;
                    case UnityWebRequest.Result.Success:
                        response = webRequest.downloadHandler.text;
                        requestSuccess = true;
                        break;
                }
            }
            yield return null;

        }
    }
}