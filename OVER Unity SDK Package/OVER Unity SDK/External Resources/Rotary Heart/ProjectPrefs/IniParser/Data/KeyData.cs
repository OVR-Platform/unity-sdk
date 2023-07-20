namespace RotaryHeart.Lib.IniParser.Data
{
    /// <summary>
    /// Class used to hold all the information for a specific key
    /// </summary>
    public class KeyData
    {
        /// <summary>
        /// The key associated for this entry
        /// </summary>
        public string Key { get; internal set; }
        /// <summary>
        /// The value associated for this entry
        /// </summary>
        public string Value { get; internal set; }
        /// <summary>
        /// The comment associated for this entry
        /// </summary>
        public string Comment { get; internal set; }

        /// <summary>
        /// Sets the default values for key, value and comment.
        /// </summary>
        public KeyData(string key, string value, string comment)
        {
            Key = key;
            Value = value;
            Comment = comment;
        }

        /// <summary>
        /// Returns a string that represents the current object information.
        /// </summary>
        public override string ToString()
        {
            return Key + "=" + Value + (string.IsNullOrEmpty(Comment) ? "" : " ;" + Comment);
        }
    }
}
