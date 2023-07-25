namespace DiscUtils.Fat
{
    /// <summary>
    /// Enumeration of known FAT types.
    /// </summary>
    public enum FatType
    {
        /// <summary>
        /// Represents no known FAT type.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents a 12-bit FAT.
        /// </summary>
        Fat12 = 12,

        /// <summary>
        /// Represents a 16-bit FAT.
        /// </summary>
        Fat16 = 16,

        /// <summary>
        /// Represents a 32-bit FAT.
        /// </summary>
        Fat32 = 32
    }
}
