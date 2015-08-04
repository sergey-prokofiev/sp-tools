using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Wrappers;

namespace sp_tools_tests.Wrappers
{
    [TestClass]
    public class FileSystemProxyTests
    {
        [TestMethod]
        public void CreateTempDirectoryTest()
        {
            var p = new FileSystemProxy();
            var s = p.CreateTempEmptyFolder();
            var b = Directory.Exists(s);
            Assert.IsTrue(b);
            Directory.Delete(s);
        }
        [TestMethod]
        public void DeleteFileTest()
        {
            var p = new FileSystemProxy();
            var s = p.CreateTempEmptyFolder();
            var fname = p + "1.txt";
            using (var f = File.CreateText(fname))
            {
                
            }
            var b = File.Exists(fname);
            Assert.IsTrue(b);
            p.DeleteFile(fname);
            b = File.Exists(fname);
            Assert.IsFalse(b);
            Directory.Delete(s, true);
        }

        [TestMethod]
        public void DeleteDirTest()
        {
            var p = new FileSystemProxy();
            var s = p.CreateTempEmptyFolder();
            var b = Directory.Exists(s);
            Assert.IsTrue(b);
            p.DeleteDirectory(s);
            b = Directory.Exists(s);
            Assert.IsFalse(b);
        }

        [TestMethod]
        public void CombineTest()
        {
            var p1 = "c:/1";
            var p2 = "a.txt";
            var p = new FileSystemProxy();
            var res = p.Combine(p1, p2);
            Assert.AreEqual(res, "c:/1\\a.txt");
        }

        private void CreateFiles(string dir)
        {
            var fnames = new[] { "1.txt", "1.doc", "2.txt" };
            foreach (var f in fnames)
            {
                var path = dir + f;
                using (File.CreateText(path))
                {

                }
            }
        }
        [TestMethod]
        public void FindFilesInSubDirTest()
        {
            var p = new FileSystemProxy();
            var dir = p.CreateTempEmptyFolder();
            CreateFiles(dir);
            var subdir = Path.Combine(dir, "1");
            Directory.CreateDirectory(subdir);
            CreateFiles(subdir+ "\\");
            var result = p.FindFiles(dir, "*.txt", true).ToList();
            Assert.AreEqual(result.Count, 4);
            var i1 = p.Combine(dir, "1.txt");
            var i2 = p.Combine(dir, "2.txt");
            var i3 = p.Combine(subdir, "1.txt");
            var i4 = p.Combine(subdir, "2.txt");            
            Assert.IsTrue(result.Contains(i1));
            Assert.IsTrue(result.Contains(i2));
            Assert.IsTrue(result.Contains(i3));
            Assert.IsTrue(result.Contains(i4));
            Directory.Delete(dir, true);
        }
        [TestMethod]
        public void FindFilesNoSubDirTest()
        {
            var p = new FileSystemProxy();
            var dir = p.CreateTempEmptyFolder();
            CreateFiles(dir);
            var subdir = Path.Combine(dir, "1");
            Directory.CreateDirectory(subdir);
            CreateFiles(subdir + "\\");
            var result = p.FindFiles(dir, "*.txt", false).ToList();
            Assert.AreEqual(result.Count, 2);
            var i1 = p.Combine(dir, "1.txt");
            var i2 = p.Combine(dir, "2.txt");
            Assert.IsTrue(result.Contains(i1));
            Assert.IsTrue(result.Contains(i2));
            Directory.Delete(dir, true);
        }
    }
}