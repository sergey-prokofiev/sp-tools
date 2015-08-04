using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpTools.Disposable;

namespace sp_tools_tests.Disposable
{
    [TestClass]
    public class DisposableReaderWriterLockSlimTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorExceptionTest()
        {
            var a = new DisposableReaderWriterLockSlim(null);
        }

        [TestMethod]
        public void CtorExceptionTestSuccess()
        {
            var a = new DisposableReaderWriterLockSlim(new ReaderWriterLockSlim());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CantAqureLock()
        {

            var l = new ReaderWriterLockSlim();
            var e = new AutoResetEvent(false);
            var t = new Thread(() =>
            {
                var b =l.TryEnterWriteLock(TimeSpan.FromSeconds(1));
                Assert.IsTrue(b);
                e.Set();
                Thread.Sleep(TimeSpan.FromMinutes(10));
                l.Dispose();
            });
            t.Start();
            e.WaitOne();
            var a = new DisposableReaderWriterLockSlim(l, TimeSpan.FromSeconds(3), SlimLockMode.Write);
        }

        [TestMethod]
        public void AddAndGet()
        {
            var c = new SynchronizedCache();
            var lst = new List<Task>();
            for (var i = 0; i < 10; i++)
            {
                var i1 = i;
                var t = new Task(() => c.Add(i1, i1.ToString()));
                t.Start();
                lst.Add(t);
            }
            Task.WaitAll(lst.ToArray());
            Assert.AreEqual(c.Count, 10);
            lst.Clear();
            for (var i = 0; i < 10; i++)
            {
                var i1 = i;
                var t = new Task(() =>
                {
                    var s = c.Read(i1);
                    Assert.AreEqual(s, i1.ToString());
                    c.Update(i1, "42");
                });
                t.Start();
                lst.Add(t);
            }
            Task.WaitAll(lst.ToArray());
            for (var i = 0; i < 10; i++)
            {
                var s = c.Read(i);
                Assert.AreEqual(s, "42");
            }
        }

        public class SynchronizedCache
        {
            private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
            private Dictionary<int, string> innerCache = new Dictionary<int, string>();

            public int Count
            { get { return innerCache.Count; } }

            public string Read(int key)
            {
                using (new DisposableReaderWriterLockSlim(cacheLock))
                {
                    return innerCache[key];
                }
            }

            public void Add(int key, string value)
            {
                using (new DisposableReaderWriterLockSlim(cacheLock, SlimLockMode.Write))
                {
                    innerCache.Add(key, value);
                }
            }


            public void Update(int key, string value)
            {
                using (new DisposableReaderWriterLockSlim(cacheLock, SlimLockMode.UpgradeableRead))
                {
                    string result;
                    if (innerCache.TryGetValue(key, out result))
                    {
                        if (result != value)
                        {
                            using (new DisposableReaderWriterLockSlim(cacheLock, SlimLockMode.Write))
                            {
                                innerCache[key] = value;
                            }
                        }
                    }
                }
            }

            ~SynchronizedCache()
            {
                cacheLock?.Dispose();
            }
        }

    }
}