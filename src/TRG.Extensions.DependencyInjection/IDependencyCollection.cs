namespace TRG.Extensions.DependencyInjection
{
    using System.Collections.Generic;

    public interface IDependencyCollection : IList<DependencyDescriptor>
    {
        void Add<T>() where T : DependencyDescriptor, new();
    }
}