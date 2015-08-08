using System;

namespace SpTools.Disposable
{
	/// <summary>
	/// Base class for disposable resources wrappers which make really 'feng shui' disposing.
	/// </summary>
	public abstract class DisposableResource : IDisposable
	{
		/// <summary>
		/// Determine if object alreadfy disposed. Check this property in descendants to determine if resource was already disposed
		/// </summary>
		protected bool Disposed { get; private set; }

		/// <summary>
		/// Dispose member-resources. Generally should not be overriden.
		/// </summary>
		public virtual void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Do not dispose member-resources, it already disposed by GC.
		/// </summary>
		~DisposableResource()
		{
			Dispose(false);
		}

		/// <summary>
		/// Override it to imlement real disposing
		/// </summary>
		/// <param name="disposeManagedResources">false if it was called from finalizer and managed resources 
		/// already disposed, false otherwise</param>
		protected abstract void DisposeResources(bool disposeManagedResources);

		private void Dispose(bool disposing)
		{
		    if (Disposed)
		    {
		        return;
		    }
		    DisposeResources(disposing);
			Disposed = true;
		}
	}
}
