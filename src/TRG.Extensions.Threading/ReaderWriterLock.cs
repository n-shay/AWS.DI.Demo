namespace TRG.Extensions.Threading
{
    using System;
    using System.Threading;

    public class ReaderWriterLock
    {
        private readonly ReaderWriterLockSlim innerLock;

        public ReaderWriterLock()
        {
            this.innerLock = new ReaderWriterLockSlim();
        }

        public Releaser ReaderLock(bool isUpgradeable = false)
        {
            if (isUpgradeable)
                this.innerLock.EnterUpgradeableReadLock();
            else
                this.innerLock.EnterReadLock();

            return Releaser.CreateReadReleaser(this, isUpgradeable);
        }

        public Releaser WriterLock()
        {
            this.innerLock.EnterWriteLock();

            return Releaser.CreateWriteReleaser(this);
        }

        private void ReaderRelease(bool isUpgradeable)
        {
            if (isUpgradeable)
                this.innerLock.ExitUpgradeableReadLock();
            else
                this.innerLock.ExitReadLock();
        }

        private void WriterRelease()
        {
            this.innerLock.ExitWriteLock();
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
                if (this.toRelease == null) return;

                if (this.writer) this.toRelease.WriterRelease();
                else this.toRelease.ReaderRelease(this.isUpgradeable);
            }
        }
    }
}