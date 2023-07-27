using RotaryHeart.Lib.IniParser.Data;
using System;
using System.IO;

namespace RotaryHeart.Lib.IniParser
{
    /// <summary>
    /// An .ini file parser that Creates and edits .ini files. With functions to fetch and delete values.
    /// </summary>
    public abstract class BaseIniParser
    {
        string m_path;

        #region Abstract data
        #region Properties
        /// <summary>
        /// How many keys are stored
        /// </summary>
        public abstract int Count { get; }
        #endregion

        /// <summary>
        /// Returns an array of all the sub sections on the file
        /// </summary>
        /// <returns>String array with all the sub sections</returns>
        public abstract string[] GetSubSections();

        /// <summary>
        /// Returns all the keys stored inside a sub section
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <returns>String array with all the keys on the sub section</returns>
        public abstract string[] GetKeys(string subSection);

        /// <summary>
        /// Returns all the values stored inside a sub section
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <returns>String array with all the values on the sub section</returns>
        public abstract string[] GetValues(string subSection);

        /// <summary>
        /// Check if the parser contains the specified sub section
        /// </summary>
        /// <param name="subSection">The sub section to search</param>
        /// <returns>true if the sub section was found; otherwise, false</returns>
        public abstract bool ContainsSubSection(string subSection);

        /// <summary>
        /// Check if the parser contains the specified key in the specified subsection
        /// </summary>
        /// <param name="subSection">Sub section to search from</param>
        /// <param name="key">The key to search</param>
        /// <returns>true if the key was found; otherwise, false</returns>
        public abstract bool ContainsKey(string subSection, string key);

        /// <summary>
        /// Set the variable and value if they dont exist including a comment. Updates the variable value and comment if does exist
        /// </summary>
        /// <param name="subSection">Section this key belongs to</param>
        /// <param name="keyData">Key information to be saved</param>
        public abstract void Set(string subSection, KeyData keyData);

        /// <summary>
        /// Get the specified key from a subSection.
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <param name="key">Key name</param>
        /// <param name="value">When this method return, contains the value with the specific subSection and key, if the key is found; otherwise is null</param>
        public abstract bool TryGet(string subSection, string key, out KeyData value);

        /// <summary>
        /// Remove the specified key from the specified subSection. Returns removal success flag
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <param name="key">Key name</param>
        /// <param name="value">When this method return, contains the value with the specific subSection and key, if the key is found; otherwise is null</param>
        public abstract bool Remove(string subSection, string key, out KeyData value);

        /// <summary>
        /// Saves the file
        /// </summary>
        /// <param name="path">The path to the file</param>
        public abstract void Save(string path);

        /// <summary>
        /// Clear the instance
        /// </summary>
        public abstract void Clear();
        #endregion

        /// <summary>
        /// Loads the .ini data from the file specified, uses the <paramref name="path"/> as the new path for the file
        /// </summary>
        /// <param name="data">File data to use</param>
        /// <param name="path">Path to the file</param>
        public bool LoadData(string data, string path)
        {
            Clear();

            string section = "";

            using (StringReader sr = new StringReader(data))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    int subsec = line.IndexOf("[");
                    int offset = line.IndexOf("=");
                    int comment = line.IndexOf(";");

                    if (subsec == 0)
                    {
                        section = line.Substring(1, line.Length - 2);
                    }

                    if (offset > 0)
                    {
                        string key = line.Substring(0, offset);

                        if (comment != -1)
                        {
                            string val = line.Substring(offset + 1, (comment - (offset + 1)));
                            val = val.Replace("\t", " ");
                            Set(section, key, val, line.Substring(comment + 1).TrimStart(' '));
                        }
                        else
                        {
                            Set(section, key, line.Substring(offset + 1));
                        }
                    }
                }

                m_path = path;

                if (path != null)
                    Save();

                return true;
            }
        }

        /// <summary>
        /// Loads the .ini file specified
        /// </summary>
        /// <param name="path">Path to the file</param>
        public bool Load(string path)
        {
            Clear();

            if (!File.Exists(path))
            {
                throw new Exception("File not found at path: " + path);
            }

            string section = "";
            string dir = path;

            using (StreamReader sr = new StreamReader(dir))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    int subsec = line.IndexOf("[");
                    int offset = line.IndexOf("=");
                    int comment = line.IndexOf(";");

                    if (subsec == 0)
                    {
                        section = line.Substring(1, line.Length - 2);
                    }

                    if (offset > 0)
                    {
                        string key = line.Substring(0, offset);

                        if (comment != -1)
                        {
                            string val = line.Substring(offset + 1, (comment - (offset + 1)));
                            val = val.Replace("\t", " ");
                            Set(section, key, val, line.Substring(comment + 1).TrimStart(' '));
                        }
                        else
                        {
                            Set(section, key, line.Substring(offset + 1));
                        }
                    }
                }

                m_path = path;

                return true;
            }
        }

        /// <summary>
        /// Saves the file, use this if a file has already been loaded
        /// </summary>
        public void Save()
        {
            if (string.IsNullOrEmpty(m_path))
            {
                throw new Exception("Parser hasn't loaded any file, use Save(string) instead.");
            }

            Save(m_path);
        }

        /// <summary>
        /// Set the variable and value if they dont exist including a comment. Updates the variable value and comment if does exist
        /// </summary>
        /// <param name="subSection">Section this key belongs to</param>
        /// <param name="key">The variable name</param>
        /// <param name="value">The value of the variable</param>
        /// <param name="comment">Optional comment of the variable</param>
        public void Set(string subSection, string key, string value, string comment = "")
        {
            Set(subSection, new KeyData(key, value, comment));
        }

        /// <summary>
        /// Get the value KeyData from the specified key and subSection.
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <param name="key">Key name</param>
        /// <returns>The KeyData</returns>
        public KeyData Get(string subSection, string key)
        {
            KeyData val;
            if (TryGet(subSection, key, out val))
                return val;

            return null;
        }

        /// <summary>
        /// Get the value from the specified key and subsection
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <param name="key">Key name</param>
        /// <returns>The value</returns>
        public string GetValue(string subSection, string key)
        {
            var val = Get(subSection, key);

            if (val != null)
                return val.Value;
            
            return null;
        }

        /// <summary>
        /// Get the comment from the specified key and subsection
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <param name="key">Key name</param>
        /// <returns>The comment</returns>
        public string GetComment(string subSection, string key)
        {
            var val = Get(subSection, key);

            if (val != null)
                return val.Comment;
            
            return null;
        }

        /// <summary>
        /// Remove the specified key from the specified subSection. Returns removal success flag
        /// </summary>
        /// <param name="subSection">Sub section name</param>
        /// <param name="key">Key name</param>
        public bool Remove(string subSection, string key)
        {
            KeyData removed;
            return Remove(subSection, key, out removed);
        }

    }
}
