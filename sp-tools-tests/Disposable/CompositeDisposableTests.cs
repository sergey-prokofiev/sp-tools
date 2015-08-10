using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SpTools.Disposable;

namespace sp_tools_tests.Disposable
{
    [TestClass]
    public class CompositeDisposableTests
    {
        [TestMethod]
        public void TestCapacityCtorEmpty()
        {
            var d = new CompositeDisposable();
            Assert.AreEqual(0, GetCapacity(d));
        }

        [TestMethod]
        public void TestCapacityCtorParameter()
        {
            var d = new CompositeDisposable(42);
            Assert.AreEqual(42, GetCapacity(d));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCollectionCtorNull()
        {
            new CompositeDisposable(null);
        }

        [TestMethod]
        public void TestCollectionCtorSuccess()
        {
            var d1 = Substitute.For<IDisposable>();
            var d2 = Substitute.For<IDisposable>();
            using (var c = new CompositeDisposable(new[] {d1, d2}))
            {
                
            }
            d1.Received(1).Dispose();
            d2.Received(1).Dispose();
        }

        [TestMethod]
        public void TestAdd()
        {
            var d1 = Substitute.For<IDisposable>();
            var d2 = Substitute.For<IDisposable>();
            using (var c = new CompositeDisposable())
            {
                c.Add(d1);
            }
            d1.Received(1).Dispose();
            d2.Received(0).Dispose();
        }

        [TestMethod]
        public void TestAddNull()
        {
            var d1 = Substitute.For<IDisposable>();
            IDisposable d2 = null;
            using (var c = new CompositeDisposable())
            {
                c.Add(d1);
                c.Add(d2);
            }
            d1.Received(1).Dispose();
        }

        [TestMethod]
        public void TestDisposeExceptions()
        {
            var d1 = Substitute.For<IDisposable>();
            var e1 = new ArgumentException();
            d1.When(d=>d.Dispose()).Do(d => { throw e1; });
            var d2 = Substitute.For<IDisposable>();
            var e2 = new InvalidOperationException();
            d2.When(d => d.Dispose()).Do(d => { throw e2; });
            var d3 = Substitute.For<IDisposable>();
            var exceptionThrown = false;
            try
            {
                using (var c = new CompositeDisposable())
                {
                    c.Add(d1);
                    c.Add(d2);
                    c.Add(d3);
                }
            }
            catch (AggregateException e)
            {
                exceptionThrown = true;
                Assert.IsTrue(e.InnerExceptions.Contains(e1));
                Assert.IsTrue(e.InnerExceptions.Contains(e2));
            }
            Assert.IsTrue(exceptionThrown);
            d1.Received(1).Dispose();
            d2.Received(1).Dispose();
            d3.Received(1).Dispose();
        }

        private int GetCapacity(CompositeDisposable d)
        {
            var t = typeof (CompositeDisposable);
            var m = t.GetField("_lst", BindingFlags.Instance | BindingFlags.NonPublic);
            var lst = (List<IDisposable>)m.GetValue(d);
            return lst.Capacity;
        }
    }
}