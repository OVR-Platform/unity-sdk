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
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Networking;
using System.Collections;
namespace OverSDK.VisualScripting
{
    [UnitTitle("Texture from URL")]
    [UnitCategory("Over")]
    [TypeIcon(typeof(OverBaseType))]

    public class OverUrlToTextureUVS : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput url;
        public ValueOutput texture;

        public Texture2D tex;
        protected override void Definition()
        {
            //NOTE: This is a coroutine unit, so we need to use the ControlInputCoroutine method
            //NOTE: you can enable the coroutine unit by setting the "Coroutine" field to true in "UPDATE" or other events like "LATE UPDATE" etc
            inputTrigger = ControlInputCoroutine("", DownloadImage);
            outputTrigger = ControlOutput("");

            url = ValueInput<string>("Url", "");
            texture = ValueOutput<Texture2D>("Texture2D");
        }



        private IEnumerator DownloadImage(Flow flow)
        {
            string _url = flow.GetValue<string>(url);

            //scarico immagine da url
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(_url))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error downloading image: " + www.error);
                    tex = null;
                }
                else
                {
                    tex = DownloadHandlerTexture.GetContent(www);
                }

                flow.SetValue(texture, tex);    

                yield return outputTrigger;
            }
        }

    }
}
