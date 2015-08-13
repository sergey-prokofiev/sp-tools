using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.CommandLine;

namespace sp_tools_tests.CommandLine
{
    [TestClass]
    public class CommandLineParserTests
    {
        [TestMethod]
        public void TestSuccess()
        {
            var commands = "a.exe --switch value1 --switch2 \"c:\\folder 1\\file1.txt\" -switch-3 value-3 --switch4 -switch5 ";
            var parser = new CommandLineParser();
            var result = parser.Parse(commands);
            Assert.AreEqual(result.Count, 5);
            Assert.AreEqual(result["switch"], "value1");
            Assert.AreEqual(result["switch2"], "\"c:\\folder 1\\file1.txt\"");
            Assert.AreEqual(result["switch-3"], "value-3");
            Assert.IsTrue(result.ContainsKey("switch4"));
            Assert.IsTrue(result.ContainsKey("switch5"));
        }

        [TestMethod]
        public void TestDublicates()
        {
            var commands = "a.exe --switch value1 -switch value2";
            var parser = new CommandLineParser();
            var result = parser.Parse(commands);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result["switch"], "value2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNull()
        {
            var parser = new CommandLineParser();
            parser.Parse(null);
        }

        [TestMethod]
        public void TestEmpty()
        {
            var parser = new CommandLineParser();
            var result = parser.Parse("");
            Assert.AreEqual(result.Count, 0);
        }
    }
}