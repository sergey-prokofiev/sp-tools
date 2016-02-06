using System;
using System.Collections.Generic;
using Common.Logging;
using SpTools.Validation;

namespace SpTools.Disposable
{
    /// <summary>
    /// Holds disposable objects and dispose all of them.
    /// </summary>
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _lst;
        private static readonly ILog Logger = LogManager.GetLogger<CompositeDisposable>();

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="capacity">Default capacity</param>
        public CompositeDisposable(int capacity = 0)
        {
            _lst = new List<IDisposable>(capacity);
            Logger.TraceFormat("Composite disposable created with {0} capacity and without childs", capacity);
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="items">Array of dispoables </param>
        public CompositeDisposable(IReadOnlyCollection<IDisposable> items)
        {
            ParametersValidator.IsNotNull(items, () => items);
            _lst = new List<IDisposable>(items);
            Logger.TraceFormat("Composite disposable created with {0} childs", _lst.Count);
        }

		/// <summary>
		/// Adds a disposable to the list of object to be disposed with this one.
		/// </summary>
		/// <param name="d"></param>
        public void Add(IDisposable d)
        {
            _lst.Add(d);
			Logger.TraceFormat("Disposable {0} was added to composite disposable", d);
		}

		/// <summary>
		/// Dispose all members. All thrown exceptions are aggregated and AggregateException is thrown if at least one exception occured.
		/// </summary>
		public void Dispose()
        {
            var ex = new List<Exception>();
            foreach (var d in _lst)
            {
                try
                {
                    d?.Dispose();
                }
                catch (Exception e)
                {
                    ex.Add(e);
                }
            }
            GC.SuppressFinalize(this);
			Logger.TraceFormat("Composite disposable was disposed, {0} errors", ex.Count);
			if (ex.Count > 0)
            {
                throw new AggregateException("One of nested Disposables thown an exception", ex);
            }
        }
    }
}