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
using UnityEngine.Events;

namespace Over
{
    public enum ArCameraActionType
    {
        GetPosition = 0,
        GetRotation = 1,
        GetForward = 200,
        GetUp = 201,
        GetRight = 202,
    };

    public class OvrArCamera : OvrNode  
    {
        public bool findAutoCamera = true;
        public Transform fakeCameraInEditor;    

        public ArCameraActionType actionType;

        [OvrVariable]
        public OvrVector3 targetPosition;
        [OvrVariable]
        public OvrQuaternion targetRotation;
        [OvrVariable]
        public OvrVector3 targetDir;

        public static Func<Vector3> GetArCameraPos = null;
        public static Func<Quaternion> GetArCameraRot = null;
        public static Func<Vector3> GetArCameraForward = null;
        public static Func<Vector3> GetArCameraUp = null;
        public static Func<Vector3> GetArCameraRight = null;

        protected override void Execution()
        {
#if !APP_MAIN
            if (fakeCameraInEditor == null)
            {
                if(findAutoCamera)
                {
                    try
                    {
                        GameObject obj = GameObject.FindGameObjectWithTag(OvrConst.PLAYER_CAMERA_TAG);

                        if (obj != null)
                            fakeCameraInEditor = obj.transform.GetChild(0);
                    }
                    catch
                    {
                        Debug.LogError("Transform Missing");
                        return;
                    }                  
                }      
                if (fakeCameraInEditor == null)
                {
                    Debug.LogError("Transform Missing");
                    return;
                }
            }

            switch (actionType)
            {
                case ArCameraActionType.GetPosition:
                    targetPosition.TypedVariable = fakeCameraInEditor.position;
                    break;
                case ArCameraActionType.GetRotation:
                    targetRotation.TypedVariable = fakeCameraInEditor.rotation;
                    break;
                case ArCameraActionType.GetForward:
                    targetDir.TypedVariable = fakeCameraInEditor.forward;
                    break;
                case ArCameraActionType.GetUp:
                    targetDir.TypedVariable = fakeCameraInEditor.up;
                    break;
                case ArCameraActionType.GetRight:
                    targetDir.TypedVariable = fakeCameraInEditor.right;
                    break;
            }

#elif APP_MAIN
            switch (actionType)
            {

                case ArCameraActionType.GetPosition:
                    if (GetArCameraPos != null)
                        targetPosition.TypedVariable = GetArCameraPos();
                    else
                        targetPosition.TypedVariable = Vector3.zero;
                    break;
                case ArCameraActionType.GetRotation:
                    if (GetArCameraRot != null)
                        targetRotation.TypedVariable = GetArCameraRot();
                    else
                        targetRotation.TypedVariable = Quaternion.identity;
                    break;
                case ArCameraActionType.GetForward:
                    if (GetArCameraForward != null)
                        targetDir.TypedVariable = GetArCameraForward();
                    else
                        targetDir.TypedVariable = Vector3.zero;
                    break;
                case ArCameraActionType.GetUp:
                    if (GetArCameraUp != null)
                        targetDir.TypedVariable = GetArCameraUp();
                    else
                        targetDir.TypedVariable = Vector3.zero;
                    break;
                case ArCameraActionType.GetRight:
                    if (GetArCameraRight != null)
                        targetDir.TypedVariable = GetArCameraRight();
                    else
                        targetDir.TypedVariable = Vector3.zero;
                    break;
            }
#endif
        }
    }
}