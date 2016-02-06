using System;
using System.Threading;
using Common.Logging;
using SpTools.Validation;

namespace SpTools.Disposable
{
    /// <summary>
    /// Helper to handle lock. It tries to acquire lock in .ctor and release it in Dispose().
    /// </summary>
    public class DisposableReaderWriterLock : DisposableResource
    {
        private readonly ReaderWriterLock _lock;
        private readonly TimeSpan _timeout;
        private readonly LockMode _mode;
        private LockCookie _cookie;

        private static readonly TimeSpan DefaultTimeout = new TimeSpan(0, 1, 0);
	    private static readonly ILog Logger = LogManager.GetLogger<DisposableReaderWriterLock>();

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="alock">Lock to handle</param>
        /// <param name="timeout">timeout to acquire lock</param>
        /// <param name="mode">Lock mode</param>
        public DisposableReaderWriterLock(ReaderWriterLock alock, TimeSpan timeout, LockMode mode = LockMode.Read)
        {
            ParametersValidator.IsNotNull(alock, ()=>alock);
            _lock = alock;
            _timeout = timeout;
            _mode = mode;
            AcquireLock();
			Logger.TraceFormat("lock created timeout={0}, mode ={1}", timeout, mode);
        }

        /// <summary>
        /// .ctor with adefault timeout.
        /// </summary>
        /// <param name="alock">Lock to handle</param>
        /// <param name="mode">Lock mode</param>
        public DisposableReaderWriterLock(ReaderWriterLock alock, LockMode mode = LockMode.Read)
            :this(alock, DefaultTimeout, mode)
        {
            
        }

        protected override void DisposeResources(bool disposeManagedResources)
        {
            ReleaseLock();
        }

        private void AcquireLock()
        {
            switch (_mode)
            {
                case LockMode.Read:
                    _lock.AcquireReaderLock(_timeout);
                    break;
                case LockMode.UpgrateToWrite:
                    _cookie = _lock.UpgradeToWriterLock(_timeout);
                    break;
                case LockMode.Write:
                    _lock.AcquireWriterLock(_timeout);
                    break;
            }
			Logger.TraceFormat("lock {0} acured", _mode);
		}

		private void ReleaseLock()
        {
            switch (_mode)
            {
                case LockMode.Read:
                    _lock.ReleaseReaderLock();
                    break;
                case LockMode.UpgrateToWrite:
                    _lock.DowngradeFromWriterLock(ref _cookie);
                    break;
                case LockMode.Write:
                    _lock.ReleaseWriterLock();
                    break;
            }
			Logger.TraceFormat("lock {0} released", _mode);
		}
	}
}