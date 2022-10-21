using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Over
{
    [RequireComponent(typeof(Canvas))]
    public class OvrCanvas : MonoBehaviour
    {
        public Canvas canvas;

        [SerializeField]
        public GameObject panel;

        protected void OnValidate()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.CallbackFunction callbackFunction = null;
            callbackFunction = () =>
            {
                if(Application.isPlaying)
                {
                    UnityEditor.EditorApplication.delayCall -= callbackFunction;
                    return;
                }

                if (gameObject != null && !gameObject.IsAPrefabNotInScene() && !OvrUtils.PrefabModeEnabled())
                {
                    if (canvas == null)
                        canvas = GetComponent<Canvas>();

                    OvrAsset ovrAsset = GetComponentInParent<OvrAsset>();

                    if (ovrAsset != null)
                    {
                        if (ovrAsset.ovrCanvas == null)
                        {
                            ovrAsset.ovrCanvas = this;
                            UnityEditor.EditorUtility.SetDirty(ovrAsset);
                        }
                        else if (ovrAsset.ovrCanvas != this)
                        {                           
                            UnityEditor.EditorApplication.delayCall -= callbackFunction;
                            Debug.LogError("OvrCanvas already in scene! You can only have one OvrCanvas at a time");
                            DestroyImmediate(gameObject);
                            return;
                        }
                    }
                    else
                    {
                        UnityEditor.EditorApplication.delayCall -= callbackFunction;
                        Debug.LogError("OvrCanvas can only be placed as a child of an OvrAsset!");
                        DestroyImmediate(gameObject);
                        return;
                    }
                }

                if (panel == null)
                {
                    if (transform.GetChild(0) != null && transform.GetChild(0).GetChild(0) != null)
                        panel = transform.GetChild(0).GetChild(0).gameObject;
                    else
                        Debug.LogError("Please do not remove panel GameObject");
                }
            };
            if (gameObject != null && !gameObject.IsAPrefabNotInScene())
            {
                UnityEditor.EditorApplication.delayCall -= callbackFunction;
                UnityEditor.EditorApplication.delayCall += callbackFunction;
            }
#endif           
        }
    }
}