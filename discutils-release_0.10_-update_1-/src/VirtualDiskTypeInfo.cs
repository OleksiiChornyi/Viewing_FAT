namespace DiscUtils
{
    /// <summary>
    /// Delegate for calculating a disk geometry from a capacity.
    /// </summary>
    /// <param name="capacity">The disk capacity to convert</param>
    /// <returns>The appropriate geometry for the disk</returns>
    public delegate Geometry GeometryCalculation(long capacity);

    /// <summary>
    /// Information about a type of virtual disk.
    /// </summary>
    public sealed class VirtualDiskTypeInfo
    {
        /// <summary>
        /// Gets or sets the name of the virtual disk type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the variant of the virtual disk type.
        /// </summary>
        public string Variant { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this disk type can represent hard disks.
        /// </summary>
        public bool CanBeHardDisk { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this disk type requires a specific geometry for any given disk capacity.
        /// </summary>
        public bool DeterministicGeometry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this disk type persists the BIOS geometry.
        /// </summary>
        public bool PreservesBiosGeometry { get; set; }

        /// <summary>
        /// Gets or sets the algorithm for determining the geometry for a given disk capacity.
        /// </summary>
        public GeometryCalculation CalcGeometry { get; set; }
    }
}
