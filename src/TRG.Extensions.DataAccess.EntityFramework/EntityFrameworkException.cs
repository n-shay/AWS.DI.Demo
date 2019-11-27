namespace TRG.Extensions.DataAccess.EntityFramework
{
    using System;

    public class EntityFrameworkException : ApplicationException
    {
        public EntityFrameworkException(string message)
            : base(message)
        {
        }

        public EntityFrameworkException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}