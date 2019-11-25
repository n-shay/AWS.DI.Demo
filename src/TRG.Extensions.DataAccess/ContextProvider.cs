namespace TRG.Extensions.DataAccess
{
    public class ContextProvider<T> : IContextProvider<T> where T:IContext
    {
        private readonly IContextFactory<T> factory;
        private readonly object syncRoot = new object();
        private T context;

        public ContextProvider(IContextFactory<T> factory)
        {
            this.factory = factory;
        }

        public T Get()
        {
            if (this.context == null)
            {
                lock (this.syncRoot)
                {
                    if (this.context == null)
                    {
                        this.context = this.factory.Create();
                    }
                }
            }

            return this.context;
        }

        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}