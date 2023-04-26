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

using System.IO;
using UnityEditor;
using UnityEngine;

namespace OverSDK.VisualScripting.Editor
{
    [CustomEditor(typeof(OverScript))]
    public class OverScriptEditor : UnityEditor.Editor
    {
        string dir = "Assets/OverScripts";
        private bool mRunningInEditor;
        private int currentPickerWindow;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            OverScript overScript = (OverScript)target;

            if (overScript.OverGraph == null)
            {
                if (GUILayout.Button("Create Graph"))
                {
                    CreateNewOverGraph(overScript);
                }
                overScript.data.variableDatas.Clear();
            }

            if (GUILayout.Button("Load Graph"))
            {
                currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive) + 100;
                EditorGUIUtility.ShowObjectPicker<OverGraph>(overScript.OverGraph, false, "", currentPickerWindow);


            }
            string commandName = Event.current.commandName;
            if (commandName == "ObjectSelectorUpdated")
            {
                overScript.OverGraph = EditorGUIUtility.GetObjectPickerObject() as OverGraph;
                Repaint();
            }
            else if (commandName == "ObjectSelectorClosed")
            {
                overScript.OverGraph = EditorGUIUtility.GetObjectPickerObject() as OverGraph;
            }

        }

        void Awake()
        {
            mRunningInEditor = Application.isEditor && !Application.isPlaying;

            if (mRunningInEditor)
            {
                if (OverScriptManager.Main == null)
                {
                    OverSDK.OvrAsset asset = FindObjectOfType<OverSDK.OvrAsset>();
                    if (asset != null)
                    {
                        GameObject scriptManager = new GameObject("Scripting");
                        scriptManager.transform.SetParent(asset.transform);
                        scriptManager.transform.SetAsFirstSibling();
                        scriptManager.AddComponent<OverScriptManager>();
                    }
                    else
                    {
                        Debug.LogError("You need an OvrAsset in order to add OverScripts to the scene");
                        DestroyImmediate(this);
                    }
                }
            }
        }

        private void OnEnable()
        {
        }

        public void OnDestroy()
        {
            if (mRunningInEditor && target == null)
            {
                if (OverScriptManager.Main != null)
                    OverScriptManager.Main.UpdateMappings();
            }
        }


        // Create new Over Graph
        public void CreateNewOverGraph(OverScript overScript)
        {
            if (overScript.OverGraph == null)
            {
                OverGraph graph = ScriptableObject.CreateInstance<OverGraph>();
                if (string.IsNullOrEmpty(graph.GUID))
                    graph.GUID = System.Guid.NewGuid().ToString();

                int i = 1;

                string newFileName = "New OverGraph";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string path = $"{dir}/{newFileName}.asset";
                string filePath = path;

                while (File.Exists(filePath))
                {
                    filePath = $"{dir}/{newFileName} {i}.asset";
                    i++;
                }
                path = filePath;

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                ProjectWindowUtil.CreateAsset(graph, path);

                //EditorGUIUtility.PingObject(graph);

                overScript.OverGraph = graph;

                if (OverScriptManager.Main != null)
                    OverScriptManager.Main.UpdateMappings();
            }
        }
    }
}