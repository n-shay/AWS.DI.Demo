using System.Threading.Tasks;
using AWS.DI.Core.Concurrency;
using SimpleInjector;

namespace AWS.DI.DependencyResolution
{
    public abstract class ServiceProviderBase : IServiceProvider
    {
        private readonly ReaderWriterLock _readerWriterLock = new ReaderWriterLock();
        private readonly Container _container;
        
        protected ServiceProviderBase()
        {
            _container = new Container();

            // make sure execution is done in the background and holding the thread, allow the instantiation of the Lambda
            Task.Run(() =>
            {
                using (_readerWriterLock.WriterLock())
                {
                    LoadPackagesInternal(_container);

                    // TODO: don't verify in production (release)
                    _container.Verify();
                }
            });
        }

        protected abstract void LoadPackagesInternal(Container container);

        public T Resolve<T>()
            where T : class
        {
            using (_readerWriterLock.ReaderLock())
            {
                return _container.GetInstance<T>();   
            }
        }
    }
}