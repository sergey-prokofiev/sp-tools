namespace SpTools.Disposable
{
    /// <summary>
    /// Lock mode for simple locks
    /// </summary>
    public enum LockMode
    {
        /// <summary>
        /// Define a read lock mode
        /// </summary>
        Read,
        /// <summary>
        /// Define a upgrate to write lock
        /// </summary>
        UpgrateToWrite,

        /// <summary>
        /// Define a write lock
        /// </summary>
        Write
    }
}