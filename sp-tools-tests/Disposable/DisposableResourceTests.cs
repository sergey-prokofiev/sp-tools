using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Disposable;

namespace sp_tools_tests.Disposable
{
    public class TestDisposable : DisposableResource
    {
        protected override void DisposeResources(bool disposeManagedResources)
        {
            DisposeCalled = true;
            ParamValue = disposeManagedResources;
        }

        public static bool DisposeCalled;
        public static bool? ParamValue;
    }
    [TestClass]
    public class DisposableResourceTests
    {
        [TestMethod]
        public void TestDisposeCalled()
        {
            TestDisposable.ParamValue = null;
            TestDisposable.DisposeCalled = false;
            using (var c = new TestDisposable())
            {
                
            }
            Assert.IsTrue(TestDisposable.DisposeCalled);
            Assert.IsNotNull(TestDisposable.ParamValue);
            Assert.IsTrue(TestDisposable.ParamValue.Value);
        }

        [TestMethod]
        public void TestFinalizerCalled()
        {
            TestDisposable.ParamValue = null;
            TestDisposable.DisposeCalled = false;
            var c = new TestDisposable();
            c = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Assert.IsTrue(TestDisposable.DisposeCalled);
            Assert.IsNotNull(TestDisposable.ParamValue);
            Assert.IsFalse(TestDisposable.ParamValue.Value);
        }

        [TestMethod]
        public void TestDoubleDisposedCalled()
        {
            TestDisposable.ParamValue = null;
            TestDisposable.DisposeCalled = false;
            var c = new TestDisposable();
            c.Dispose();
            Assert.IsTrue(TestDisposable.DisposeCalled);
            Assert.IsNotNull(TestDisposable.ParamValue);
            Assert.IsTrue(TestDisposable.ParamValue.Value);
            c.Dispose();
            TestDisposable.ParamValue = null;
            TestDisposable.DisposeCalled = false;
            Assert.IsFalse(TestDisposable.DisposeCalled);
            Assert.IsNull(TestDisposable.ParamValue);
        }
    }
}