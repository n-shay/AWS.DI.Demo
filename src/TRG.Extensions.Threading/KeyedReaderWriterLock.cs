namespace TRG.Extensions.Threading
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    public class KeyedReaderWriterLock<TKey>
    {
        private readonly ConcurrentDictionary<TKey, Lazy<AsyncReaderWriterLock>> innerDictionary =
            new ConcurrentDictionary<TKey, Lazy<AsyncReaderWriterLock>>();

        public async Task<IDisposable> ReaderLockAsync(TKey key)
        {
            var innerLock = this.GetOrCreate(key);

            return await innerLock.ReaderLockAsync();
        }

        public async Task<IDisposable> WriterLockAsync(TKey key)
        {
            var innerLock = this.GetOrCreate(key);

            return await innerLock.WriterLockAsync();
        }
        
        private AsyncReaderWriterLock GetOrCreate(TKey key)
        {
            var lazyLock = this.innerDictionary.GetOrAdd(key, k => new Lazy<AsyncReaderWriterLock>(() => new AsyncReaderWriterLock()));
            return lazyLock.Value;
        }
    }
}