namespace DiscUtils
{
    using System;
    using System.IO;

    internal abstract class FileLocator
    {
        public abstract bool Exists(string fileName);

        public abstract Stream Open(string fileName, FileMode mode, FileAccess access, FileShare share);

        public abstract FileLocator GetRelativeLocator(string path);

        public abstract string GetFullPath(string path);

        public abstract string GetDirectoryFromPath(string path);

        public abstract string GetFileFromPath(string path);

        public abstract DateTime GetLastWriteTimeUtc(string path);

        public abstract bool HasCommonRoot(FileLocator other);

        public abstract string ResolveRelativePath(string path);

        internal string MakeRelativePath(FileLocator fileLocator, string path)
        {
            if (!HasCommonRoot(fileLocator))
            {
                return null;
            }

            string ourFullPath = GetFullPath(string.Empty) + @"\";
            string otherFullPath = fileLocator.GetFullPath(path);

            return Utilities.MakeRelativePath(otherFullPath, ourFullPath);
        }
    }
}
