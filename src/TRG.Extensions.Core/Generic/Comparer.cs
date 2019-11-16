namespace TRG.Extensions.Generic
{
    using System;
    using System.Collections.Generic;

    public class Comparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> equalityComparer;

        public Comparer(Func<T, T, bool> equalityComparer)
        {
            this.equalityComparer = equalityComparer;
        }

        public bool Equals(T x, T y)
        {
            return this.equalityComparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}