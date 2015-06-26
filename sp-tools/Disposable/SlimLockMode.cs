namespace SpTools.Disposable
{
	/// <summary>
	/// Type of lock
	/// </summary>
	public enum SlimLockMode
	{
		/// <summary>
		/// Read lock
		/// </summary>
		Read,

		/// <summary>
		/// Read lock, can be upgrated to write lock
		/// </summary>
		UpgradeableRead,

		/// <summary>
		/// Write lock
		/// </summary>
		Write
	}
}
