using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Disposable;

namespace sp_tools_tests.Disposable
{
    [TestClass]
    public class DisposableUnmanagedMemoryTests
    {
        [TestMethod]
        public void SuccessUsageOfComAllocator()
        {
            var x = new DisposableUnmanagedMemory(sizeof(int), true);
            Marshal.WriteInt32(x.Handler, 42);
            var result = Marshal.ReadInt32(x.Handler);
            x.Dispose();
            Assert.AreEqual(result, 42);            
        }

        [TestMethod]
        public void SuccessUsageOfRegularAllocator()
        {
            var x = new DisposableUnmanagedMemory(sizeof(int), false);
            Marshal.WriteInt32(x.Handler, 42);
            var result = Marshal.ReadInt32(x.Handler);
            x.Dispose();
            Assert.AreEqual(result, 42);
        }

        [TestMethod]
        public void EnsureMemoryCleaned()
        {
            var x = new DisposableUnmanagedMemory(sizeof(int), false);
            Marshal.WriteInt32(x.Handler, 42);
            x.Dispose();
            Assert.AreEqual(x.Handler, IntPtr.Zero);
        }
    }
}