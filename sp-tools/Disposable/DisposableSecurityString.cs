using System;
using System.Runtime.InteropServices;
using System.Security;
using Common.Logging;

namespace SpTools.Disposable
{
	/// <summary>
	/// Wrapper which help to decrypt security string to common string.
	/// Implements IDisposable to clear encrypted string asap after using.
	/// Use it only in rare cases to pass password somewhere.
	/// </summary>
	public class DisposableSecurityString : DisposableResource
	{
		private readonly IntPtr _passwordPointer;
	    private string _str;
		private static readonly ILog Logger = LogManager.GetLogger<DisposableSecurityString>();

		/// <summary>
		/// Decripted string. Valid until dispose not called.
		/// </summary>
		public string DecryptedString { get {return _str;} }

		/// <summary>
		/// Creates a wrapper.
		/// </summary>
		/// <param name="source">Source secure string</param>
		public DisposableSecurityString(SecureString source)
		{
			_passwordPointer = Marshal.SecureStringToBSTR(source);
            _str = Marshal.PtrToStringBSTR(_passwordPointer);
			Logger.TraceFormat("disposable string created");
		}

		/// <summary>
		/// Dispose decrypted string.
		/// </summary>
		protected override void DisposeResources(bool disposeManagedResources)
		{
		    _str = String.Empty;
            Marshal.ZeroFreeBSTR(_passwordPointer);
			Logger.TraceFormat("disposable string disposed");
		}
	}
}
