namespace AWS.DI.Core.Concurrency
{
    using System;
    using System.Threading;

    public class ReaderWriterLock
    {
        private readonly ReaderWriterLockSlim _innerLock;

        public ReaderWriterLock()
        {
            _innerLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        public Releaser ReaderLock(bool isUpgradeable = false)
        {
            if (isUpgradeable)
                _innerLock.EnterUpgradeableReadLock();
            else
                _innerLock.EnterReadLock();

            return Releaser.CreateReadReleaser(this, isUpgradeable);
        }

        public Releaser WriterLock()
        {
            _innerLock.EnterWriteLock();

            return Releaser.CreateWriteReleaser(this);
        }

        private void ReaderRelease(bool isUpgradeable)
        {
            if (isUpgradeable)
                _innerLock.ExitUpgradeableReadLock();
            else
                _innerLock.ExitReadLock();
        }

        private void WriterRelease()
        {
            _innerLock.ExitWriteLock();
        }

        public struct Releaser : IDisposable
        {
            private readonly ReaderWriterLock _toRelease;
            private readonly bool _writer;
            private readonly bool _isUpgradeable;

            internal static Releaser CreateReadReleaser(ReaderWriterLock toRelease, bool isUpgradeable)
            {
                return new Releaser(toRelease, false, isUpgradeable);
            }

            internal static Releaser CreateWriteReleaser(ReaderWriterLock toRelease)
            {
                return new Releaser(toRelease, true);
            }

            private Releaser(ReaderWriterLock toRelease, bool writer, bool isUpgradeable = false)
            {
                _toRelease = toRelease;
                _writer = writer;
                _isUpgradeable = isUpgradeable;
            }

            public void Dispose()
            {
                if (_toRelease == null) return;

                if (_writer) _toRelease.WriterRelease();
                else _toRelease.ReaderRelease(_isUpgradeable);
            }
        }
    }
}