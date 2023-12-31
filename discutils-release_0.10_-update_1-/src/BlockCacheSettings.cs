namespace DiscUtils
{
    /// <summary>
    /// Settings controlling BlockCache instances.
    /// </summary>
    public sealed class BlockCacheSettings
    {
        /// <summary>
        /// Initializes a new instance of the BlockCacheSettings class.
        /// </summary>
        public BlockCacheSettings()
        {
            this.BlockSize = (int)(4 * Sizes.OneKiB);
            this.ReadCacheSize = 4 * Sizes.OneMiB;
            this.LargeReadSize = 64 * Sizes.OneKiB;
            this.OptimumReadSize = (int)(64 * Sizes.OneKiB);
        }

        /// <summary>
        /// Initializes a new instance of the BlockCacheSettings class.
        /// </summary>
        /// <param name="settings">The cache settings</param>
        internal BlockCacheSettings(BlockCacheSettings settings)
        {
            this.BlockSize = settings.BlockSize;
            this.ReadCacheSize = settings.ReadCacheSize;
            this.LargeReadSize = settings.LargeReadSize;
            this.OptimumReadSize = settings.OptimumReadSize;
        }

        /// <summary>
        /// Gets or sets the size (in bytes) of each cached block.
        /// </summary>
        public int BlockSize { get; set; }

        /// <summary>
        /// Gets or sets the size (in bytes) of the read cache.
        /// </summary>
        public long ReadCacheSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum read size that will be cached.
        /// </summary>
        /// <remarks>Large reads are not cached, on the assumption they will not
        /// be repeated.  This setting controls what is considered 'large'.
        /// Any read that is more than this many bytes will not be cached.</remarks>
        public long LargeReadSize { get; set; }

        /// <summary>
        /// Gets or sets the optimum size of a read to the wrapped stream.
        /// </summary>
        /// <remarks>This value must be a multiple of BlockSize</remarks>
        public int OptimumReadSize { get; set; }
    }
}
