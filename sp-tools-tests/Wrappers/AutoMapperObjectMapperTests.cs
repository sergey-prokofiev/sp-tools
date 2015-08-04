using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Wrappers;

namespace sp_tools_tests.Wrappers
{
    [TestClass]
    public class AutoMapperObjectMapperTests
    {
        class H1
        {
            public int Value { get; set; }
        }
        class H2
        {
            public int Value { get; set; }
        }
        [TestInitialize]
        public void Init()
        {
            AutoMapper.Mapper.CreateMap<H1, H2>();
        }
        [TestMethod]
        public void MapToNewTests()
        {
            var obj = new AutoMapperObjectMapper();
            var result = obj.Map<H1, H2>(new H1 {Value = 42});
            Assert.AreEqual(result.Value, 42);
        }
        [TestMethod]
        public void MapToExistentTests()
        {
            var obj = new AutoMapperObjectMapper();
            var result = new H2();
            result = obj.Map(new H1 { Value = 42 }, result);
            Assert.AreEqual(result.Value, 42);
        }
    }
}