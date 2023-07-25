namespace DiscUtils.Fat
{
    using System;
    using System.Text;

    /// <summary>
    /// FAT file system options.
    /// </summary>
    public sealed class FatFileSystemOptions : DiscFileSystemOptions
    {
        private Encoding _encoding;

        internal FatFileSystemOptions()
        {
            FileNameEncoding = Encoding.GetEncoding(437);
        }

        internal FatFileSystemOptions(FileSystemParameters parameters)
        {
            if (parameters != null && parameters.FileNameEncoding != null)
            {
                FileNameEncoding = parameters.FileNameEncoding;
            }
            else
            {
                FileNameEncoding = Encoding.GetEncoding(437);
            }
        }

        /// <summary>
        /// Gets or sets the character encoding used for file names.
        /// </summary>
        public Encoding FileNameEncoding
        {
            get
            {
                return _encoding;
            }

            set
            {
                if (!value.IsSingleByte)
                {
                    throw new ArgumentException(value.EncodingName + " is not a single byte encoding");
                }

                _encoding = value;
            }
        }
    }
}
