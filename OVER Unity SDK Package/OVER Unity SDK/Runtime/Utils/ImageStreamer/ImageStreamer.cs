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

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace OverSDK
{
    public enum ImageRenderMode { RawImage, MaterialOverride, APIOnly }

    public class ImageStreamer : MonoBehaviour
    {
        public string url;

        public ImageRenderMode renderMode;

        // RenderTexture
        public RawImage targetRawImage;
        //MaterialOverride
        public Renderer targetRenderer;
        public string targetMaterialProperty;


        public AspectRatioFitter.AspectMode aspectRatio;

        [SerializeField] protected Texture2D texture;
        public Texture2D Texture => texture;

        public bool playOnStart;

        Coroutine downloadImageCoroutine;
        //Mono

        private void Start()
        {
            if (playOnStart)
            {
                Play();
            }
        }

        //Main
        public void Play()
        {
            Stop();
            downloadImageCoroutine = StartCoroutine(DownloadImage());
            texture = null;
        }

        public void Stop()
        {
            if (downloadImageCoroutine != null) { StopCoroutine(downloadImageCoroutine); }
        }

        //
        private IEnumerator DownloadImage()
        {
            //scarico immagine da url
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error downloading image: " + www.error);
                    texture = null;
                }
                else
                {
                    texture = DownloadHandlerTexture.GetContent(www);
                }
            }

            //renderizzo immagine su superficie
            switch (renderMode)
            {
                case ImageRenderMode.RawImage: RenderOnRawImage(); break;
                case ImageRenderMode.MaterialOverride: MaterialOverride(); break;
                case ImageRenderMode.APIOnly: break;
            }
        }

        void RenderOnRawImage()
        {
            if (texture != null)
            {
                var fitter = targetRawImage.gameObject.GetComponent<AspectRatioFitter>();
                if (fitter == null) fitter = targetRawImage.gameObject.AddComponent<AspectRatioFitter>();

                float height = texture.height;
                float width = texture.width;
                float aspectRatioCalc = width / height;

                fitter.aspectMode = aspectRatio;
                fitter.aspectRatio = aspectRatioCalc;

                targetRawImage.texture = texture;
            }
        }

        void MaterialOverride()
        {
            if (targetRenderer != null && targetRenderer.sharedMaterial != null && texture != null)
            {
                targetRenderer.sharedMaterial.SetTexture(targetMaterialProperty, texture);
            }
        }

    }
}