namespace SpTools.Comparision
{
	/// <summary>
	/// Base class for which simplifies implementation of object's comparision
	/// </summary>
	/// <typeparam name="TRealType">Real type of the object</typeparam>
	public abstract class ComparableBase<TRealType>
	{
		/// <summary>
		/// <see cref="object.Equals(object)"/>
		/// </summary>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((TRealType)obj);
		}

		/// <summary>
		/// Calls GetHash()
		/// </summary>
		public override int GetHashCode()
		{
			return GetHash();
		}


		/// <summary>
		/// Compare current enity with passed into arguments
		/// </summary>
		protected abstract bool Equals(TRealType other);

		/// <summary>
		/// Should be overrided to get hash code
		/// </summary>
		protected abstract int GetHash();

	}
}