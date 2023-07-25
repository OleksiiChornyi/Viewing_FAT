namespace DiscUtils
{
    using System;

    internal abstract class Tuple
    {
        public abstract object this[int i]
        {
            get;
        }

        protected static bool Equals<V>(V a, V b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            else if (a == null)
            {
                return false;
            }
            else
            {
                return a.Equals(b);
            }
        }
    }
}
