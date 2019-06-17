using System.Collections;
using System.Collections.Generic;

namespace TRG.Extensions.DependencyInjection
{
    public class DependencyCollection : IDependencyCollection
    {
        private readonly List<DependencyDescriptor> _descriptors = new List<DependencyDescriptor>();

        public IEnumerator<DependencyDescriptor> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(DependencyDescriptor item)
        {
            _descriptors.Add(item);
        }

        public void Clear()
        {
            _descriptors.Clear();
        }

        public bool Contains(DependencyDescriptor item)
        {
            return _descriptors.Contains(item);
        }

        public void CopyTo(DependencyDescriptor[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array, arrayIndex);
        }

        public bool Remove(DependencyDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        public int Count => _descriptors.Count;

        public bool IsReadOnly => false;

        public int IndexOf(DependencyDescriptor item)
        {
            return _descriptors.IndexOf(item);
        }

        public void Insert(int index, DependencyDescriptor item)
        {
            _descriptors.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _descriptors.RemoveAt(index);
        }

        public DependencyDescriptor this[int index]
        {
            get => _descriptors[index];
            set => _descriptors[index] = value;
        }
    }
}