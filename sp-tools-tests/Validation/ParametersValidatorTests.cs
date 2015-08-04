using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Validation;

namespace sp_tools_tests.Validation
{
    [TestClass]
    public class ParametersValidatorTests
    {
        [TestMethod]
        public void IsNotNullPassTest()
        {
            var obj = new object();
            ParametersValidator.IsNotNull(obj, () => obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullThrowTest()
        {
            object obj = null;
            ParametersValidator.IsNotNull(obj, () => obj);
        }

        [TestMethod]
        public void IsNotNullOrWhiteSpacePassTest()
        {
            const string st = "Test String";
            ParametersValidator.IsNotNullOrWhiteSpace(st, () => st);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrWhiteSpaceWithNullTest()
        {
            string st = null;
            ParametersValidator.IsNotNullOrWhiteSpace(st, () => st);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrWhiteSpaceWithEmptyTest()
        {
            var st = String.Empty;
            ParametersValidator.IsNotNullOrWhiteSpace(st, () => st);
        }

        [TestMethod]
        public void IsNotNullOrEmptyPassTest()
        {
            const string st = "Test String";
            ParametersValidator.IsNotNullOrEmpty(st, () => st);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrEmptyWithNullTest()
        {
            string st = null;
            ParametersValidator.IsNotNullOrEmpty(st, () => st);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrEmptyWithEmptyTest()
        {
            var st = String.Empty;
            ParametersValidator.IsNotNullOrEmpty(st, () => st);
        }

        [TestMethod]
        public void IsNotEmptyGuidPassTest()
        {
            var id = Guid.NewGuid();
            ParametersValidator.IsNotEmpty(id, () => id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsNotEmptyGuidTest()
        {
            var id = Guid.Empty;
            ParametersValidator.IsNotEmpty(id, () => id);
        }

        [TestMethod]
        public void IsNotNullOrEmptyIEnumerablePassTest()
        {
            var array = new[] { "one", "two" };
            ParametersValidator.IsNotNullOrEmpty(array, () => array);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNullIEnumerableTest()
        {
            object[] array = null;
            ParametersValidator.IsNotNullOrEmpty(array, () => array);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsEmptyIEnumerableTest()
        {
            var array = new object[] { };
            ParametersValidator.IsNotNullOrEmpty(array, () => array);
        }
    }
}