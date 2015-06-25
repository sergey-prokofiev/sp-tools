using System;
using System.Runtime.InteropServices;
using System.Security;
using NLog;

namespace Jet.Puzzles.Disposable
{
	/// <summary>
	/// Wrapper which help to decrypt security string to common string.
	/// Implements IDisposable to clear encrypted string asap after using.
	/// Use it only in rare cases to pass password somewhere.
	/// </summary>
	public class DisposableSecurityString : DisposableResource
	{
		private static readonly Logger _log = LogManager.GetCurrentClassLogger(); 
		private readonly IntPtr _passwordPointer;

		/// <summary>
		/// Decripted string. Valid until dispose not called.
		/// </summary>
		public string DecriptedString { get; private set; }

		/// <summary>
		/// Creates a wrapper.
		/// </summary>
		/// <param name="source">Source secure string</param>
		public DisposableSecurityString(SecureString source)
		{
			_log.Trace("Creating secure string wrapper");
			_passwordPointer = Marshal.SecureStringToBSTR(source);
			DecriptedString = Marshal.PtrToStringBSTR(_passwordPointer);
		}

		/// <summary>
		/// Dispose decrypted string.
		/// </summary>
		protected override void DisposeResources(bool disposeManagedResources)
		{
			_log.Trace("Disposing secure string wrapper");
			Marshal.ZeroFreeBSTR(_passwordPointer);
		}
	}
}
