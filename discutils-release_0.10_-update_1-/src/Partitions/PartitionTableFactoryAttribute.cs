namespace DiscUtils.Partitions
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal sealed class PartitionTableFactoryAttribute : Attribute
    {
    }
}
