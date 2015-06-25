using System;
using System.Runtime.InteropServices;

namespace SpTools.Disposable
{
	/// <summary>
	/// Helper which allocate/dispose unmanaged memory.
	/// </summary>
	public class DisposableUnmanagedMemory : DisposableResource
	{
		private readonly bool _useAllocCoTaskMem;

		/// <summary>
		/// Pointer to allocated memory
		/// </summary>
		public IntPtr Handler { get; private set; }

		/// <summary>
		/// .ctor.Allocates nessesary memory.
		/// </summary>
		/// <param name="size">Size of memory to allocated</param>
		/// <param name="useCOMMemoryAllocator">If true, Marshal.AllocCoTaskMem used, else Marshal.AllocHGlobal</param>
		public DisposableUnmanagedMemory(int size, bool useCOMMemoryAllocator)
			: this(useCOMMemoryAllocator ? Marshal.AllocCoTaskMem(size) : Marshal.AllocHGlobal(size), useCOMMemoryAllocator)
		{
		}

		/// <summary>
		/// .ctor. Wraps already allocated memory to clean it in dispose
		/// </summary>
		/// <param name="allocatedPointer">Already allocated poiner</param>
		/// <param name="useCOMMemoryCleaner">if true, in dispose called FreeCoTaskMem, else - FreeHGlobal</param>
		public DisposableUnmanagedMemory(IntPtr allocatedPointer, bool useCOMMemoryCleaner)
		{
			_useAllocCoTaskMem = useCOMMemoryCleaner;
			Handler = allocatedPointer;
		}

		protected override void DisposeResources(bool disposeManagedResources)
		{
			if (_useAllocCoTaskMem)
			{
				Marshal.FreeCoTaskMem(Handler);
			}
			else
			{
				Marshal.FreeHGlobal(Handler);
			}
		}
	}
}
