using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Expression;
using System.Linq.Expressions;

namespace sp_tools_tests.Expression
{
    [TestClass]
    public class ExpressionsAndOrTests
    {
        public class Helper
        {
            public int Value { get; set; }
            public int Value2 { get; set; }
        }
        private List<Helper> GetValues()
        {
            return new List<Helper>
            {
                new Helper { Value = 1 }, new Helper { Value = 2 }, new Helper { Value = 3 }, new Helper { Value = 4 }, new Helper { Value = 5 }, 
                new Helper { Value = 6 }, new Helper { Value = 7 }, new Helper { Value = 8 }, new Helper { Value = 9 }, new Helper { Value = 10 }
            };
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AndTestParameter1Null()
        {
            Expression<Func<Helper, bool>> where = x => x.Value == 5;
            var lst = ExpressionUtils.And(null, where);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AndTestParameter2Null()
        {
            Expression<Func<Helper, bool>> where = x => x.Value == 5;
            var lst = ExpressionUtils.And(where, null);
        }

        [TestMethod]
        public void AndTestSuccess()
        {
            var data = GetValues();

            Expression<Func<Helper, bool>> firstCondition = x => x.Value == 5;
            Expression<Func<Helper, bool>> secondCondition = y => y.Value2 == 0;
            var and = ExpressionUtils.And(firstCondition, secondCondition);

            var result = data.AsQueryable().Where(and).ToArray();

            Assert.AreEqual(result.Length, 1);
            Assert.AreEqual(result[0].Value, 5);
            Assert.AreEqual(result[0].Value2, 0);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OrTestParameter1Null()
        {
            Expression<Func<Helper, bool>> where = x => x.Value == 5;
            var lst = ExpressionUtils.Or(null, where);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OrTestParameter2Null()
        {
            Expression<Func<Helper, bool>> where = x => x.Value == 5;
            var lst = ExpressionUtils.Or(where, null);
        }

        [TestMethod]
        public void OrTestSuccess()
        {
            var data = GetValues();

            Expression<Func<Helper, bool>> firstCondition = x => x.Value == 5;
            Expression<Func<Helper, bool>> secondCondition = y => y.Value == 7;
            var and = ExpressionUtils.Or(firstCondition, secondCondition);

            var result = data.AsQueryable().Where(and).ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Value, 5);
            Assert.AreEqual(result[1].Value, 7);

        }


    }
}