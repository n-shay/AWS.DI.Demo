namespace RV.Application.DataAccess
{
    public class ContextProvider<T> : IContextProvider<T> where T:IContext
    {
        private readonly IContextFactory<T> _factory;
        private readonly object _syncRoot = new object();
        private T _context;

        public ContextProvider(IContextFactory<T> factory)
        {
            _factory = factory;
        }

        public T Get()
        {
            if (_context == null)
            {
                lock (_syncRoot)
                {
                    if (_context == null)
                    {
                        _context = _factory.Create();
                    }
                }
            }

            return _context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}