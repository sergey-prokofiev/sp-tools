using System;
using System.Threading;
using SpTools.Validation;

namespace SpTools.Disposable
{
    /// <summary>
    /// Functional style extension for disposing objects
    /// </summary>
    public static class DisposableExtensions
    {
        /// <summary>
        /// Wraps any action into a using statement
        /// </summary>
        /// <param name="d">Disposable to handle</param>
        /// <param name="action">Action to execute</param>
        public static void RunSafe(this IDisposable d, Action action)
        {
            ParametersValidator.IsNotNull(d, ()=>d);
            ParametersValidator.IsNotNull(action, () => action);
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
            using (new DisposableReaderWriterLockSlim(alock, SlimLockMode.Write))
            {
                action();
            }
        }

    }
}