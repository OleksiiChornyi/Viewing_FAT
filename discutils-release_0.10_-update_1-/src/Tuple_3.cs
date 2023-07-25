namespace DiscUtils
{
    using System;

    internal class Tuple<A, B, C> : Tuple
    {
        private A _a;
        private B _b;
        private C _c;

        public Tuple(A a, B b, C c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public A First
        {
            get { return _a; }
        }

        public B Second
        {
            get { return _b; }
        }

        public C Third
        {
            get { return _c; }
        }

        public override object this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return _a;
                    case 1: return _b;
                    case 2: return _c;
                    default: throw new ArgumentOutOfRangeException("i", i, "Invalid index");
                }
            }
        }

        public override bool Equals(object obj)
        {
            Tuple<A, B, C> asType = obj as Tuple<A, B, C>;
            if (asType == null)
            {
                return false;
            }

            return Equals(_a, asType._a) && Equals(_b, asType._b) && Equals(_c, asType._c);
        }

        public override int GetHashCode()
        {
            return ((_a == null) ? 0x14AB32BC : _a.GetHashCode())
                ^ ((_b == null) ? 0x65BC32DE : _b.GetHashCode())
                ^ ((_c == null) ? 0x2D4C25CF : _b.GetHashCode());
        }
    }
}
