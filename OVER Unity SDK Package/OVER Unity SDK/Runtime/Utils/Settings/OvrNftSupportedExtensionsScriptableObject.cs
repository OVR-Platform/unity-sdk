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
using System.Collections.Generic;
using UnityEngine;

namespace OverSDK
{
    public class OvrNftSupportedExtensionsScriptableObject : ScriptableObject
    {
        public List<string> Image;
        public List<string> Audio;
        public List<string> AssetBundle;
        public List<string> Glb;
        public List<string> Video;

        public OvrNftSupportedExtensionsScriptableObject()
        {
            Image = new List<string>();
            Audio = new List<string>();
            AssetBundle = new List<string>();
            Glb = new List<string>();
            Video = new List<string>();

            InitSupportExtensions();
        }

        private void InitSupportExtensions()
        {
            Image.Add("png");
            Image.Add("jpg");
            Image.Add("jpeg");

            Audio.Add("mp3");

            AssetBundle.Add("unity3d");

            Glb.Add("glb");
            Glb.Add("gltf");

            Video.Add("mp4");
        }
    }
}
