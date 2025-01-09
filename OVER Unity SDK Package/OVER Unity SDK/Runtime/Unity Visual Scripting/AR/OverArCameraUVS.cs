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
using System;
namespace OverSDK.VisualScripting
{

    [UnitTitle("Over Ar Camera")]
    [UnitCategory("Over")]
    [TypeIcon(typeof(OverBaseType))]

    public class OverArCameraUVS : Unit
    {
        public Camera mainCamera;

        [DoNotSerialize]
        [PortLabel("Output")]
        public ValueOutput Ref;
        public ValueOutput Position;
        public ValueOutput Rotation;
        public ValueOutput Forward;
        public ValueOutput Up;
        public ValueOutput Right;



        public static Func<Vector3> GetArCameraPos = null;
        public static Func<Quaternion> GetArCameraRot = null;
        public static Func<Vector3> GetArCameraForward = null;
        public static Func<Vector3> GetArCameraUp = null;
        public static Func<Vector3> GetArCameraRight = null;

        protected override void Definition()
        {
            Ref = ValueOutput<Camera>("Camera", GetCamera);
            Position = ValueOutput<Vector3>("Position", GetPosition);
            Rotation = ValueOutput<Quaternion>("Rotation", GetRotation);
            Forward = ValueOutput<Vector3>("Forward", GetForward);
            Up = ValueOutput<Vector3>("Up", GetUp);
            Right = ValueOutput<Vector3>("Right", GetRight);
        }

        private void GetCamera()
        {
            Camera _camera = null;
#if !APP_MAIN
            try
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag(OvrConst.PLAYER_CAMERA_TAG);

                if (objs != null)
                {
                    foreach (GameObject obj in objs)
                    {
                        if (obj.transform.childCount > 0)
                        {
                            _camera = obj.transform.GetChild(0).GetComponent<Camera>();

                        }
                    }
                }
                else
                {
                    _camera = Camera.main;
                }


            }
            catch (Exception ex)
            {
                Debug.LogError("Transform Missing " + ex.Message);

                _camera = Camera.main;
            }
#endif
            mainCamera = _camera;
        }

        private Camera GetCamera(Flow flow)
        {
            if (mainCamera == null)
                GetCamera();

            return mainCamera;
        }

        private Vector3 GetPosition(Flow flow)
        {
            if (mainCamera == null)
                GetCamera();

            Vector3 vector3 = Vector3.zero;

#if !APP_MAIN
            if (mainCamera != null)
            {
                vector3 = mainCamera.transform.position;
            }
#else
        if (GetArCameraPos != null)
        {
            vector3 = GetArCameraPos();
        }
#endif

            return vector3;
        }

        private Quaternion GetRotation(Flow flow)
        {
            if (mainCamera == null)
                GetCamera();

            Quaternion quaternion = Quaternion.identity;

#if !APP_MAIN
            if (mainCamera != null)
            {
                quaternion = mainCamera.transform.rotation;
            }
#else
        {
            if (GetArCameraRot != null)
            {
                quaternion = GetArCameraRot();
            }
        }
#endif
            return quaternion;
        }

        private Vector3 GetForward(Flow flow)
        {
            if (mainCamera == null)
                GetCamera();

            Vector3 vector3 = Vector3.forward;

#if !APP_MAIN
            if (mainCamera != null)
            {
                vector3 = mainCamera.transform.forward;
            }
#else
        {
            if (GetArCameraForward != null)
            {
                vector3 = GetArCameraForward();
            }
        }
#endif
            return vector3;
        }

        private Vector3 GetUp(Flow flow)
        {
            if (mainCamera == null)
                GetCamera();

            Vector3 vector3 = Vector3.up;

#if !APP_MAIN
            if (mainCamera != null)
            {
                vector3 = mainCamera.transform.up;
            }
#else
        {
            if (GetArCameraUp != null)
            {
                vector3 = GetArCameraUp();
            }
        }
#endif
            return vector3;
        }

        private Vector3 GetRight(Flow flow)
        {
            if (mainCamera == null)
                GetCamera();

            Vector3 vector3 = Vector3.right;

#if !APP_MAIN
            if (mainCamera != null)
            {
                vector3 = mainCamera.transform.right;
            }
#else
        {
            if (GetArCameraRight != null)
            {
                vector3 = GetArCameraRight();
            }
        }
#endif
            return vector3;
        }
    }
}
