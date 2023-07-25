namespace DiscUtils
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal sealed class VirtualDiskFactoryAttribute : Attribute
    {
        private string _type;
        private string[] _fileExtensions;

        public VirtualDiskFactoryAttribute(string type, string fileExtensions)
        {
            _type = type;
            _fileExtensions = fileExtensions.Replace(".", string.Empty).Split(',');
        }

        public string Type
        {
            get { return _type; }
        }

        public string[] FileExtensions
        {
            get { return _fileExtensions; }
        }
    }
}
