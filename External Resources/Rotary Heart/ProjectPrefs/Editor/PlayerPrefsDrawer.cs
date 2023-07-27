using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using UnityEngine;
using UnityEditor;

namespace RotaryHeart.Lib.ProjectPreferences
{
    public class PlayerPrefsDrawer : EditorWindow
    {
        enum SortBy
        {
            Key, Value
        }

        /// <summary>
        /// Base class used to hold the actual data stored with a key
        /// </summary>
        public class Data
        {
            public enum DataType
            {
                _string, _int, _float
            }
            
            public string Key { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
            public DataType Type { get; set; }
            public bool Custom { get; private set; }

            /// <summary>
            /// Base class used to hold the actual data stored with a key.
            /// </summary>
            /// <param name="key">Key used to save the data.</param>
            /// <param name="value">Value stored.</param>
            /// <param name="type">Type of the value</param>
            /// <param name="custom">Custom added</param>
            public Data(string key, string value, DataType type, bool custom)
            {
                Key = key;
                Value = value;
                Selected = false;
                Type = type;
                Custom = custom;
            }
        }

        GUIContent sortAscContent = new GUIContent("Sort ▲", "Change sort order");
        GUIContent sortDescContent = new GUIContent("Sort ▼", "Change sort order");

        SortBy sortBy = SortBy.Key;
        bool ascending;
        string filter;
        SortBy filterOption;
        bool selectAll;
        Vector2 scrollPos;

        List<Data> data = new List<Data>();

        [MenuItem("Window/Rotary Heart/Project Prefs/Player Prefs")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            PlayerPrefsDrawer window = (PlayerPrefsDrawer)EditorWindow.GetWindow(typeof(PlayerPrefsDrawer));
            window.titleContent = new GUIContent("Player Prefs");
            window.minSize = new Vector2(610, 200);
            window.Refresh();
            window.Show();
        }

        void OnGUI()
        {
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar, GUILayout.Width(position.width)))
            {
                EditorGUILayout.LabelField("Sort By", GUILayout.Width(50));
                SortBy tempSortBy =
                    (SortBy)EditorGUILayout.EnumPopup(sortBy, EditorStyles.toolbarDropDown, GUILayout.Width(75));
                bool tempSort = GUILayout.Toggle(ascending, ascending ? sortAscContent : sortDescContent,
                                                 EditorStyles.toolbarButton, GUILayout.Width(50));

                if (tempSortBy != sortBy || tempSort != ascending)
                {
                    ascending = tempSort;
                    sortBy = tempSortBy;
                    SortItems();
                }

                if (GUILayout.Button("Options", EditorStyles.toolbarButton, GUILayout.Width(50)))
                {
                    GenericMenu menu = new GenericMenu();

                    menu.AddItem(new GUIContent("New"), false, OnNew);

                    menu.AddSeparator("");

                    menu.AddItem(new GUIContent("Export"), false, OnExport);
                    menu.AddItem(new GUIContent("Export Selected"), false, OnExportSelected);

                    menu.AddSeparator("");

                    menu.AddItem(new GUIContent("Delete All"), false, OnDeleteAll);
                    menu.AddItem(new GUIContent("Delete selected"), false, OnDeleteSelected);
                    
                    //TODO: Add import logic here
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Import"), false, OnImport);

                    menu.ShowAsContext();
                }

                string filterTemp =
                    EditorGUILayout.TextField(filter, EditorStyles.toolbarTextField,
                                              GUILayout.Width(position.width - 405));

                if (string.IsNullOrEmpty(filter))
                {
                    filter = "";
                    EditorGUI.LabelField(GUILayoutUtility.GetLastRect(), "Filter");
                }

                EditorGUILayout.LabelField("Filter By", GUILayout.Width(60));
                SortBy filterOptionTemp =
                    (SortBy)EditorGUILayout.EnumPopup(filterOption, EditorStyles.toolbarDropDown, GUILayout.Width(75));

                if (filterTemp != null && filter != null &&
                    (!filterTemp.Equals(filter) || filterOption != filterOptionTemp))
                {
                    filter = filterTemp;
                    filterOption = filterOptionTemp;
                    Filter();
                }

                if (GUILayout.Button(new GUIContent("", "Refresh"), EditorStyles.toolbarButton, GUILayout.Width(20)))
                {
                    Refresh();
                }

                EditorGUI.LabelField(GUILayoutUtility.GetLastRect(), EditorGUIUtility.IconContent("d_RotateTool On"));
            }

            GUI.enabled = false;
#if UNITY_EDITOR_OSX
            string plistFilename = string.Format("unity.{0}.{1}.plist", PlayerSettings.companyName, PlayerSettings.productName);
            string playerPrefsPath = System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Library/Preferences"),
                plistFilename);
            EditorGUILayout.TextField("Player Prefs Path", playerPrefsPath);
#else
            EditorGUILayout.TextField("Player Prefs Path", "Computer\\HKEY_CURRENT_USER\\Software\\Unity\\UnityEditor\\" + Application.companyName + "\\" + Application.productName);
#endif
            GUI.enabled = true;

            using (new GUILayout.HorizontalScope(GUILayout.Width(position.width - 20)))
            {
                bool temp = EditorGUILayout.Toggle(selectAll, GUILayout.Width(25));
                GUI.enabled = false;
                GUI.color *= 1.5f;
                EditorGUILayout.TextField("Key", EditorStyles.label);
                EditorGUILayout.TextField("Value", EditorStyles.label);
                EditorGUILayout.TextField("Type", EditorStyles.label);
                EditorGUILayout.TextField("", EditorStyles.label, GUILayout.Width(40));
                GUI.color *= (1f / 1.5f);
                GUI.enabled = true;

                if (temp != selectAll)
                {
                    selectAll = temp;

                    foreach (var dataEntry in data)
                    {
                        dataEntry.Selected = selectAll;
                    }
                }
            }

            using (EditorGUILayout.ScrollViewScope scrollViewScope =
                new EditorGUILayout.ScrollViewScope(scrollPos, false, false))
            {
                scrollPos = scrollViewScope.scrollPosition;

                foreach (Data dataEntry in data)
                {
                    using (new EditorGUILayout.HorizontalScope(GUILayout.Width(position.width - 20)))
                    {
                        dataEntry.Selected = EditorGUILayout.Toggle(dataEntry.Selected, GUILayout.Width(25));
                        Color prevColor = GUI.color;
                        if (!dataEntry.Selected)
                        {
                            GUI.color = Color.white * 0.88f;
                        }

                        if (!PlayerPrefs.HasKey(dataEntry.Key))
                            GUI.color = new Color(1f, 0.51f, 0.24f);

                        dataEntry.Key = EditorGUILayout.TextField(dataEntry.Key);
                        dataEntry.Value = EditorGUILayout.TextField(dataEntry.Value);

                        GUI.enabled = dataEntry.Custom;
                        dataEntry.Type = (Data.DataType)EditorGUILayout.EnumPopup(dataEntry.Type);
                        GUI.enabled = true;
                        GUI.color = prevColor;

                        GUI.color *= 0;
                        if (GUILayout.Button(new GUIContent("", "Save"), EditorStyles.toolbarButton,
                                             GUILayout.Width(20)))
                        {
                            switch (dataEntry.Type)
                            {
                                case Data.DataType._int:
                                    PlayerPrefs.SetInt(dataEntry.Key, int.Parse(dataEntry.Value));
                                    break;
                                
                                case Data.DataType._float:
                                    PlayerPrefs.SetFloat(dataEntry.Key, float.Parse(dataEntry.Value));
                                    break;
                                
                                case Data.DataType._string:
                                    PlayerPrefs.SetString(dataEntry.Key, dataEntry.Value);
                                    break;
                            }

                            Refresh();
                            break;
                        }

                        GUI.color = prevColor;
                        Rect rect = GUILayoutUtility.GetLastRect();
                        rect.y += 1;
                        rect.x += 2;
                        EditorGUI.LabelField(rect, EditorGUIUtility.IconContent("Collab"));
                        GUI.color *= 0;
                        if (GUILayout.Button(new GUIContent("", "Erase"), EditorStyles.toolbarButton,
                                             GUILayout.Width(20)))
                        {
                            PlayerPrefs.DeleteKey(dataEntry.Key);
                            Refresh();
                            break;
                        }

                        GUI.color = prevColor;
                        rect = GUILayoutUtility.GetLastRect();
                        rect.y += 1;
                        rect.x += 2;

                        EditorGUI.LabelField(rect, EditorGUIUtility.IconContent("winbtn_mac_close_h"));
                    }
                }
            }
        }

        /// <summary>
        /// Refreshes the editor information
        /// </summary>
        void Refresh()
        {
            data.Clear();

#if UNITY_EDITOR_OSX
            string plistFilename = string.Format("unity.{0}.{1}.plist", PlayerSettings.companyName, PlayerSettings.productName);
            string playerPrefsPath = System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Library/Preferences"),
                plistFilename);
            
            // Parse the player prefs file if it exists
            if (System.IO.File.Exists(playerPrefsPath))
            {
                // Parse the plist then cast it to a Dictionary
                object plist = Plist.readPlist(playerPrefsPath);

                Dictionary<string, object> parsed = plist as Dictionary<string, object>;

                foreach (KeyValuePair<string, object> keyValuePair in parsed)
                {
                    if (keyValuePair.Value is int)
                    {
                        data.Add(new Data(keyValuePair.Key, PlayerPrefs.GetInt(keyValuePair.Key).ToString(), Data.DataType._int, false));
                    }
                    else if (keyValuePair.Value is string)
                    {
                        data.Add(new Data(keyValuePair.Key, PlayerPrefs.GetString(keyValuePair.Key), Data.DataType._string, false));
                    }
                    else
                    {
                        data.Add(new Data(keyValuePair.Key, PlayerPrefs.GetFloat(keyValuePair.Key).ToString(), Data.DataType._int, false));
                    }
                }
            }
#else
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Unity\\UnityEditor\\" + Application.companyName + "\\" + Application.productName))
            {
                if (key != null)
                {
                    foreach (string valueName in key.GetValueNames())
                    {
                        string prefName = valueName.Substring(0, valueName.LastIndexOf("_"));

                        switch (key.GetValueKind(valueName))
                        {
                            case RegistryValueKind.DWord:
                                
                                if (PlayerPrefs.GetInt(prefName, System.Int32.MinValue) == System.Int32.MinValue)
                                    data.Add(new Data(prefName, PlayerPrefs.GetFloat(prefName).ToString(), Data.DataType._float, false));
                                else
                                    data.Add(new Data(prefName, PlayerPrefs.GetInt(prefName).ToString(), Data.DataType._int, false));
                                
                                break;
                            
                            case RegistryValueKind.Binary:
                                data.Add(new Data(prefName, PlayerPrefs.GetString(prefName), Data.DataType._string, false));
                                break;
                        }
                    }
                }
            }
#endif
        }

        /// <summary>
        /// Change the current sort of the elements
        /// </summary>
        void SortItems()
        {
            switch (sortBy)
            {
                case SortBy.Key:
                    if (!ascending)
                        data.Sort((x, y) => x.Key.CompareTo(y.Key));
                    else
                        data.Sort((x, y) => y.Key.CompareTo(x.Key));
                    break;

                case SortBy.Value:
                    if (!ascending)
                        data.Sort((x, y) => x.Value.CompareTo(y.Value));
                    else
                        data.Sort((x, y) => y.Value.CompareTo(x.Value));
                    break;
            }
        }

        /// <summary>
        /// Filters the elements based on the text to search
        /// </summary>
        void Filter()
        {
            Refresh();

            if (string.IsNullOrEmpty(filter))
                return;

            switch (filterOption)
            {
                case SortBy.Key:
                    data = data.Where(c => c.Key.ToLower().Contains(filter.ToLower())).ToList();
                    break;

                case SortBy.Value:
                    data = data.Where(c => c.Value.ToLower().Contains(filter.ToLower())).ToList();
                    break;
            }

            SortItems();
        }

        /// <summary>
        /// Adds an empty data
        /// </summary>
        void OnNew()
        {
            data.Add(new Data("", "", Data.DataType._string, true));
        }

        /// <summary>
        /// Imports the data to the player prefs
        /// </summary>
        void OnImport()
        {
            string path = EditorUtility.OpenFilePanel("Export prefs", "", "ini");

            if (path.Length != 0)
            {
                IniParser.DictionaryIniParser parser = new IniParser.DictionaryIniParser(path);

                foreach (string key in parser.GetKeys("PlayerPrefs"))
                {
                    string value = parser.GetValue("PlayerPrefs", key);
                    float val;
                    if (float.TryParse(value, out val) && !value.Contains(","))
                    {
                        if (value.Contains("."))
                            PlayerPrefs.SetFloat(key, val);
                        else
                            PlayerPrefs.SetInt(key, int.Parse(value));
                    }
                    else
                    {
                        PlayerPrefs.SetString(key, value);
                    }
                }
                
                parser.Clear();
                parser = null;
            }

            Refresh();
        }
        
        /// <summary>
        /// Exports the player prefs
        /// </summary>
        void OnExport()
        {
            string path = EditorUtility.SaveFilePanel("Export prefs", "", "PlayerPrefs.ini", "ini");
            
            if (path.Length != 0)
            {
                IniParser.DictionaryIniParser parser = new IniParser.DictionaryIniParser();
            
                foreach (Data element in data)
                {
                    parser.Set("PlayerPrefs", element.Key, element.Value);
                }
            
                parser.Save(path);
                parser.Clear();
                parser = null;
            }
        }

        /// <summary>
        /// Exports only the selected elements
        /// </summary>
        void OnExportSelected()
        {
            string path = EditorUtility.SaveFilePanel("Export prefs", "", "PlayerPrefs.ini", "ini");
            
            if (path.Length != 0)
            {
                IniParser.DictionaryIniParser parser = new IniParser.DictionaryIniParser();
            
                List<Data> selectedElements = data.Where(c => c.Selected).ToList();
            
                foreach (Data element in selectedElements)
                {
                    parser.Set("PlayerPrefs", element.Key, element.Value);
                }
            
                parser.Save(path);
                parser.Clear();
                parser = null;
            }
        }

        /// <summary>
        /// Deletes all the data stored
        /// </summary>
        void OnDeleteAll()
        {
            if (EditorUtility.DisplayDialog("Delete", "You are about to delete all the keys, this cannot be undone. Are you sure?", "Yes", "Cancel"))
            {
                PlayerPrefs.DeleteAll();
                Refresh();
            }
        }

        /// <summary>
        /// Deletes only the selected elements
        /// </summary>
        void OnDeleteSelected()
        {
            if (EditorUtility.DisplayDialog("Delete", "You are about to delete all the selected keys, this cannot be undone. Are you sure?", "Yes", "Cancel"))
            {
                List<Data> selectedElements = data.Where(c => c.Selected).ToList();

                foreach (Data element in selectedElements)
                {
                    PlayerPrefs.DeleteKey(element.Key);
                }

                Refresh();
            }
        }
    }
}