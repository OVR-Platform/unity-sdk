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
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Over
{
    [RequireComponent(typeof(Canvas))]
    public class OvrCanvas : MonoBehaviour
    {
        public Canvas canvas;

        [SerializeField]
        public GameObject panel;

#if UNITY_EDITOR
        private UnityEditor.EditorApplication.CallbackFunction callbackFunction;
        protected void OnValidate()
        {
            callbackFunction = null;

            callbackFunction = () =>
            {
                if (Application.isPlaying)
                {
                    UnityEditor.EditorApplication.delayCall -= callbackFunction;
                    return;
                }

                if (this != null && gameObject != null && !gameObject.IsAPrefabNotInScene() && !OvrUtils.PrefabModeEnabled())
                {
                    if (canvas == null)
                        canvas = GetComponent<Canvas>();

                    OvrAsset ovrAsset = GetComponentInParent<OvrAsset>();

                    if (ovrAsset == null)
                    {
                        ovrAsset = FindObjectOfType<OvrAsset>();

                        if (ovrAsset != null)
                        {
                            transform.parent = ovrAsset.transform;
                        }
                        else
                        {
                            UnityEditor.EditorApplication.delayCall -= callbackFunction;
                            Debug.LogError("OvrCanvas can only be placed as a child of an OvrAsset!");
                            DestroyImmediate(gameObject);
                            return;
                        }
                    }


                    if (ovrAsset.ovrCanvas == null)
                    {
                        ovrAsset.ovrCanvas = this;
                        UnityEditor.EditorUtility.SetDirty(ovrAsset);
                    }
                    else if (ovrAsset.ovrCanvas != this)
                    {
                        UnityEditor.EditorApplication.delayCall -= callbackFunction;
                        Debug.LogError("OvrCanvas already in scene! You can only have one OvrCanvas at a time", ovrAsset.ovrCanvas);
                        EditorGUIUtility.PingObject(ovrAsset.ovrCanvas);
                        DestroyImmediate(gameObject);
                        return;
                    }
                }

                if (this != null && gameObject != null && panel == null)
                {
                    if (transform.GetChild(0) != null && transform.GetChild(0).GetChild(0) != null)
                        panel = transform.GetChild(0).GetChild(0).gameObject;
                    else
                        Debug.LogError("Please do not remove panel GameObject");
                }
            };

            if (this != null && gameObject != null && !gameObject.IsAPrefabNotInScene())
            {
                UnityEditor.EditorApplication.delayCall -= callbackFunction;
                UnityEditor.EditorApplication.delayCall += callbackFunction;
            }
        }

        protected void OnDestroy()
        {
            UnityEditor.EditorApplication.delayCall -= callbackFunction;
        }
#endif
    }
}