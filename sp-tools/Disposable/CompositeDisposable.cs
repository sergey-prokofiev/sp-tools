using System;
using System.Collections.Generic;
using SpTools.Validation;

namespace SpTools.Disposable
{
    /// <summary>
    /// Holds disposable objects and dispose all of them.
    /// </summary>
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _lst;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="capacity">Default capacity</param>
        public CompositeDisposable(int capacity = 0)
        {
            _lst = new List<IDisposable>(capacity);
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="items">Array of dispoables </param>
        public CompositeDisposable(IReadOnlyCollection<IDisposable> items)
        {
            ParametersValidator.IsNotNull(items, () => items);
            _lst = new List<IDisposable>(items);
        }
        /// <summary>
        /// Adds a disposable to the list of object to be disposed with this one.
        /// </summary>
        /// <param name="d"></param>
        public void Add(IDisposable d)
        {
            _lst.Add(d);
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
            if (ex.Count > 0)
            {
                throw new AggregateException("One of nested Disposables thown an exception", ex);
            }
        }
    }
}