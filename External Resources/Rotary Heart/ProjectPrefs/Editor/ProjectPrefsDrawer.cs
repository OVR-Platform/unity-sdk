using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace RotaryHeart.Lib.ProjectPreferences
{
    /// <summary>
    /// Editor window that can be used to manage the data stored.
    /// </summary>
    public class ProjectPrefsDrawer : EditorWindow
    {
        enum SortBy
        {
            Section, Key, Value
        }

        /// <summary>
        /// Base class used to hold the actual data stored with a key
        /// </summary>
        public class Data
        {
            public string Section { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }

            /// <summary>
            /// Base class used to hold the actual data stored with a key.
            /// </summary>
            /// <param name="section">Section where the data is stored.</param>
            /// <param name="key">Key used to save the data.</param>
            /// <param name="value">Value stored.</param>
            public Data(string section, string key, string value)
            {
                Section = section;
                Key = key;
                Value = value;
                Selected = false;
            }
        }

        GUIContent sortAscContent = new GUIContent("Sort ▲", "Change sort order");
        GUIContent sortDescContent = new GUIContent("Sort ▼", "Change sort order");

        SortBy sortBy = SortBy.Section;
        bool ascending;
        string filter;
        SortBy filterOption;
        bool selectAll;
        Vector2 scrollPos;

        List<Data> data = new List<Data>();

        [MenuItem("Window/Rotary Heart/Project Prefs/Project Prefs")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            ProjectPrefsDrawer window = (ProjectPrefsDrawer)EditorWindow.GetWindow(typeof(ProjectPrefsDrawer));
            window.titleContent = new GUIContent("Project Prefs");
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

            using (EditorGUI.ChangeCheckScope changeCheck = new EditorGUI.ChangeCheckScope())
            {
                using (new GUILayout.HorizontalScope())
                {
                    GUI.enabled = false;
                    string tmp = EditorGUILayout.TextField("Project Prefs Path",
                                                           PlayerPrefs.GetString(
                                                               "ProjectPrefsPath",
                                                               System.IO.Path.GetDirectoryName(
                                                                   Application.dataPath + "/../ProjectSettings/") +
                                                               "/ProjectPreferences.ini"));
                    GUI.enabled = true;

                    if (GUILayout.Button("...", GUILayout.Width(35)))
                    {
                        string path = EditorUtility.OpenFolderPanel("Select path", "", "");

                        if (!string.IsNullOrEmpty(path))
                        {
                            PlayerPrefs.SetString("ProjectPrefsPath", path + "/ProjectPreferences.ini");
                            ProjectPrefs.ResetParser();
                            Refresh();
                        }
                    }
                    else if (changeCheck.changed)
                    {
                        PlayerPrefs.SetString("ProjectPrefsPath", tmp);
                        ProjectPrefs.ResetParser();
                        Refresh();
                    }
                }
            }

            using (new GUILayout.HorizontalScope(GUILayout.Width(position.width - 20)))
            {
                bool temp = EditorGUILayout.Toggle(selectAll, GUILayout.Width(25));
                GUI.enabled = false;
                GUI.color *= 1.5f;
                EditorGUILayout.TextField("Section", EditorStyles.label);
                EditorGUILayout.TextField("Key", EditorStyles.label);
                EditorGUILayout.TextField("Value", EditorStyles.label);
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

                        dataEntry.Section = EditorGUILayout.TextField(dataEntry.Section);
                        if (string.IsNullOrEmpty(dataEntry.Section))
                        {
                            EditorGUI.LabelField(GUILayoutUtility.GetLastRect(), "No Section",
                                                 EditorStyles.toolbarTextField);
                        }

                        dataEntry.Key = EditorGUILayout.TextField(dataEntry.Key);
                        dataEntry.Value = EditorGUILayout.TextField(dataEntry.Value);
                        GUI.color = prevColor;

                        GUI.color *= 0;
                        if (GUILayout.Button(new GUIContent("", "Save"), EditorStyles.toolbarButton,
                                             GUILayout.Width(20)))
                        {
                            ProjectPrefs.SetString(dataEntry.Section, dataEntry.Key, dataEntry.Value);
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
                            ProjectPrefs.RemoveKey(dataEntry.Section, dataEntry.Key);
                            Refresh();
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

            foreach (var subSection in ProjectPrefs.GetSections())
            {
                foreach (var key in ProjectPrefs.GetKeys(subSection))
                {
                    data.Add(new Data(subSection, key, ProjectPrefs.GetString(subSection, key)));
                }
            }
        }

        /// <summary>
        /// Change the current sort of the elements
        /// </summary>
        void SortItems()
        {
            switch (sortBy)
            {
                case SortBy.Section:
                    if (!ascending)
                        data.Sort((x, y) => x.Section.CompareTo(y.Section));
                    else
                        data.Sort((x, y) => y.Section.CompareTo(x.Section));
                    break;

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
                case SortBy.Section:
                    data = data.Where(c => c.Section.ToLower().Contains(filter.ToLower())).ToList();
                    break;

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
            data.Add(new Data("", "", ""));
        }

        /// <summary>
        /// Exports the ini file
        /// </summary>
        void OnExport()
        {
            var path = EditorUtility.SaveFilePanel("Export prefs", "", "ProjectPrefs.ini", "ini");

            if (path.Length != 0)
            {
                System.IO.File.Copy(ProjectPrefs.AssetPath, path);
            }
        }

        /// <summary>
        /// Exports only the selected elements
        /// </summary>
        void OnExportSelected()
        {
            var path = EditorUtility.SaveFilePanel("Export prefs", "", "ProjectPrefs.ini", "ini");

            if (path.Length != 0)
            {
                IniParser.DictionaryIniParser parser = new IniParser.DictionaryIniParser();

                var selectedElements = data.Where(c => c.Selected).ToList();

                foreach (var element in selectedElements)
                {
                    parser.Set(element.Section, element.Key, element.Value);
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
                ProjectPrefs.Clear();
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
                var selectedElements = data.Where(c => c.Selected).ToList();

                foreach (var element in selectedElements)
                {
                    ProjectPrefs.RemoveKey(element.Section, element.Key);
                }

                Refresh();
            }
        }
    }
}