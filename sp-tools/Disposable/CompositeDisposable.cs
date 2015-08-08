using System;
using System.Collections.Generic;

namespace SpTools.Disposable
{
    /// <summary>
    /// Holds some disposable memebrs and dispose it.
    /// Handle only direct call of di
    /// </summary>
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _lst = new List<IDisposable>();

        /// <summary>
        /// Adds a disposable to the list of object to be disposed with this one.
        /// </summary>
        /// <param name="d"></param>
        public void Add(IDisposable d)
        {
            _lst.Add(d);
        }
        /*
        protected override void DisposeResources(bool disposeManagedResources)
        {
            //do nothing: Dispose is overriden and call from finzlizer should not be handled  - all list members finalizer should be called automatically too.
        }
        */
        /// <summary>
        /// Dispose all members
        /// </summary>
        public void Dispose()
        {
            foreach (var d in _lst)
            {
                d?.Dispose();
            }
        }
    }
}