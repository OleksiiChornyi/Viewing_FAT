namespace DiscUtils
{
    using System;
    using System.IO;

    internal sealed class DiscFileLocator : FileLocator
    {
        private DiscFileSystem _fileSystem;
        private string _basePath;

        public DiscFileLocator(DiscFileSystem fileSystem, string basePath)
        {
            _fileSystem = fileSystem;
            _basePath = basePath;
        }

        public override bool Exists(string fileName)
        {
            return _fileSystem.FileExists(Utilities.CombinePaths(_basePath, fileName));
        }

        public override Stream Open(string fileName, FileMode mode, FileAccess access, FileShare share)
        {
            return _fileSystem.OpenFile(Utilities.CombinePaths(_basePath, fileName), mode, access);
        }

        public override FileLocator GetRelativeLocator(string path)
        {
            return new DiscFileLocator(_fileSystem, Utilities.CombinePaths(_basePath, path));
        }

        public override string GetFullPath(string path)
        {
            return Utilities.CombinePaths(_basePath, path);
        }

        public override string GetDirectoryFromPath(string path)
        {
            return Utilities.GetDirectoryFromPath(path);
        }

        public override string GetFileFromPath(string path)
        {
            return Utilities.GetFileFromPath(path);
        }

        public override DateTime GetLastWriteTimeUtc(string path)
        {
            return _fileSystem.GetLastWriteTimeUtc(Utilities.CombinePaths(_basePath, path));
        }

        public override bool HasCommonRoot(FileLocator other)
        {
            DiscFileLocator otherDiscLocator = other as DiscFileLocator;

            if (otherDiscLocator == null)
            {
                return false;
            }

            // Common root if the same file system instance.
            return Object.ReferenceEquals(otherDiscLocator._fileSystem, _fileSystem);
        }

        public override string ResolveRelativePath(string path)
        {
            return Utilities.ResolveRelativePath(_basePath, path);
        }
    }
}
