using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SpTools.Disposable;

namespace sp_tools_tests.Disposable
{
    [TestClass]
    public class DisposableExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunSafeNullDisposableTest()
        {
            IDisposable d = null;
            d.RunSafe(() => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunSafeNullActionTest()
        {
            var d = Substitute.For<IDisposable>();
            d.RunSafe(null);
        }

        [TestMethod]
        public void RunSafeSuccessTest()
        {
            var d = Substitute.For<IDisposable>();
            var b = false;
            d.RunSafe(()=>b=true);
            Assert.IsTrue(b);
            d.Received(1).Dispose();
        }

        [TestMethod]
        public void RunSafeWithExceptionTest()
        {
            var d = Substitute.For<IDisposable>();
            var b = false;
            try
            {
                d.RunSafe(() => { throw new InvalidOperationException(); });
            }
            catch (InvalidOperationException)
            {
                b = true;
            }
            Assert.IsTrue(b);
            d.Received(1).Dispose();
        }
#region Slim
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithReadLockSlimNullLockTest()
        {
            ReaderWriterLockSlim l = null;
            l.WithReadLock(() => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithReadLockSlimNullActionTest()
        {
            var l = new ReaderWriterLockSlim();
            l.WithReadLock(null);
        }

        [TestMethod]
        public void WithReadLockSlimSuccessTest()
        {
            var l = new ReaderWriterLockSlim();
            Assert.IsFalse(l.IsReadLockHeld);
            l.WithReadLock(()=> {Assert.IsTrue(l.IsReadLockHeld);});
            Assert.IsFalse(l.IsReadLockHeld);
        }

        [TestMethod]
        public void WithReadLockSlimWithExceptionTest()
        {
            var l = new ReaderWriterLockSlim();
            Assert.IsFalse(l.IsReadLockHeld);
            var thrown = false;
            try
            {
                l.WithReadLock(() =>
                {
                    Assert.IsTrue(l.IsReadLockHeld);
                    throw new InvalidOperationException();
                });
            }
            catch (InvalidOperationException)
            {
                thrown = true;
            }
            Assert.IsFalse(l.IsReadLockHeld);
            Assert.IsTrue(thrown);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithReadLockSlimTimeoutNullLockTest()
        {
            ReaderWriterLockSlim l = null;
            l.WithReadLock(() => { }, TimeSpan.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithReadLockSlimTimeoutNullActionTest()
        {
            var l = new ReaderWriterLockSlim();
            l.WithReadLock(null, TimeSpan.Zero);
        }

        [TestMethod]
        public void WithReadLockSlimTimeoutSuccessTest()
        {
            var l = new ReaderWriterLockSlim();
            var e= new AutoResetEvent(false);
            var t = new Thread(() =>
            {
                l.WithReadLock(() =>
                {
                    Assert.IsTrue(l.IsReadLockHeld);
                }, TimeSpan.FromSeconds(5));
                e.Set();
            });
            l.EnterWriteLock();
            t.Start();
            Thread.Sleep(1000);
            Assert.AreEqual(l.WaitingReadCount, 1);
            l.ExitWriteLock();
            e.WaitOne();
            Assert.IsFalse(l.IsReadLockHeld);
        }

        [TestMethod]
        public void WithReadLockSlimTimeoutWithExceptionTest()
        {
            var l = new ReaderWriterLockSlim();
            var e = new AutoResetEvent(false);
            var t = new Thread(() =>
            {
                var b = false;
                try
                {
                    l.WithReadLock(() =>
                    {
                        Assert.IsTrue(l.IsReadLockHeld);
                        throw new InvalidOperationException();
                    }, TimeSpan.FromSeconds(5));
                }
                catch (InvalidOperationException)
                {
                    b = true;
                }
                Assert.IsTrue(b);
                e.Set();
            });
            l.EnterWriteLock();
            t.Start();
            Thread.Sleep(1000);
            Assert.AreEqual(l.WaitingReadCount, 1);
            l.ExitWriteLock();
            e.WaitOne();
            Assert.IsFalse(l.IsReadLockHeld);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithWriteLockSlimNullLockTest()
        {
            ReaderWriterLockSlim l = null;
            l.WithWriteLock(() => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithhWriteLockSlimNullActionTest()
        {
            var l = new ReaderWriterLockSlim();
            l.WithWriteLock(null);
        }

        [TestMethod]
        public void WithWriteLockSlimSuccessTest()
        {
            var l = new ReaderWriterLockSlim();
            Assert.IsFalse(l.IsWriteLockHeld);
            l.WithWriteLock(() => { Assert.IsTrue(l.IsWriteLockHeld); });
            Assert.IsFalse(l.IsWriteLockHeld);
        }

        [TestMethod]
        public void WithWriteLockSlimWithExceptionTest()
        {
            var l = new ReaderWriterLockSlim();
            Assert.IsFalse(l.IsWriteLockHeld);
            var thrown = false;
            try
            {
                l.WithWriteLock(() =>
                {
                    Assert.IsTrue(l.IsWriteLockHeld);
                    throw new InvalidOperationException();
                });
            }
            catch (InvalidOperationException)
            {
                thrown = true;
            }
            Assert.IsFalse(l.IsWriteLockHeld);
            Assert.IsTrue(thrown);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithWriteLockSlimTimeoutNullLockTest()
        {
            ReaderWriterLockSlim l = null;
            l.WithWriteLock(() => { }, TimeSpan.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithWriteLockSlimTimeoutNullActionTest()
        {
            var l = new ReaderWriterLockSlim();
            l.WithWriteLock(null, TimeSpan.Zero);
        }

        [TestMethod]
        public void WithWriteLockSlimTimeoutSuccessTest()
        {
            var l = new ReaderWriterLockSlim();
            var e = new AutoResetEvent(false);
            var t = new Thread(() =>
            {
                l.WithWriteLock(() =>
                {
                    Assert.IsTrue(l.IsWriteLockHeld);
                }, TimeSpan.FromSeconds(5));
                e.Set();
            });
            l.EnterWriteLock();
            t.Start();
            Thread.Sleep(1000);
            Assert.AreEqual(l.WaitingWriteCount, 1);
            l.ExitWriteLock();
            e.WaitOne();
            Assert.IsFalse(l.IsWriteLockHeld);
        }

        [TestMethod]
        public void WithWriteLockSlimTimeoutWithExceptionTest()
        {
            var l = new ReaderWriterLockSlim();
            var e = new AutoResetEvent(false);
            var t = new Thread(() =>
            {
                var b = false;
                try
                {
                    l.WithWriteLock(() =>
                    {
                        Assert.IsTrue(l.IsWriteLockHeld);
                        throw new InvalidOperationException();
                    }, TimeSpan.FromSeconds(5));
                }
                catch (InvalidOperationException)
                {
                    b = true;
                }
                Assert.IsTrue(b);
                e.Set();
            });
            l.EnterWriteLock();
            t.Start();
            Thread.Sleep(1000);
            Assert.AreEqual(l.WaitingWriteCount, 1);
            l.ExitWriteLock();
            e.WaitOne();
            Assert.IsFalse(l.IsWriteLockHeld);
        }
        #endregion
        #region No Slim
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithReadLockNullLockTest()
        {
            ReaderWriterLock l = null;
            l.WithReadLock(() => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithReadLockNullActionTest()
        {
            var l = new ReaderWriterLock();
            l.WithReadLock(null);
        }

        [TestMethod]
        public void WithReadLockSuccessTest()
        {
            var l = new ReaderWriterLock();
            Assert.IsFalse(l.IsReaderLockHeld);
            l.WithReadLock(() => { Assert.IsTrue(l.IsReaderLockHeld); });
            Assert.IsFalse(l.IsReaderLockHeld);
        }

        [TestMethod]
        public void WithReadLockWithExceptionTest()
        {
            var l = new ReaderWriterLock();
            Assert.IsFalse(l.IsReaderLockHeld);
            var thrown = false;
            try
            {
                l.WithReadLock(() =>
                {
                    Assert.IsTrue(l.IsReaderLockHeld);
                    throw new InvalidOperationException();
                });
            }
            catch (InvalidOperationException)
            {
                thrown = true;
            }
            Assert.IsFalse(l.IsReaderLockHeld);
            Assert.IsTrue(thrown);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithReadLockTimeoutNullLockTest()
        {
            ReaderWriterLock l = null;
            l.WithReadLock(() => { }, TimeSpan.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithReadLockTimeoutNullActionTest()
        {
            var l = new ReaderWriterLock();
            l.WithReadLock(null, TimeSpan.Zero);
        }

        [TestMethod]
        public void WithReadLockTimeoutSuccessTest()
        {
            var l = new ReaderWriterLock();
            var e = new AutoResetEvent(false);
            var lockGetted = false;
            var t = new Thread(() =>
            {
                l.WithReadLock(() =>
                {
                    Assert.IsTrue(l.IsReaderLockHeld);
                    lockGetted = true;
                }, TimeSpan.FromSeconds(5));
                e.Set();
            });
            l.AcquireWriterLock(100);
            t.Start();
            Thread.Sleep(1000);
            Assert.IsFalse(lockGetted);
            l.ReleaseWriterLock();
            e.WaitOne();
            Assert.IsTrue(lockGetted);
        }

        [TestMethod]
        public void WithReadLockTimeoutWithExceptionTest()
        {
            var l = new ReaderWriterLock();
            var e = new AutoResetEvent(false);
            var lockGetted = false;
            var t = new Thread(() =>
            {
                var b = false;
                try
                {
                    l.WithReadLock(() =>
                    {
                        lockGetted = true;
                        Assert.IsTrue(l.IsReaderLockHeld);
                        throw new InvalidOperationException();
                    }, TimeSpan.FromSeconds(5));
                }
                catch (InvalidOperationException)
                {
                    b = true;
                }
                Assert.IsTrue(b);
                e.Set();
            });
            l.AcquireWriterLock(100);
            t.Start();
            Thread.Sleep(1000);
            Assert.IsFalse(lockGetted);
            l.ReleaseWriterLock();
            e.WaitOne();
            Assert.IsFalse(l.IsReaderLockHeld);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithWriteLockNullLockTest()
        {
            ReaderWriterLock l = null;
            l.WithWriteLock(() => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithhWriteLockNullActionTest()
        {
            var l = new ReaderWriterLock();
            l.WithWriteLock(null);
        }

        [TestMethod]
        public void WithWriteLockSuccessTest()
        {
            var l = new ReaderWriterLock();
            Assert.IsFalse(l.IsWriterLockHeld);
            l.WithWriteLock(() => { Assert.IsTrue(l.IsWriterLockHeld); });
            Assert.IsFalse(l.IsWriterLockHeld);
        }

        [TestMethod]
        public void WithWriteLockWithExceptionTest()
        {
            var l = new ReaderWriterLock();
            Assert.IsFalse(l.IsWriterLockHeld);
            var thrown = false;
            try
            {
                l.WithWriteLock(() =>
                {
                    Assert.IsTrue(l.IsWriterLockHeld);
                    throw new InvalidOperationException();
                });
            }
            catch (InvalidOperationException)
            {
                thrown = true;
            }
            Assert.IsFalse(l.IsWriterLockHeld);
            Assert.IsTrue(thrown);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithWriteLockTimeoutNullLockTest()
        {
            ReaderWriterLock l = null;
            l.WithWriteLock(() => { }, TimeSpan.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithWriteLockTimeoutNullActionTest()
        {
            var l = new ReaderWriterLock();
            l.WithWriteLock(null, TimeSpan.Zero);
        }

        [TestMethod]
        public void WithWriteLockTimeoutSuccessTest()
        {
            var l = new ReaderWriterLock();
            var e = new AutoResetEvent(false);
            var lockGetted = false;
            var t = new Thread(() =>
            {
                l.WithWriteLock(() =>
                {
                    lockGetted = true;
                    Assert.IsTrue(l.IsWriterLockHeld);
                }, TimeSpan.FromSeconds(5));
                e.Set();
            });
            l.AcquireWriterLock(100);
            t.Start();
            Thread.Sleep(1000);
            Assert.IsFalse(lockGetted);
            l.ReleaseWriterLock();
            e.WaitOne();
            Assert.IsTrue(lockGetted);
            Assert.IsFalse(l.IsWriterLockHeld);
        }

        [TestMethod]
        public void WithWriteLockTimeoutWithExceptionTest()
        {
            var l = new ReaderWriterLock();
            var e = new AutoResetEvent(false);
            var lockGetted = false;
            var t = new Thread(() =>
            {
                var b = false;
                try
                {
                    l.WithWriteLock(() =>
                    {
                        lockGetted = true;
                        Assert.IsTrue(l.IsWriterLockHeld);
                        throw new InvalidOperationException();
                    }, TimeSpan.FromSeconds(5));
                }
                catch (InvalidOperationException)
                {
                    b = true;
                }
                Assert.IsTrue(b);
                e.Set();
            });
            l.AcquireWriterLock(100);
            t.Start();
            Thread.Sleep(1000);
            Assert.IsFalse(lockGetted);
            l.ReleaseWriterLock();
            e.WaitOne();
            Assert.IsTrue(lockGetted);
            Assert.IsFalse(l.IsWriterLockHeld);
        }
        
        #endregion
    }
}