using RotaryHeart.Lib.IniParser;
using System;
using UnityEngine;

namespace RotaryHeart.Lib.ProjectPreferences
{
    public class ProjectPrefs
    {
        //Path to the .ini file where the data is going to be saved.
        public static string AssetPath;

        //Holds the reference for the parser.
        private static DictionaryIniParser assetParser;

        /// <summary>
        /// Creates the reference for the parser, if the file doesn't exist it creates it.
        /// </summary>
        private static DictionaryIniParser Parser
        {
            get
            {
                if (assetParser == null)
                {
                    AssetPath = PlayerPrefs.GetString("ProjectPrefsPath", System.IO.Path.GetDirectoryName(Application.dataPath + "/../ProjectSettings/") + "/ProjectPreferences.ini");

                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(AssetPath));

                    if (!System.IO.File.Exists(AssetPath))
                    {
                        using (var stream = System.IO.File.Create(AssetPath))
                        {
                        }
                    }

                    assetParser = new DictionaryIniParser(AssetPath);
                }

                return assetParser;
            }
        }

        /// <summary>
        /// Reset the parser so that it reloads again
        /// </summary>
        public static void ResetParser()
        {
            assetParser = null;
        }

        /// <summary>
        /// Returns the sections saved on the preferences
        /// </summary>
        /// <returns>Array of sections</returns>
        public static string[] GetSections()
        {
            return Parser.GetSubSections();
        }

        /// <summary>
        /// Returns the keys saved on the specified section
        /// </summary>
        /// <param name="section">Section to search</param>
        /// <returns>Array of keys saved on the section</returns>
        public static string[] GetKeys(string section)
        {
            return Parser.GetKeys(section);
        }

        /// <summary>
        /// Returns the values stored on a section
        /// </summary>
        /// <param name="section">Section to search</param>
        /// <returns>Array of values saved on the section</returns>
        public static string[] GetValues(string section)
        {
            return Parser.GetValues(section);
        }

        #region Get
        /// <summary>
        /// Returns the string value corresponding to key in the preference file if it exists.
        /// </summary>
        /// <param name="section">Section to search</param>
        /// <param name="key">Key to search</param>
        /// <param name="defaultValue">Default value to be used if the key is not found</param>
        /// <returns>Value stored if the key is found; otherwise, <paramref name="defaultValue"/> is returned</returns>
        public static string GetString(string section, string key, string defaultValue = default(string))
        {
            IniParser.Data.KeyData keyData;
            string value;

            if (Parser.TryGet(section, key, out keyData))
            {
                value = keyData.Value;
            }
            else
            {
                value = defaultValue;
            }

            return value;
        }

        /// <summary>
        /// Returns the int value corresponding to key in the preference file if it exists.
        /// </summary>
        /// <param name="section">Section to search</param>
        /// <param name="key">Key to search</param>
        /// <param name="defaultValue">Default value to be used if the key is not found</param>
        /// <returns>Value stored if the key is found; otherwise, <paramref name="defaultValue"/> is returned</returns>
        public static int GetInt(string section, string key, int defaultValue = default(int))
        {
            var value = GetString(section, key);
            return value == null ? defaultValue : Convert.ToInt32(value);
        }

        /// <summary>
        /// Returns the float value corresponding to key in the preference file if it exists.
        /// </summary>
        /// <param name="section">Section to search</param>
        /// <param name="key">Key to search</param>
        /// <param name="defaultValue">Default value to be used if the key is not found</param>
        /// <returns>Value stored if the key is found; otherwise, <paramref name="defaultValue"/> is returned</returns>
        public static float GetFloat(string section, string key, float defaultValue = default(float))
        {
            var value = GetString(section, key);
            return value == null ? defaultValue : Convert.ToSingle(value);
        }

        /// <summary>
        /// Returns the bool value corresponding to key in the preference file if it exists.
        /// </summary>
        /// <param name="section">Section to search</param>
        /// <param name="key">Key to search</param>
        /// <param name="defaultValue">Default value to be used if the key is not found</param>
        /// <returns>Value stored if the key is found; otherwise, <paramref name="defaultValue"/> is returned</returns>
        public static bool GetBool(string section, string key, bool defaultValue = default(bool))
        {
            var value = GetString(section, key);
            return value == null ? defaultValue : Convert.ToBoolean(value);
        }
        #endregion

        #region Set
        /// <summary>
        /// Sets the string value of the preference identified by key.
        /// </summary>
        /// <param name="section">Section where the key is going to be stored</param>
        /// <param name="key">Key for the value</param>
        /// <param name="value">Value to be stored</param>
        public static void SetString(string section, string key, string value)
        {
            Parser.Set(section, key, value);
            Save();
        }

        /// <summary>
        /// Sets the int value of the preference identified by key.
        /// </summary>
        /// <param name="section">Section where the key is going to be stored</param>
        /// <param name="key">Key for the value</param>
        /// <param name="value">Value to be stored</param>
        public static void SetInt(string section, string key, int value)
        {
            SetString(section, key, Convert.ToString(value));
        }

        /// <summary>
        /// Sets the float value of the preference identified by key.
        /// </summary>
        /// <param name="section">Section where the key is going to be stored</param>
        /// <param name="key">Key for the value</param>
        /// <param name="value">Value to be stored</param>
        public static void SetFloat(string section, string key, float value)
        {
            SetString(section, key, Convert.ToString(value));
        }

        /// <summary>
        /// Sets the bool value of the preference identified by key.
        /// </summary>
        /// <param name="section">Section where the key is going to be stored</param>
        /// <param name="key">Key for the value</param>
        /// <param name="value">Value to be stored</param>
        public static void SetBool(string section, string key, bool value)
        {
            SetString(section, key, Convert.ToString(value));
        }
        #endregion

        /// <summary>
        /// Removes a section from the file
        /// </summary>
        /// <param name="section">Section to be removed</param>
        /// <returns>True if the section was removed successfully; otherwise, false</returns>
        public static bool RemoveSection(string section)
        {
            return Parser.RemoveSubSection(section);
        }

        /// <summary>
        /// Removes a key from a specific section
        /// </summary>
        /// <param name="section">Section that contains this key</param>
        /// <param name="key">Key to be removed</param>
        /// <returns>True if the key was removed successfully; otherwise, false</returns>
        public static bool RemoveKey(string section, string key)
        {
            bool value = Parser.Remove(section, key);
            Save();
            return value;
        }

        /// <summary>
        /// Saves the information to the file
        /// </summary>
        public static void Save()
        {
            Parser.Save();
        }

        /// <summary>
        /// Erases everything on the file
        /// </summary>
        public static void Clear()
        {
            Parser.Clear();
            Save();
        }

        /// <summary>
        /// Check if the file contains the specified section
        /// </summary>
        /// <param name="section">Section to search</param>
        /// <returns>True if the section was found; otherwise, false</returns>
        public static bool HasSection(string section)
        {
            return Parser.ContainsSubSection(section);
        }

        /// <summary>
        /// Check if the file contains a specific key
        /// </summary>
        /// <param name="section">Section where they key is stored</param>
        /// <param name="key">Key to search</param>
        /// <returns>True if the key was found; otherwise, false</returns>
        public static bool HasKey(string section, string key)
        {
            return Parser.ContainsKey(section, key);
        }
    }
}