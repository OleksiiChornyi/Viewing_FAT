namespace DiscUtils
{
    using System;

    internal class Tuple<A, B> : Tuple
    {
        public A _a;
        public B _b;

        public Tuple(A a, B b)
        {
            _a = a;
            _b = b;
        }

        public A First
        {
            get { return _a; }
        }

        public B Second
        {
            get { return _b; }
        }

        public override object this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return _a;
                    case 1: return _b;
                    default: throw new ArgumentOutOfRangeException("i", i, "Invalid index");
                }
            }
        }

        public override bool Equals(object obj)
        {
            Tuple<A, B> asType = obj as Tuple<A, B>;
            if (asType == null)
            {
                return false;
            }

            return Equals(_a, asType._a) && Equals(_b, asType._b);
        }

        public override int GetHashCode()
        {
            return ((_a == null) ? 0x14AB32BC : _a.GetHashCode()) ^ ((_b == null) ? 0x65BC32DE : _b.GetHashCode());
        }
    }
}
