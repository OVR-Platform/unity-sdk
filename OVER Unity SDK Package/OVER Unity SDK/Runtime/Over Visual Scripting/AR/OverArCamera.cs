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

using BlueGraph;
using System;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    public struct OvrConst
    {
        public const string PLAYER_CAMERA_TAG = "PlayerCam";
    }


    [Tags("Component")]
    public class OvrCameraNode : OverNode { }

    [Node(Path = "AR", Icon = "AR/OverArCamera")]
    [Tags("Component")]
    public class OverArCamera : OvrCameraNode
    {
        [Output("Ref")] public Camera _camera;
        [Output("Position")] public Vector3 posn;
        [Output("Rotation")] public Quaternion rot;
        [Output("Forward")] public Vector3 fwd;
        [Output("Up")] public Vector3 up;
        [Output("Right")] public Vector3 rgt;

        public static Func<Vector3> GetArCameraPos = null;
        public static Func<Quaternion> GetArCameraRot = null;
        public static Func<Vector3> GetArCameraForward = null;
        public static Func<Vector3> GetArCameraUp = null;
        public static Func<Vector3> GetArCameraRight = null;

        public override object OnRequestNodeValue(Port port)
        {
#if !APP_MAIN
            try
            {
                if (_camera == null)
                {
                    OvrPlayerSimulator obj = GameObject.FindObjectOfType<OvrPlayerSimulator>();
                    if (obj != null)
                        _camera = obj.mainCamera;
                    else
                        _camera = Camera.main;
                }
            }
            catch
            {
                Debug.LogError("Transform Missing");
            }


            if (_camera != null)
            {
                switch (port.Name)
                {
                    case "Ref": return _camera;
                    case "Position": posn = _camera.transform.position; return posn;
                    case "Rotation": rot = _camera.transform.rotation; return rot;
                    case "Forward": fwd = _camera.transform.forward; return fwd;
                    case "Up": up = _camera.transform.up; return up;
                    case "Right": rgt = _camera.transform.right; return rgt;
                }
            }

#elif APP_MAIN
        switch (port.Name)
        {
            case "Position": posn = (GetArCameraPos != null) ? GetArCameraPos() : Vector3.zero; return posn;
            case "Rotation": rot = (GetArCameraRot != null) ? GetArCameraRot() : Quaternion.identity; return rot;
            case "Forward": fwd = (GetArCameraForward != null) ? GetArCameraForward() : Vector3.forward; return fwd;
            case "Up": up = (GetArCameraUp != null) ? GetArCameraUp() : Vector3.up; return up;
            case "Right": rgt = (GetArCameraRight != null) ? GetArCameraRight() : Vector3.right; return rgt;
        }
#endif
            return base.OnRequestNodeValue(port);
        }
    }

    [Tags("Component")]
    [Node(Path = "AR/Camera", Name = "World to Screen", Icon = "AR/OverArCamera")]
    [Output("Screen Position", Multiple = true, Type = typeof(Vector3))]
    public class OverWorldToScreenPoint : OvrCameraNode
    {
        [Input("Camera")] public Camera camera;
        [Input("World Position")] public Vector3 posn;

        [Editable("Eye")] public Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono;

        public override object OnRequestNodeValue(Port port)
        {
            Camera _camera = GetInputValue("Camera", camera);
            Vector3 _posn = GetInputValue("World Position", posn);

            return _camera.WorldToScreenPoint(_posn, eye);
        }
    }

    [Tags("Component")]
    [Node(Path = "AR/Camera", Name = "Screen to World", Icon = "AR/OverArCamera")]
    [Output("World Position", Multiple = true, Type = typeof(Vector3))]
    public class OverScreenToWorldPoint : OvrCameraNode
    {
        [Input("Camera")] public Camera camera;
        [Input("Screen Position")] public Vector3 posn;

        [Editable("Eye")] public Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono;

        public override object OnRequestNodeValue(Port port)
        {
            Camera _camera = GetInputValue("Camera", camera);
            Vector3 _posn = GetInputValue("Screen Position", posn);

            return _camera.ScreenToWorldPoint(_posn, eye);
        }
    }
}