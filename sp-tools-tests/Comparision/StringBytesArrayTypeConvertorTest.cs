using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Comparision;

namespace sp_tools_tests.Comparision
{
    [TestClass]
    public class StringBytesArrayTypeConvertorTest
    {
        [TestMethod]
        public void CanConvertToTests()
        {
            var c = new StringBytesArrayTypeConverter();
            var r1= c.CanConvertTo(null, typeof (byte[]));
            Assert.IsTrue(r1);
            var r2 = c.CanConvertTo(null, typeof(byte));
            Assert.IsFalse(r2);
        }

        [TestMethod]
        public void CanConvertFromTests()
        {
            var c = new StringBytesArrayTypeConverter();
            var r1 = c.CanConvertFrom(null, typeof(string));
            Assert.IsTrue(r1);
            var r2 = c.CanConvertFrom(null, typeof(byte));
            Assert.IsFalse(r2);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertFromInt()
        {
            var c = new StringBytesArrayTypeConverter();
            c.ConvertFrom(24);
        }

        [TestMethod]
        public void ConvertFromSuccessTest()
        {
            var c = new StringBytesArrayTypeConverter();
            var r2 = c.ConvertFrom("XXXX") as byte[];
            var r3 = Convert.FromBase64String("XXXX");
            Assert.AreEqual(r2.Length, r3.Length);
            for (var i = 0; i < r2.Length; i++)
            {
                Assert.AreEqual(r2[i], r3[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertToInt()
        {
            var c = new StringBytesArrayTypeConverter();
            c.ConvertTo("ZZZ", typeof(int));
        }

        [TestMethod]
        public void ConvertToSuccessTest()
        {
            var c = new StringBytesArrayTypeConverter();
            var b = Convert.FromBase64String("XXXX");
            var r2 = c.ConvertTo(b, typeof(string)) as string;
            Assert.AreEqual(r2, "XXXX");
        }
    }
}