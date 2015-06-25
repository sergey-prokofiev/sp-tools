#region File Description & License
// ****************************************************************
// DESC: ComparableBase.cs implementation.
//  
// © 2013 McGraw-Hill Education
// ****************************************************************
#endregion

namespace Tegrity.Utilities
{
	/// <summary>
	/// Base class for which simplifies obkect's comparision
	/// </summary>
	/// <typeparam name="TRealType">Real type of the object</typeparam>
	public abstract class ComparableBase<TRealType>
	{
		////////////////////////////////////////////////////////////
		// Public Methods/Atributes
		////////////////////////////////////////////////////////////
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


		////////////////////////////////////////////////////////////
		// Protected Methods/Atributes
		////////////////////////////////////////////////////////////
		/// <summary>
		/// Compare current enity with passed into arguments
		/// </summary>
		protected abstract bool Equals(TRealType other);

		/// <summary>
		/// Should be overrided to get hash code
		/// </summary>
		/// <returns></returns>
		protected abstract int GetHash();

	}
}