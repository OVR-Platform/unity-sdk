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

using OverSimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [Serializable]
    public class OverScriptReference
    {
        public OverScript overScript;
        public OverGraph overGraphAsset;
    }

    public enum ScriptManagementError { MultipleScript }

    [Serializable]
    public struct ErrorScriptMessage
    {
        public ScriptManagementError error;
        public OverScript source;
        public string message;

        public ErrorScriptMessage(ScriptManagementError error, OverScript source)
        {
            this.error = error;
            this.source = source;
            this.message = "";

            switch (error)
            {
                case ScriptManagementError.MultipleScript: this.message = $"Multiple occurrences of {source.OverGraph.GraphName}[{source.GUID}]. Remove one to continue."; break;
                default: break;
            }
        }
    }

    public class OverScriptManager : MonoBehaviour
    {
        private static OverScriptManager main = null;
        public static OverScriptManager Main
        {
            get
            {
                if (main == null)
                {
                    OverScriptManager overScriptManager = (OverScriptManager)FindObjectOfType(typeof(OverScriptManager));
                    if (overScriptManager != null && !overScriptManager.IsError)
                        main = overScriptManager;
                }
                return main;
            }
        }

        public Dictionary<string, OverScriptReference> overScriptsReferences = new Dictionary<string, OverScriptReference>();
        [SerializeField] public List<OverScript> managedScripts = new List<OverScript>();
        public bool IsError => errors.Count > 0;

        [SerializeField] OverScriptData data;
        public OverScriptData Data
        {
            get
            {
                if (data == null) data = new OverScriptData();
                return data;
            }
            set { data = value; }
        }

        [Header("Debug")]
        public List<ErrorScriptMessage> errors = new List<ErrorScriptMessage>();

        public static Func<string> GetEnvironmentIndex = null;

        public JSONNode SaveFileJSON { get; private set; }
        [SerializeField][ReadOnly] string localSaveFilePath;

        private void OnValidate()
        {
            bool isInActualScene = gameObject != null && !string.IsNullOrEmpty(gameObject.scene.name) && gameObject.scene.name.Equals(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            if (isInActualScene)
                ConvertOldVariablesToNewList(alsoMangedScript: false);
        }

        private void Awake()
        {
            if (IsError)
            {
                DisplayErrors();
                if (main == this)
                    main = null;
                Destroy(this);
                return;
            }

            UpdateScriptReferences();

            //After updating references to scripts
            //Centralized because there may be variables in common between scripts
            ConvertOldVariablesToNewList();

            GetOrCreateInternalSaveFile();

            Data.RebuildLookupDictionary();
        }

#if UNITY_EDITOR && !APP_MAIN  
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void DidReloadScripts()
        {
            if (OverScriptManager.Main != null)
                OverScriptManager.Main.UpdateScriptReferences();
        }
#endif

        public void UpdateScriptReferences()
        {
            overScriptsReferences.Clear();
            managedScripts.Clear();
            errors.Clear();

            OverScript[] scripts = FindObjectsOfType<OverScript>();

            // ERROR HANDLING
            List<string> guids = new List<string>();
            foreach (OverScript script in scripts)
            {
                if (script.OverGraph != null)
                {
                    if (!guids.Contains(script.GUID))
                    {
                        guids.Add(script.GUID);
                    }
                    else
                    {
                        script.GUID = Guid.NewGuid().ToString();
                    }
                }
            }

            DisplayErrors();

            foreach (OverScript script in scripts)
            {
                if (script.OverGraph != null)
                {
                    OverScriptReference reference = new OverScriptReference() 
                    {
                        overGraphAsset = script.OverGraph,
                        overScript = script
                    };

                    if (!overScriptsReferences.ContainsKey(reference.overScript.GUID))
                    {
                        overScriptsReferences.Add(reference.overScript.GUID, reference);
                        managedScripts.Add(script);
                    }
                    else
                    {
                        overScriptsReferences[reference.overScript.GUID] = reference;
                    }
                }
            }

            ConvertOldVariablesToNewList(alsoMangedScript: false);
        }

        public OverScript GetOverScript(string scriptGUID)
        {
            return managedScripts.Find(x => x.GUID.Equals(scriptGUID));
        }

        public void DisplayErrors()
        {
            foreach (var error in errors)
            {
                Debug.LogError($"ERROR: {error.message}", error.source.gameObject);
            }
        }

        //save internal
        public void GetOrCreateInternalSaveFile()
        {
            string saveDirectoryPath = "";
#if !APP_MAIN
            saveDirectoryPath = Path.Combine(Application.persistentDataPath, "SaveFiles");
#elif APP_MAIN
            if(GetEnvironmentIndex != null)
            {
                string environmentId = GetEnvironmentIndex();
                saveDirectoryPath = Path.Combine(Application.persistentDataPath, "SaveFiles", environmentId);
            }  
            else
            { 
                return;
            }
#endif

            if (!Directory.Exists(saveDirectoryPath))
            {
                Directory.CreateDirectory(saveDirectoryPath);
            }

            string fileName = "saveFileJson";
            string fileExtension = "json";
            localSaveFilePath = Path.Combine(saveDirectoryPath, $"{fileName}.{fileExtension}");

            if (!File.Exists(localSaveFilePath))
            {
                using (FileStream fileStream = File.Create(localSaveFilePath))
                {
                    JSONObject newJson = new JSONObject();
                    byte[] byteArray = Encoding.UTF8.GetBytes(newJson.ToString());
                    fileStream.Write(byteArray, 0, byteArray.Length);
                    fileStream.Close();
                }
            }

            // Read all lines from the text file
            string[] lines = File.ReadAllLines(localSaveFilePath);
            string completeJson = string.Empty;
            // Process each line
            foreach (string line in lines)
            {
                completeJson += line;
            }

            try
            {
                SaveFileJSON = JSONNode.Parse(completeJson);
            }
            catch
            {
                SaveFileJSON = JSONNode.Parse("{}");
            }
        }

        public bool SaveInternalSaveFile(string key, string value)
        {
            if (SaveFileJSON != null)
            {
                SaveFileJSON[key] = value;

                if (File.Exists(localSaveFilePath))
                {
                    using (FileStream fileStream = File.OpenWrite(localSaveFilePath))
                    {
                        byte[] byteArray = Encoding.UTF8.GetBytes(SaveFileJSON.ToString());
                        fileStream.Write(byteArray, 0, byteArray.Length);
                        fileStream.Close();
                    }
                    return true;
                }
            }
            return false;
        }

        public void UpdateVariableChangedSubscription(bool subscribe)
        {
            if (Data != null)
            {
                bool isSubscribed = Data.IsDelegateSubscribed(OverScriptData_OnVariablesListChanged);

                if (subscribe is true &&
                    isSubscribed is false)
                {
                    // Subscribe to the event
                    Data.OnVariablesListChanged -= OverScriptData_OnVariablesListChanged;
                    Data.OnVariablesListChanged += OverScriptData_OnVariablesListChanged;
                }
                else if (subscribe is false &&
                         isSubscribed is true)
                {
                    // Unsubscribe from the event
                    Data.OnVariablesListChanged -= OverScriptData_OnVariablesListChanged;
                }
            }
        }

        private void OverScriptData_OnVariablesListChanged(OverScriptData.VariableListType variableListType, OverVariableData overVariableData)
        {
            foreach (var script in managedScripts)
            {
                if (script != null && script.OverGraph != null)
                {
                    // Non so se servono entrambi, boh... buona fortuna nuovo responsabile dell'SDK
                    script.OverGraph.UpdateDataFromManager();
                    //script.OverGraph.ValidateInternalNodes();
                }
            }
        }
        public void ApplyVariableChangesTo(OverVariableData variable)
        {           
            foreach (OverScript script in managedScripts)
            {
                if (script != null && script.OverGraph != null)
                {
                    script.OverGraph.UpdateDataFromManager();
                }
            }
        }

        /******************** Managed scripts public ********************/

        public void AddManagedScript(OverScript scriptToAdd)
        {
            if (!managedScripts.Contains(scriptToAdd))
                managedScripts.Add(scriptToAdd);
        }

        public void RemoveManagedScript(OverScript scriptToAdd) => managedScripts.Remove(scriptToAdd);

       

        public void ConvertOldVariablesToNewList(bool alsoMangedScript = true)
        {
            // Manager variables
            int totalVariablesConverted = Data.UpdateOldVariableDataList();
            // script.OverGraph.UpdateDataFromManager();  This is done automatically after 

            if (alsoMangedScript)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    EditorUtility.SetDirty(this);
#endif

                // Scripts variables
                foreach (OverScript script in managedScripts)
                {
                    script.UpdateOldVariableList();
#if UNITY_EDITOR
                    if (!Application.isPlaying)
                        EditorUtility.SetDirty(script);
#endif     
                }
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    AssetDatabase.SaveAssets();               
#endif
            }
        }

    }
}