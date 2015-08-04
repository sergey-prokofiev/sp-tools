using System;
using System.Threading;
using SpTools.Validation;

namespace SpTools.Disposable
{
	/// <summary>
	/// Helper to handle lock. It tries to acquire lock in .ctor and release it in Dispose().
	/// </summary>
	public sealed class DisposableReaderWriterLockSlim : DisposableResource
	{
		private readonly SlimLockMode _mode;
		private static readonly TimeSpan DefaultTimeout = new TimeSpan(0, 1, 0);
		private readonly TimeSpan _timeout;
        private ReaderWriterLockSlim Lock { get; set; }

		/// <summary>
		/// .ctor. Try to acquire lock with default timeout (1h).
		/// <exception cref="InvalidOperationException"> If wrong SlimLockMode specified</exception>
		/// <exception cref="ApplicationException"> If lock cannot be acquired during timeout</exception>
		/// <exception cref="ArgumentNullException"> alock is null</exception>
		/// <exception cref="LockRecursionException"> 
		/// <see cref="ReaderWriterLockSlim.TryEnterReadLock(System.TimeSpan)"/>
		/// <see cref="ReaderWriterLockSlim.TryEnterUpgradeableReadLock(System.TimeSpan)"/>
		/// <see cref="ReaderWriterLockSlim.TryEnterWriteLock(System.TimeSpan)"/>
		/// </exception>
		/// </summary>
		/// <param name="alock">Lock object</param>
		/// <param name="mode">Mode to acquire lock</param>
		public DisposableReaderWriterLockSlim(ReaderWriterLockSlim alock, SlimLockMode mode = SlimLockMode.Read)
			:this(alock, DefaultTimeout, mode)
		{
			
		}

		/// <summary>
		/// .ctor. Try to acquire lock with given timeout.
		/// <exception cref="InvalidOperationException"> If wrong SlimLockMode specified</exception>
		/// <exception cref="ApplicationException"> If lock cannot be acquired during timeout</exception>
		/// <exception cref="ArgumentNullException"> alock is null</exception>
		/// <exception cref="LockRecursionException"> 
		/// <see cref="ReaderWriterLockSlim.TryEnterReadLock(System.TimeSpan)"/>
		/// <see cref="ReaderWriterLockSlim.TryEnterUpgradeableReadLock(System.TimeSpan)"/>
		/// <see cref="ReaderWriterLockSlim.TryEnterWriteLock(System.TimeSpan)"/>
		/// </exception>
		/// </summary>
		/// <param name="alock">Lock object</param>
		/// <param name="mode">Mode to acquire lock</param>
		/// <param name="timeout">Timeout to acquire lock</param>
		public DisposableReaderWriterLockSlim(ReaderWriterLockSlim alock, TimeSpan timeout, SlimLockMode mode = SlimLockMode.Read)
		{
		    ParametersValidator.IsNotNull(alock, () => alock);
            Lock = alock;
			_mode = mode;
			_timeout = timeout;
			AcquireLock();
		}

		/// <summary>
		/// Release lock, accured in .ctor
		/// <exception cref="SynchronizationLockException">
		/// <see cref="ReaderWriterLockSlim.ExitReadLock"/>
		/// <see cref="ReaderWriterLockSlim.ExitUpgradeableReadLock"/>
		/// <see cref="ReaderWriterLockSlim.ExitWriteLock"/>
		/// </exception>
		/// </summary>
		protected override void DisposeResources(bool disposeManagedResources)
		{
			ReleaseLock();
		}

		private void ReleaseLock()
		{
			switch (_mode)
			{
				case SlimLockMode.Read:
					Lock.ExitReadLock();
					break;
				case SlimLockMode.UpgradeableRead:
					Lock.ExitUpgradeableReadLock();
					break;
				case SlimLockMode.Write:
					Lock.ExitWriteLock();
					break;
			}
		}

		private void AcquireLock()
		{
		    var result = false;
			switch (_mode)
			{
				case SlimLockMode.Read:
					result = Lock.TryEnterReadLock(_timeout);
					break;
				case SlimLockMode.UpgradeableRead:
					result = Lock.TryEnterUpgradeableReadLock(_timeout);
					break;
				case SlimLockMode.Write:
					result = Lock.TryEnterWriteLock(_timeout);
					break;
			}
		    if (!result)
		    {
		        throw new InvalidOperationException("Cannot acquire lock");
		    }
		}
	}
}
