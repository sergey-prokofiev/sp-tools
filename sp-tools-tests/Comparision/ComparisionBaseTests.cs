using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SpTools.Comparision;

namespace sp_tools_tests.Comparision
{

    public class ComparableTestClass : ComparableBase<ComparableTestClass>
    {
        public int Value;
        public bool EqualsCalled = false;
        public bool GetHashCalled = false;

        protected override bool Equals(ComparableTestClass other)
        {
            EqualsCalled = true;
            return Value == other.Value;
        }

        protected override int GetHash()
        {
            GetHashCalled = true;
            return Value.GetHashCode();
        }
    }

    public class ComparableTestClass2 : ComparableBase<ComparableTestClass2>
    {
        public int Value;
        public bool EqualsCalled = false;
        public bool GetHashCalled = false;

        protected override bool Equals(ComparableTestClass2 other)
        {
            EqualsCalled = true;
            return Value == other.Value;
        }

        protected override int GetHash()
        {
            GetHashCalled = true;
            return Value.GetHashCode();
        }
    }


    [TestClass]
    public class ComparisionBaseTests
    {
        [TestMethod]
        public void TestEqualsWithNull()
        {
            var obj = new ComparableTestClass();
            var result = obj.Equals(null);
            Assert.IsFalse(result);
            Assert.IsFalse(obj.EqualsCalled);
            Assert.IsFalse(obj.GetHashCalled);
        }

        [TestMethod]
        public void TestEqualsWithSelf()
        {
            var obj = new ComparableTestClass();
            var obj2 = obj;
            var result = obj.Equals(obj2);
            Assert.IsTrue(result);
            Assert.IsFalse(obj.EqualsCalled);
            Assert.IsFalse(obj.GetHashCalled);
        }

        [TestMethod]
        public void TestEqualsWithAnother()
        {
            var obj = new ComparableTestClass();            
            var obj2 = new ComparableTestClass();

            var result = obj.Equals(obj2);
            Assert.IsTrue(result);
            Assert.IsTrue(obj.EqualsCalled);
            Assert.IsFalse(obj.GetHashCalled);
        }

        [TestMethod]
        public void TestEqualsWithDiffTypes()
        {
            var obj = new ComparableTestClass();
            var obj2 = new ComparableTestClass2();

            var result = obj.Equals(obj2);
            Assert.IsFalse(result);
            Assert.IsFalse(obj.EqualsCalled);
            Assert.IsFalse(obj.GetHashCalled);
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            var obj = new ComparableTestClass();

            obj.GetHashCode();

            Assert.IsTrue(obj.GetHashCalled);
            Assert.IsFalse(obj.EqualsCalled);
        }
    }
}
