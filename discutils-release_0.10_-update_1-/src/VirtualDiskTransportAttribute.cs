namespace DiscUtils
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal sealed class VirtualDiskTransportAttribute : Attribute
    {
        private string _scheme;

        public VirtualDiskTransportAttribute(string scheme)
        {
            _scheme = scheme;
        }

        public string Scheme
        {
            get { return _scheme; }
        }
    }
}
