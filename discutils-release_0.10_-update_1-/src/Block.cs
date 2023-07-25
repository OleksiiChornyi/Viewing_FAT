namespace DiscUtils
{
    internal class Block
    {
        public Block()
        {
        }
        
        public long Position { get; set; }

        public byte[] Data { get; set; }

        public int Available { get; set; }

        public bool Equals(Block other)
        {
            return Position == other.Position;
        }
    }
}
