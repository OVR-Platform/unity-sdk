/**
 * OVR Unity SDK License
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
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [Serializable]
    public class OverDataMapping
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
                    main = (OverScriptManager)FindObjectOfType(typeof(OverScriptManager));
                return main;
            }
        }

        public Dictionary<string, OverDataMapping> overDataMappings = new Dictionary<string, OverDataMapping>();
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

        [SerializeField][HideInInspector] List<OverGraphVariableData> ghostVariables = new List<OverGraphVariableData>();

        [Header("Debug")]
        [SerializeField] public List<ErrorScriptMessage> errors = new List<ErrorScriptMessage>();

        public static Func<string> GetEnvironmentIndex = null;

        public JSONNode SaveFileJSON { get; private set; }
        [SerializeField][ReadOnly] string localSaveFilePath;

        private void OnValidate()
        {
            RefreshGlobals();
        }

        public void RefreshGlobals()
        {
            Dictionary<string, OverVariableData> dict = Data.VariableDict;
            if (ghostVariables.Count != Data.variables.Count)
            {
                foreach (OverScript managed in managedScripts)
                {
                    managed.OverGraph.ValidateInternalNodes();
                }

                ghostVariables = new List<OverGraphVariableData>(Data.variables.Select(x => x.ToGraphData()));
            }
        }

        public void ApplyVariableChangesTo(OverVariableData variable)
        {
            foreach (OverScript managed in managedScripts)
            {
                managed.OverGraph.ValidateInternalNodes();
            }

            ghostVariables = new List<OverGraphVariableData>(Data.variables.Select(x => x.ToGraphData()));
        }

        private void Awake()
        {
            if (IsError)
            {
                DisplayErrors();
                return;
            }

            UpdateMappings();

            GetOrCreateInternalSaveFile();
        }

#if UNITY_EDITOR && !APP_MAIN  
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void DidReloadScripts()
        {
            if (OverScriptManager.Main != null)
                OverScriptManager.Main.UpdateMappings();
        }
#endif

        public void UpdateMappings()
        {
            overDataMappings.Clear();
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
                        //errors.Add(new ErrorScriptMessage(ScriptManagementError.MultipleScript, script));
                        script.GUID = Guid.NewGuid().ToString();
                    }
                }
            }

            DisplayErrors();

            foreach (OverScript script in scripts)
            {
                if (script.OverGraph != null)
                {
                    OverDataMapping mapping = new OverDataMapping()
                    {
                        overGraphAsset = script.OverGraph,
                        overScript = script
                    };

                    if (!overDataMappings.ContainsKey(mapping.overScript.GUID))
                    {
                        overDataMappings.Add(mapping.overScript.GUID, mapping);
                        managedScripts.Add(script);
                    }
                    else
                    {
                        overDataMappings[mapping.overScript.GUID] = mapping;
                    }
                }
            }
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
    }
}