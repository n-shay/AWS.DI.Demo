namespace TRG.Extensions.DependencyInjection
{
    using System;

    public class DependencyResolutionException : ApplicationException
    {
        public DependencyResolutionException(Type type)
            : this(type, null)
        {
        }

        public DependencyResolutionException(Type type, Exception innerException)
            : this($"The type {type.Name} could not be resolved.", innerException)
        {
        }

        public DependencyResolutionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}