namespace SpTools.Wrappers
{
    /// <summary>
    /// Interface to map entites
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// Creates a new entity of TDest type and map it from source
        /// </summary>
        TDest Map<TSrc, TDest>(TSrc source);
        
        /// <summary>
        /// MNaps from source to destination
        /// </summary>
        TDest Map<TSrc, TDest>(TSrc source, TDest destination);
    }
}
