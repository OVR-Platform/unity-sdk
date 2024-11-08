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
using OverSimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    public class OverUtilityUVS
    {

        public JSONNode SaveFileJSON { get; private set; }
        [SerializeField][ReadOnly] string localSaveFilePath;

        public static Func<string> GetEnvironmentIndex = null;

        private static OverUtilityUVS main = null;
        public static OverUtilityUVS Main
        {
            get
            {
                if (main == null)
                {
                    main = new OverUtilityUVS();
                    
                }
                return main;
            }
        }

        public OverUtilityUVS()
        {
            GetOrCreateInternalSaveFile();
        }

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

        public static Dictionary<TKey, string> AotDictionaryToDictionary<TKey>(AotDictionary aotDict)
        {
            if (aotDict == null)
                return null;

            Dictionary<TKey, string> result = new(capacity: aotDict.Count);
            string expectedKeyTypeName = typeof(TKey).Name;
            foreach (DictionaryEntry entry in aotDict)
            {
                if (entry.Key is TKey convertedKey)
                {
                    if(entry.Value is string)
                    {
                        result[convertedKey] = (string)entry.Value;
                    }
                    else
                    {
                        throw new System.Exception($"All dictionary values must be of type STRING");
                    }
                    
                }
                else
                {
                    throw new System.Exception($"All dictionary keys must be of type {expectedKeyTypeName}, but found {entry.Key?.GetType().Name ?? "null"} (value={entry.Key?.ToString() ?? "null"})");
                }
            }

            return result;
        }

    }
}
