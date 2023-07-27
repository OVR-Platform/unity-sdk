/**
 * OVR Unity SDK License
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

using OverSDK.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace OverSDK
{
    [InitializeOnLoad]
    public class OverVisualScriptingInstantiator : Editor
    {
        static double renameTime;

        [MenuItem("GameObject/OVER Visual Scripting/Over Script", isValidateFunction: false, priority: 1)]
        public static OverScript InstantiateOverScript()
        {
            if (OverScriptManager.Main == null)
            {
                InstantiateOverScriptManager();
            }

            OverSDK.OvrAsset asset = FindObjectOfType<OverSDK.OvrAsset>() ?? OvrPrefabInstantiator.InstantiateOvrAsset();

            GameObject newScriptObject = new GameObject("Over Script");
            newScriptObject.transform.SetParent(asset.transform);
            OverScript newScript = newScriptObject.AddComponent<OverScript>();

            //prompt rename mode
            EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
            Selection.activeGameObject = newScriptObject;
            renameTime = EditorApplication.timeSinceStartup + 0.2d;
            EditorApplication.update += EngageRenameMode;

            return newScript;
        }

        [MenuItem("GameObject/OVER Visual Scripting/Over Script Manager", isValidateFunction: false, priority: 1)]
        public static OverScriptManager InstantiateOverScriptManager()
        {
            OverSDK.OvrAsset asset = FindObjectOfType<OverSDK.OvrAsset>() ?? OvrPrefabInstantiator.InstantiateOvrAsset();
            GameObject scriptManager = new GameObject("Over Script Manager");
            scriptManager.transform.SetParent(asset.transform);
            scriptManager.transform.SetAsFirstSibling();
            OverScriptManager manager = scriptManager.AddComponent<OverScriptManager>();

            //prompt rename mode
            EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
            Selection.activeGameObject = scriptManager;
            renameTime = EditorApplication.timeSinceStartup + 0.2d;
            EditorApplication.update += EngageRenameMode;

            return manager;
        }

        private static void EngageRenameMode()
        {
            if (EditorApplication.timeSinceStartup >= renameTime)
            {
                EditorApplication.update -= EngageRenameMode;
                EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
                EditorApplication.ExecuteMenuItem("Edit/Rename");
            }
        }
    }
}

