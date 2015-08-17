using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Wrappers;

namespace sp_tools_tests.Wrappers
{
    [TestClass]
    public class DateTimeProxyTests
    {
        [TestMethod]
        public void TestNow()
        {
            var w = new DateTimeProxy();
            var dt = DateTime.Now;
            var dt2 = w.Now();
            var diff = dt2 - dt;
            Assert.IsTrue(diff.TotalSeconds < 2);
        }

        [TestMethod]
        public void TestUtcNow()
        {
            var w = new DateTimeProxy();
            var dt = DateTime.UtcNow;
            var dt2 = w.UtcNow();
            var diff = dt2 - dt;
            Assert.IsTrue(diff.TotalSeconds < 2);
        }

        [TestMethod]
        public void TestOffset()
        {
            var w = new DateTimeProxy();
            var o1 = DateTimeOffset.Now.Offset;
            var o2 = w.CurrentOffset();
            Assert.AreEqual(o1, o2);
        }
    }
}