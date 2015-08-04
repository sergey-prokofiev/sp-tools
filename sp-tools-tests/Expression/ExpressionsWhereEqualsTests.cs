using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Expression;

namespace sp_tools_tests.Expression
{
    [TestClass]
    public class ExpressionsWhereEqualsTests
    {
        public class Helper
        {
            public int Value { get; set; }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhereEqualStPropertyEmptyStringTest()
        {
            ExpressionUtils.GetWhereEqualExpression<Helper>("", 42);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhereEqualStPropertyNullStringTest()
        {
            ExpressionUtils.GetWhereEqualExpression<Helper>((string)null, 42);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhereEqualStPropertyNullObjTest()
        {
            ExpressionUtils.GetWhereEqualExpression<Helper>("xxx", null);
        }

        [TestMethod]
        public void WhereEqualStPropertySuccess()
        {
            var ar = new[] { new Helper { Value = 1 }, new Helper { Value = 2 }, new Helper { Value = 3 } };
            var e = ExpressionUtils.GetWhereEqualExpression<Helper>("Value", 2);
            var res = ar.AsQueryable().Where(e).ToArray();
            Assert.AreEqual(res.Length, 1);
            Assert.AreEqual(res[0].Value, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhereEqualTPropertyNullStringTest()
        {
            ExpressionUtils.GetWhereEqualExpression<Helper>((PropertyInfo)null, 42);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhereEqualTPropertyNullObjTest()
        {
            var i = typeof (Helper).GetProperty("Value");
            ExpressionUtils.GetWhereEqualExpression<Helper>(i, null);
        }

        [TestMethod]
        public void WhereEqualTPropertySuccess()
        {
            var ar = new[] { new Helper { Value = 1 }, new Helper { Value = 2 }, new Helper { Value = 3 } };
            var i = typeof(Helper).GetProperty("Value");
            var e = ExpressionUtils.GetWhereEqualExpression<Helper>(i, 2);
            var res = ar.AsQueryable().Where(e).ToArray();
            Assert.AreEqual(res.Length, 1);
            Assert.AreEqual(res[0].Value, 2);
        }
    }
}
