using SimpleInjector;

namespace AWS.DI.DependencyResolution
{
    public abstract class ServiceProviderBase : IServiceProvider
    {
        private readonly object _syncRoot = new object();
        private Container _container;
        
        protected abstract void RegisterInternal(Container container);

        public T Resolve<T>()
            where T : class
        {
            return GetOrCreateInternal().GetInstance<T>();
        }

        private Container GetOrCreateInternal()
        {
            if (_container == null)
            {
                lock (_syncRoot)
                {
                    if (_container == null)
                    {
                        _container = new Container();
                        
                        RegisterInternal(_container);

#if DEBUG
                        // don't verify in release
                        _container.Verify();
#endif
                    }
                }
            }

            return _container;
        }
    }
}