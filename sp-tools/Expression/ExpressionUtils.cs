#region File Description & License
// ****************************************************************
// DESC: ExpressionUtils.cs implementation.
//  
// © 2013 McGraw-Hill Education
// ****************************************************************
#endregion

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Tegrity.Utilities
{
	/// <summary>
	/// Expression utils.
	/// </summary>
	public static class ExpressionUtils 
	{
		////////////////////////////////////////////////////////////
		// Public Methods/Atributes
		////////////////////////////////////////////////////////////

		/// <summary>
		/// Joins two expressions with logical And.
		/// </summary>
		/// <typeparam name="T">Object type.</typeparam>
		/// <param name="first">First expression.</param>
		/// <param name="second">Second expression.</param>
		/// <returns>Join of two expressions with logical And.</returns>
		public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			var andExpression = Expression.And(first.Body, second.Body);
			var result = Expression.Lambda<Func<T, bool>>(andExpression, first.Parameters[0]);
			return result;
		}

		/// <summary>
		/// Gets property info based on property expression.
		/// </summary>
		/// <typeparam name="TEntity">Entity type.</typeparam>
		/// <typeparam name="TProperty">Member type.</typeparam>
		/// <param name="memberExpression">Member expression.</param>
		/// <returns>Member info.</returns>
		public static MemberInfo GetMemberInfo<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> memberExpression)
		{
			var memberAccess = (MemberExpression) memberExpression.Body;
			return memberAccess.Member;
		}

		/// <summary>
		/// Gets member name based on property expression.
		/// </summary>
		/// <typeparam name="TEntity">Entity type.</typeparam>
		/// <typeparam name="TProperty">Member type.</typeparam>
		/// <param name="memberExpression">Member expression.</param>
		/// <returns>Member name.</returns>
		public static string GetMemberName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> memberExpression)
		{
			return GetMemberInfo(memberExpression).Name;
		}

		/// <summary>
		/// Gets Where expression by object type, property name and selector value.
		/// </summary>
		/// <typeparam name="T">Object type.</typeparam>
		/// <param name="property">Property name.</param>
		/// <param name="value">Object value.</param>
		/// <returns></returns>
		public static Expression<Func<T, bool>> GetWhereExpression<T>(string property, object value) where T : class
		{
			Validator.IsNotNullOrWhiteSpace(property, () => property);
			Validator.IsNotNull(value, () => value);

			var propertyInfo = typeof(T).GetProperty(property);

			return GetWhereExpression<T>(propertyInfo, value);
		}

		/// <summary>
		/// Gets Where expression by object type, property info and selector value.
		/// </summary>
		/// <typeparam name="T">Object type.</typeparam>
		/// <param name="property">Property info.</param>
		/// <param name="value">Object value.</param>
		/// <returns></returns>
		public static Expression<Func<T, bool>> GetWhereExpression<T>(PropertyInfo property, object value) where T : class
		{
			Validator.IsNotNull(property, () => property);
			Validator.IsNotNull(value, () => value);

			var parameter = Expression.Parameter(typeof(T), "x");
			var propertyExpression = Expression.MakeMemberAccess(parameter, property);
			var constExpression = Expression.Constant(value, value.GetType());
			var body = Expression.Equal(propertyExpression, constExpression);
			var result = Expression.Lambda<Func<T, bool>>(body, parameter);
			return result;
		}
	}
}