
namespace SpTools.Wrappers
{
	/// <summary>
	/// Maps objects using AutoMapper.
	/// </summary>
	public class AutoMapperObjectMapper : IObjectMapper
	{
		////////////////////////////////////////////////////////////
		// Public Methods/Atributes
		////////////////////////////////////////////////////////////

		/// <summary>
		/// Maps source object to destination object.
		/// </summary>
		/// <typeparam name="TSrc">Source type.</typeparam>
		/// <typeparam name="TDest">Destination type.</typeparam>
		/// <param name="source">Source object.</param>
		/// <returns>Destination object.</returns>
		public TDest Map<TSrc, TDest>(TSrc source)
		{
			return AutoMapper.Mapper.Map<TSrc, TDest>(source);
		}

        /// <summary>
        /// Maps source object to an existing destination object
        /// </summary>
        /// <typeparam name="TSrc">Source type</typeparam>
        /// <typeparam name="TDest">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <returns>Destination object with updated properties</returns>
	    public TDest Map<TSrc, TDest>(TSrc source, TDest destination)
	    {
	        return AutoMapper.Mapper.Map(source, destination);
	    }
	}
}
