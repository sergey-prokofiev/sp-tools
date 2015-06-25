#region File Description & License
// ****************************************************************
// DESC: StringBytesArrayTypeConvertor.cs implementation.
//  
// © 2013 McGraw-Hill Education
// ****************************************************************
#endregion

using System;
using System.ComponentModel;
using System.Globalization;

namespace Tegrity.Utilities
{
	/// <summary>
	/// Converts the value of an string type into a byte array type
	/// </summary>
	public class StringBytesArrayTypeConverter : TypeConverter
	{
		////////////////////////////////////////////////////////////
		// Public Methods/Atributes
		////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets a value indicating whether this converter can convert an object to the given destination type using the context.
        /// </summary>
		/// <param name="context"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(byte[]) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>
		///	Gets a value indicating whether this converter can
		/// convert an object in the given source type to the native type of the converter using the context.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sourceType"></param>
		/// <returns></returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// Converts the given object to the type of this converter, using the specified context and culture information.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// An <see cref="T:System.Object"/> that represents the converted value.
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var s = value as string;
			return s != null ? Convert.FromBase64String(s) : base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Converts the given value object to the specified type, using the specified context and culture information.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// An <see cref="T:System.Object"/> that represents the converted value.
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is byte[] && destinationType == typeof(string))
			{
				return Convert.ToBase64String((byte[]) value);
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}