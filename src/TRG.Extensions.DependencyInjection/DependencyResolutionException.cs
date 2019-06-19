using System;

namespace TRG.Extensions.DependencyInjection
{
    public class DependencyResolutionException : Exception
    {
        public DependencyResolutionException(Type type)
            : base($"The type {type.Name} could not be resolved.")
        {
        }
    }
}