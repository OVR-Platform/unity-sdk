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

using System;
using UnityEngine;

namespace Over
{
    public class OvrPoap : OvrNode
    {
        [TextArea(5, 5)] public string url;
        public static Action<OvrPoap> OpenPoap = null;

        protected void OnValidate()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.CallbackFunction callbackFunction = null;
            callbackFunction = () =>
            {
                if (Application.isPlaying)
                {
                    UnityEditor.EditorApplication.delayCall -= callbackFunction;
                    return;
                }

                if (gameObject != null && !gameObject.IsAPrefabNotInScene() && !OvrUtils.PrefabModeEnabled())
                {
                    OvrAsset ovrAsset = GetComponentInParent<OvrAsset>();

                    if (ovrAsset != null)
                    {
                        if (ovrAsset.ovrPoap == null)
                        {
                            ovrAsset.ovrPoap = this;
                            UnityEditor.EditorUtility.SetDirty(ovrAsset);
                        }
                        else if (ovrAsset.ovrPoap != this)
                        {
                            UnityEditor.EditorApplication.delayCall -= callbackFunction;
                            Debug.LogError("OvrPoap already in scene! You can only have one OvrPoap at a time");
                            DestroyImmediate(this);
                            return;
                        }
                    }
                    else
                    {
                        UnityEditor.EditorApplication.delayCall -= callbackFunction;
                        Debug.LogError("OvrPoap can only be placed as a child of an OvrAsset!");
                        DestroyImmediate(gameObject);
                        return;
                    }
                }
            };
            if (gameObject != null && !gameObject.IsAPrefabNotInScene())
            {
                UnityEditor.EditorApplication.delayCall -= callbackFunction;
                UnityEditor.EditorApplication.delayCall += callbackFunction;
            }
#endif           
        }

        protected override void Execution()
        {
            if (OpenPoap != null)
            {
                OpenPoap(this);
            }
        }
    }
}
