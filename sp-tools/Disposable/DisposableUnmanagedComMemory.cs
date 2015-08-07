using System;
using System.Runtime.InteropServices;

namespace SpTools.Disposable
{
    /// <summary>
    /// Helper which allocate/dispose unmanaged memory using COM alloctor.
    /// </summary>
    public class DisposableUnmanagedComMemory : DisposableResource
    {
        /// <summary>
        /// Pointer to allocated memory
        /// </summary>
        public IntPtr Handler { get; private set; }

        /// <summary>
        /// .ctor. Allocates nessesary memory.
        /// </summary>
        /// <param name="size">Size of memory to allocated</param>
        public DisposableUnmanagedComMemory(int size)
            : this(Marshal.AllocCoTaskMem(size))
        {
        }

        /// <summary>
        /// .ctor. Wraps already allocated memory to clean it in dispose
        /// </summary>
        /// <param name="allocatedPointer">Already allocated poiner</param>
        public DisposableUnmanagedComMemory(IntPtr allocatedPointer)
        {
            Handler = allocatedPointer;
        }

        protected override void DisposeResources(bool disposeManagedResources)
        {
            Marshal.FreeCoTaskMem(Handler);
            Handler = IntPtr.Zero;
        }
    }
}