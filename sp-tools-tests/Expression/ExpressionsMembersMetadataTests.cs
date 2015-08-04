using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Expression;

namespace sp_tools_tests.Expression
{
    [TestClass]
    public class ExpressionsMembersMetadataTests
    {
        public class Helper
        {
            public int Value { get; set; }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMemberInfoNullParam()
        {
            ExpressionUtils.GetMemberInfo<object, int>(null);
        }

        [TestMethod]
        public void GetMemberInfoSuccess()
        {
            var i = ExpressionUtils.GetMemberInfo<Helper, int>(h=>h.Value);
            var i2 = typeof (Helper).GetProperty("Value");
            Assert.AreSame(i, i2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMemberNameNullParam()
        {
            ExpressionUtils.GetMemberName<object, int>(null);
        }

        [TestMethod]
        public void GetMemberNameSuccess()
        {
            var n = ExpressionUtils.GetMemberName<Helper, int>(h => h.Value);
            Assert.AreEqual(n, "Value");
        }
    }
}