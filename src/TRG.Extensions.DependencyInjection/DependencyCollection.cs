namespace TRG.Extensions.DependencyInjection
{
    using System.Collections;
    using System.Collections.Generic;

    public class DependencyCollection : IDependencyCollection
    {
        private readonly IList<DependencyDescriptor> descriptors = new List<DependencyDescriptor>();

        public IEnumerator<DependencyDescriptor> GetEnumerator()
        {
            return this.descriptors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(DependencyDescriptor item)
        {
            this.descriptors.Add(item);
        }

        public void Clear()
        {
            this.descriptors.Clear();
        }

        public bool Contains(DependencyDescriptor item)
        {
            return this.descriptors.Contains(item);
        }

        public void CopyTo(DependencyDescriptor[] array, int arrayIndex)
        {
            this.descriptors.CopyTo(array, arrayIndex);
        }

        public bool Remove(DependencyDescriptor item)
        {
            return this.descriptors.Remove(item);
        }

        public int Count => this.descriptors.Count;

        public bool IsReadOnly => false;

        public int IndexOf(DependencyDescriptor item)
        {
            return this.descriptors.IndexOf(item);
        }

        public void Insert(int index, DependencyDescriptor item)
        {
            this.descriptors.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.descriptors.RemoveAt(index);
        }

        public DependencyDescriptor this[int index]
        {
            get => this.descriptors[index];
            set => this.descriptors[index] = value;
        }

        public void Add<T>() where T : DependencyDescriptor, new()
        {
            this.Add(new T());
        }
    }
}