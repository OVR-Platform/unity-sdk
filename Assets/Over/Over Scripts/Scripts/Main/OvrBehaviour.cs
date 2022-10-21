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

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace Over
{
    public enum OvrRandomDirection { X, Y, Z, XY, YZ, XZ, XYZ };
    public enum OvrSpace { Local, World };
    public enum OvrGameObjectType { This, Other }
    public enum OvrCoroutineType { Frames, Seconds };    

    public struct OvrConst
    {
        public const string PLAYER_CAMERA_TAG = "PlayerCam";
    }

    [Serializable]
    public abstract class OvrNode : MonoBehaviour
    {
        [SerializeField]
        protected string nodeId;
        public string NodeId { get => nodeId; set => nodeId = value; }

        [OvrNodeList]
        public List<OvrNode> preExecutionNodes = new List<OvrNode>();
        [OvrNodeList]
        public List<OvrNode> postExecutionNodes = new List<OvrNode>();

        protected abstract void Execution();

        [ContextMenu("Execute")]
        public virtual void Execute()
        {
            PreExecute();
            Execution();
            PostExecute();
        }

        public virtual void PreExecute()
        {
            foreach (var node in preExecutionNodes)
            {
                if (node != null)
                    node.Execute();
                else if (Application.isEditor)
                    Debug.LogError("Null reference");
            }
        }

        public virtual void PostExecute()
        {
            foreach (var node in postExecutionNodes)
            {
                if (node != null)
                    node.Execute();
                else if (Application.isEditor)
                    Debug.LogError("Null reference");
            }
        }

        public virtual void AddNode(OvrNode ovrNode)
        {
            AddToPreExecutionNodes(ovrNode);
        }
        public virtual void AddToPreExecutionNodes(OvrNode ovrNode)
        {
            if (ovrNode != null)
                preExecutionNodes.Add(ovrNode);
            else if (Application.isEditor)
                Debug.LogError("Null reference");

        }
        public virtual void AddToPostExecutionNodes(OvrNode ovrNode)
        {
            if (ovrNode != null)
                postExecutionNodes.Add(ovrNode);
            else if (Application.isEditor)
                Debug.LogError("Null reference");
        }
    }

    public enum AsyncNodeState { ToBeExecuted, InExecution, Executed }

    [Serializable]
    public abstract class OvrAsyncNode : OvrNode
    {
        public AsyncNodeState asyncNodeState;

        protected Coroutine executionCoroutine;

        public override void Execute()
        {
            PreExecute();
            Execution(); 
        }

        public override void PreExecute()
        {
            asyncNodeState = AsyncNodeState.ToBeExecuted;
            base.PreExecute();
            asyncNodeState = AsyncNodeState.InExecution;
        }

        public override void PostExecute()
        {
            asyncNodeState = AsyncNodeState.Executed;
            base.PostExecute();
        }

        public virtual void StopExecution()
        {
            if (asyncNodeState == AsyncNodeState.InExecution)
                PostExecute();
        }
    }

    public class OvrBehaviour : MonoBehaviour
    {

    }

    public static class OvrUtils
    {
        public static bool IsA<T>(this object obj)
        {
            return obj is T;
        }

        //Prefab
        public static bool IsAPrefabNotInScene(this GameObject gameObject)
        {
#if UNITY_EDITOR
            return UnityEditor.PrefabUtility.IsPartOfAnyPrefab(gameObject) && gameObject.scene.name == null;
#else
        return false;
#endif
        }
        public static bool PrefabModeEnabled()
        {
#if UNITY_EDITOR
            return UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null;
#else
        return false;
#endif
        }

    }

}