using System;
using System.Threading;
using Common.Logging;
using SpTools.Validation;

namespace SpTools.Disposable
{
    /// <summary>
    /// Functional style extension for disposing objects
    /// </summary>
    public static class DisposableExtensions
    {
	    private static readonly ILog Logger = LogManager.GetLogger(typeof(DisposableExtensions));
		/// <summary>
        /// Wraps any action into a using statement
        /// </summary>
        /// <param name="d">Disposable to handle</param>
        /// <param name="action">Action to execute</param>
        public static void RunSafe(this IDisposable d, Action action)
        {
            ParametersValidator.IsNotNull(d, ()=>d);
            ParametersValidator.IsNotNull(action, () => action);
			Logger.TraceFormat("Runnig action {0} with disposable {1}", action, d);
            using (d)
            {
                action();
            }
        }

        /// <summary>
        /// Wraps action into try/finally statement with getting a read lock in try and releasing in finally.
        /// </summary>
        /// <param name="alock">Lock to handle</param>
        /// <param name="action">Action to execute</param>
        /// <param name="timeout">Timeout to get lock</param>
        public static void WithReadLock(this ReaderWriterLockSlim alock, Action action, TimeSpan timeout)
        {
            ParametersValidator.IsNotNull(alock, () => alock);
            ParametersValidator.IsNotNull(action, () => action);
			Logger.TraceFormat("Runnig action {0} with slim lock {1} and timeout {2} with read lock", action, alock, timeout);
			using (new DisposableReaderWriterLockSlim(alock, timeout))
            {
                action();
            }
        }

        /// <summary>
        /// Wraps action into try/finally statement with getting a read lock in try and releasing in finally. 
        /// Default timeout of <see cref="DisposableReaderWriterLockSlim"/> is used.
        /// </summary>
        /// <param name="alock">Lock to handle</param>
        /// <param name="action">Action to execute</param>
        public static void WithReadLock(this ReaderWriterLockSlim alock, Action action)
        {
            ParametersValidator.IsNotNull(alock, () => alock);
            ParametersValidator.IsNotNull(action, () => action);
			Logger.TraceFormat("Runnig action {0} with slim lock {1} with read lock", action, alock);
			using (new DisposableReaderWriterLockSlim(alock))
            {
                action();
            }
        }

        /// <summary>
        /// Wraps action into try/finally statement with getting a write lock in try and releasing in finally. 
        /// Default timeout of <see cref="DisposableReaderWriterLockSlim"/> is used.
        /// </summary>
        /// <param name="alock">Lock to handle</param>
        /// <param name="action">Action to execute</param>
        /// <param name="timeout">TImeout to get write lock</param>
        public static void WithWriteLock(this ReaderWriterLockSlim alock, Action action, TimeSpan timeout)
        {
            ParametersValidator.IsNotNull(alock, () => alock);
            ParametersValidator.IsNotNull(action, () => action);
			Logger.TraceFormat("Runnig action {0} with slim lock {1} and timeout {2} with write lock", action, alock, timeout);
			using (new DisposableReaderWriterLockSlim(alock, timeout, SlimLockMode.Write))
            {
                action();
            }
        }

        /// <summary>
        /// Wraps action into try/finally statement with getting a write lock in try and releasing in finally. 
        /// Default timeout of <see cref="DisposableReaderWriterLockSlim"/> is used.
        /// </summary>
        /// <param name="alock">Lock to handle</param>
        /// <param name="action">Action to execute</param>
        public static void WithWriteLock(this ReaderWriterLockSlim alock, Action action)
        {
            ParametersValidator.IsNotNull(alock, () => alock);
            ParametersValidator.IsNotNull(action, () => action);
			Logger.TraceFormat("Runnig action {0} with slim lock {1} with write lock", action, alock);
			using (new DisposableReaderWriterLockSlim(alock, SlimLockMode.Write))
            {
                action();
            }
        }


        /// <summary>
        /// Wraps action into try/finally statement with getting a read lock in try and releasing in finally.
        /// </summary>
        /// <param name="alock">Lock to handle</param>
        /// <param name="action">Action to execute</param>
        /// <param name="timeout">Timeout to get lock</param>
        public static void WithReadLock(this ReaderWriterLock alock, Action action, TimeSpan timeout)
        {
            ParametersValidator.IsNotNull(alock, () => alock);
            ParametersValidator.IsNotNull(action, () => action);
			Logger.TraceFormat("Runnig action {0} with lock {1} with read lock and timeout {2}", action, alock, timeout);
			using (new DisposableReaderWriterLock(alock, timeout))
            {
                action();
            }
        }

        /// <summary>
        /// Wraps action into try/finally statement with getting a read lock in try and releasing in finally. 
        /// Default timeout of <see cref="DisposableReaderWriterLockSlim"/> is used.
        /// </summary>
        /// <param name="alock">Lock to handle</param>
        /// <param name="action">Action to execute</param>
        public static void WithReadLock(this ReaderWriterLock alock, Action action)
        {
            ParametersValidator.IsNotNull(alock, () => alock);
            ParametersValidator.IsNotNull(action, () => action);
			Logger.TraceFormat("Runnig action {0} with lock {1} with read lock", action, alock);
			using (new DisposableReaderWriterLock(alock))
            {
                action();
            }
        }

        /// <summary>
        /// Wraps action into try/finally statement with getting a write lock in try and releasing in finally. 
        /// Default timeout of <see cref="DisposableReaderWriterLockSlim"/> is used.
        /// </summary>
        /// <param name="alock">Lock to handle</param>
        /// <param name="action">Action to execute</param>
        /// <param name="timeout">TImeout to get write lock</param>
        public static void WithWriteLock(this ReaderWriterLock alock, Action action, TimeSpan timeout)
        {
            ParametersValidator.IsNotNull(alock, () => alock);
            ParametersValidator.IsNotNull(action, () => action);
			Logger.TraceFormat("Runnig action {0} with lock {1} with write lock and timeout {2}", action, alock, timeout);
			using (new DisposableReaderWriterLock(alock, timeout, LockMode.Write))
            {
                action();
            }
        }

        /// <summary>
        /// Wraps action into try/finally statement with getting a write lock in try and releasing in finally. 
        /// Default timeout of <see cref="DisposableReaderWriterLockSlim"/> is used.
        /// </summary>
        /// <param name="alock">Lock to handle</param>
        /// <param name="action">Action to execute</param>
        public static void WithWriteLock(this ReaderWriterLock alock, Action action)
        {
            ParametersValidator.IsNotNull(alock, () => alock);
            ParametersValidator.IsNotNull(action, () => action);
			Logger.TraceFormat("Runnig action {0} with lock {1} with read lock", action, alock);
			using (new DisposableReaderWriterLock(alock, LockMode.Write))
            {
                action();
            }
        }
    }
}