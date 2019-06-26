using System;
using SimpleInjector;
using TRG.Extensions.Threading;

namespace TRG.Extensions.Lambda
{
    public static class LambdaLifestyle
    {
        private static Guid _lambdaScopeId;
        private static Lifestyle _active;
        private static readonly object SyncRoot = new object();
        private static readonly ReaderWriterLock ScopeIdMutex = new ReaderWriterLock();

        public static Lifestyle Active
        {
            get
            {
                if (_active == null)
                {
                    lock (SyncRoot)
                    {
                        if (_active == null)
                        {
                            _active = Lifestyle.CreateCustom("Lambda", LifestyleApplierFactory);
                        }
                    }
                }

                return _active;
            }
        }

        internal static void Invoked()
        {
            using (ScopeIdMutex.WriterLock())
            {
                _lambdaScopeId = Guid.NewGuid();
            }
        }

        internal static void Initialized()
        {
            using (ScopeIdMutex.WriterLock())
            {
                _lambdaScopeId = Guid.NewGuid();
            }
        }

        private static Func<object> LifestyleApplierFactory(Func<object> instanceCreator)
        {
            var current = Guid.Empty;
            var syncRoot = new object();
            object instance = null;

            return () =>
            {
                lock (syncRoot)
                {
                    using (ScopeIdMutex.ReaderLock())
                    {
                        if (current != _lambdaScopeId)
                        {
                            instance = instanceCreator.Invoke();
                            current = _lambdaScopeId;
                        }
                    }
                    return instance;
                }
            };
        }
    }
}