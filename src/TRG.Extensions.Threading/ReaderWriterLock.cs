using System;
using System.Threading;

namespace TRG.Extensions.Threading
{
    public class ReaderWriterLock
    {
        private readonly ReaderWriterLockSlim _innerLock;

        public ReaderWriterLock()
        {
            _innerLock = new ReaderWriterLockSlim();
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
            private readonly ReaderWriterLock toRelease;
            private readonly bool writer;
            private readonly bool isUpgradeable;

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
                this.toRelease = toRelease;
                this.writer = writer;
                this.isUpgradeable = isUpgradeable;
            }

            public void Dispose()
            {
                if (toRelease == null) return;

                if (writer) toRelease.WriterRelease();
                else toRelease.ReaderRelease(isUpgradeable);
            }
        }
    }
}