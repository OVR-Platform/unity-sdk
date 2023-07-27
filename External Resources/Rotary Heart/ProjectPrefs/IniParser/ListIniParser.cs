using RotaryHeart.Lib.IniParser.Data;
using System.Collections.Generic;
using System.IO;

namespace RotaryHeart.Lib.IniParser
{
    /// <summary>
    /// An .ini file parser that uses Lists. This should be used when a data needs to be loaded on the order that is written.
    /// </summary>
    public class ListIniParser : BaseIniParser
    {
        public class ListSubSection : BaseSubSectionData
        {
            public List<KeyData> Data { get; private set; }

            public ListSubSection()
            {
                Data = new List<KeyData>();
            }

            public override string ToString()
            {
                string data = "[" + SubSection + "]";

                foreach (KeyData subSection in Data)
                {
                    data += "{" + subSection.Value + "}, ";
                }

                return data;
            }
        }

        Dictionary<string, ListSubSection> m_data = new Dictionary<string, ListSubSection>();

        /// <summary>
        /// How many elements does the system has loaded.
        /// </summary>
        public override int Count
        {
            get { return m_data.Count; }
        }

        /// <summary>
        /// New instance without loading any file.
        /// </summary>
        public ListIniParser() { }

        /// <summary>
        /// Creates an instance from the <paramref name="data"/> sent and set the save path to <paramref name="path"/>
        /// </summary>
        /// <param name="data">Data to load</param>
        /// <param name="path">File path</param>
        public ListIniParser(string data, string path)
        {
            LoadData(data, path);
        }

        /// <summary>
        /// Creates an instance and loads a file on the specified path.
        /// </summary>
        /// <param name="path">File path</param>
        public ListIniParser(string path)
        {
            Load(path);
        }

        /// <summary>
        /// Get the entire sub section
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        public ListSubSection GetSubSection(string subSection)
        {
            ListSubSection section;
            if (TryGetSubSection(subSection, out section))
                return section;

            return null;
        }

        /// <summary>
        /// Get the specified subSection
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <param name="value">When this method return, contains the value with the specific subSection and key, if the key is found; otherwise is null</param>
        public bool TryGetSubSection(string subSection, out ListSubSection value)
        {
            value = null;

            if (m_data.TryGetValue(subSection, out value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes the specified subsection
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <returns>returns true if the sub section was removed; otherwise, false</returns>
        public bool RemoveSubSection(string subSection)
        {
            ListSubSection removedSection;
            return RemoveSubSection(subSection, out removedSection);
        }

        /// <summary>
        /// Removes the specified subsection
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <param name="section">When this method return, contains the entire sub section, if the key is found; otherwise is null</param>
        /// <returns></returns>
        public bool RemoveSubSection(string subSection, out ListSubSection section)
        {
            section = null;

            if (m_data.ContainsKey(subSection))
            {
                section = m_data[subSection];
                m_data.Remove(subSection);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns an array of all the sub sections on the file
        /// </summary>
        /// <returns>String array with all the sub sections</returns>
        public override string[] GetSubSections()
        {
            string[] subSections = new string[m_data.Count];

            int i = 0;
            foreach (string section in m_data.Keys)
            {
                subSections[i] = section;
                i++;
            }

            return subSections;
        }

        /// <summary>
        /// Returns all the keys stored inside a sub section
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <returns>String array with all the keys on the sub section</returns>
        public override string[] GetKeys(string subSection)
        {
            ListSubSection section;
            if (!TryGetSubSection(subSection, out section))
                return null;

            string[] keys = new string[section.Data.Count];

            int i = 0;
            foreach (KeyData data in section.Data)
            {
                keys[i] = data.Key;
                i++;
            }

            return keys;
        }

        /// <summary>
        /// Returns all the values stored inside a sub section
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <returns>String array with all the values on the sub section</returns>
        public override string[] GetValues(string subSection)
        {
            ListSubSection section;
            if (!TryGetSubSection(subSection, out section))
                return null;

            string[] values = new string[section.Data.Count];

            int i = 0;
            foreach (KeyData data in section.Data)
            {
                values[i] = data.Value;
                i++;
            }

            return values;
        }

        /// <summary>
        /// Check if the parser contains the specified sub section
        /// </summary>
        /// <param name="subSection">The sub section to search</param>
        /// <returns>true if the sub section was found; otherwise, false</returns>
        public override bool ContainsSubSection(string subSection)
        {
            ListSubSection containsSection;
            return TryGetSubSection(subSection, out containsSection);
        }

        /// <summary>
        /// Check if the parser contains the specified key in the specified subsection
        /// </summary>
        /// <param name="subSection">Sub section to search from</param>
        /// <param name="key">The key to search</param>
        /// <returns>true if the key was found; otherwise, false</returns>
        public override bool ContainsKey(string subSection, string key)
        {
            ListSubSection section;
            if (!TryGetSubSection(subSection, out section))
                return false;

            bool contains = false;

            foreach (KeyData entry in section.Data)
            {
                if (entry.Key.Equals(key))
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }

        /// <summary>
        /// Set the variable and value if they dont exist including a comment. Updates the variable value and comment if does exist
        /// </summary>
        /// <param name="subSection">Section this key belongs to</param>
        /// <param name="keyData">Key information to be saved</param>
        public override void Set(string subSection, KeyData keyData)
        {
            ListSubSection section;
            //Check if the sub section exists
            if (m_data.TryGetValue(subSection, out section))
            {
                section.SubSection = subSection;

                int index = section.Data.FindIndex(data => data.Key.Equals(keyData.Key));

                //Check if the key exists inside this sub section
                if (index != -1)
                {
                    section.Data[index] = keyData;
                }
                //Create new key
                else
                {
                    section.Data.Add(keyData);
                }
            }
            //Create new sub section and add key
            else
            {
                section = new ListSubSection() { SubSection = subSection };
                section.Data.Add(keyData);
                m_data.Add(subSection, section);
            }
        }

        /// <summary>
        /// Get the specified key from a subSection.
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <param name="key">Key name</param>
        /// <param name="value">When this method return, contains the value with the specific subSection and key, if the key is found; otherwise is null</param>
        public override bool TryGet(string subSection, string key, out KeyData value)
        {
            value = null;

            ListSubSection section;
            if (TryGetSubSection(subSection, out section))
            {
                foreach (var entry in section.Data)
                {
                    if (entry.Key.Equals(key))
                    {
                        value = entry;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Remove the specified key from the specified subSection. Returns removal success flag
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <param name="key">Key name</param>
        /// <param name="value">When this method return, contains the value with the specific subSection and key, if the key is found; otherwise is null</param>
        public override bool Remove(string subSection, string key, out KeyData value)
        {
            value = null;

            ListSubSection section;
            //Check if the sub section exists
            if (!m_data.TryGetValue(subSection, out section))
            {
                throw new System.Exception("Sub section not found");
            }

            int index = section.Data.FindIndex(data => data.Key.Equals(key));

            //Check if the key exists
            if (index != -1)
            {
                throw new System.Exception("Key not found");
            }

            value = section.Data[index];

            section.Data.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Saves the file
        /// </summary>
        /// <param name="path">The path to the file</param>
        public override void Save(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (StreamWriter wr = new StreamWriter(path))
            {
                //Iterate the sub section dictionary
                foreach (KeyValuePair<string, ListSubSection> section in m_data)
                {
                    //Only add the subsection if it has any
                    if (!string.IsNullOrEmpty(section.Key))
                    {
                        wr.WriteLine("\n[" + section.Key + "]\n");
                    }

                    //Iterate the section data
                    foreach (KeyData sectionData in section.Value.Data)
                    {
                        //Get the key and value
                        string p1 = sectionData.Key + (string.IsNullOrEmpty(sectionData.Value) ? "" : "=" + sectionData.Value);

                        //Check if we should write comments
                        if (!string.IsNullOrEmpty(sectionData.Comment))
                        {
                            p1 += new string('\t', 1) + "; " + sectionData.Comment;
                        }

                        wr.WriteLine(p1);
                    }

                    wr.WriteLine("\n");
                }
            }
        }

        /// <summary>
        /// Clear the instance
        /// </summary>
        public override void Clear()
        {
            m_data = new Dictionary<string, ListSubSection>();
        }
    }
}
