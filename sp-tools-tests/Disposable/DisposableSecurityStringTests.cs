using System;
using System.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Disposable;

namespace sp_tools_tests.Disposable
{
    [TestClass]
    public class DisposableSecurityStringTests
    {
        [TestMethod]
        public void TestDisposeCalled()
        {
            var str = new SecureString();
            var st = "Hello";
            foreach (var c in st)
            {
                str.AppendChar(c);
            }
            var xx = new DisposableSecurityString(str);
            Assert.AreEqual(st, xx.DecriptedString);
            xx.Dispose();
            Assert.AreEqual(String.Empty, xx.DecriptedString);
        }
    }
}