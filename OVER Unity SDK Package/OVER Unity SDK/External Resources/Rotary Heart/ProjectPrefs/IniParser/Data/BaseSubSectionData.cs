namespace RotaryHeart.Lib.IniParser.Data
{
    /// <summary>
    /// Base class that holds the sub section data
    /// </summary>
    public abstract class BaseSubSectionData
    {
        /// <summary>
        /// Sub section name that holds the data
        /// </summary>
        public string SubSection { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object information.
        /// </summary>
        public abstract override string ToString();
    }
}
