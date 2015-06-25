using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SpTools.Validation
{
	/// <summary>
	/// Validation utility
	/// </summary>
	public static class Validator
	{
		/// <summary>
		/// Validates that parameter is not null.
		/// Usage: Validator.IsNotNull(param, () => param).
		/// </summary>
		/// <typeparam name="T">Parameter type.</typeparam>
		/// <param name="parameter">Parameter value.</param>
		/// <param name="parameterExpression">Expression to determine parameter name.</param>
		// ReSharper disable UnusedParameter.Global
		public static void IsNotNull<T>(T parameter, Expression<Func<T>> parameterExpression) where T : class 
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(GetMemberName(parameterExpression));
			}
		}

		/// <summary>
		/// Validates that string is not null or empty.
		/// </summary>
		/// <param name="parameter">Parameter value.</param>
		/// <param name="parameterExpression">Expression to determine parameter name.</param>
		public static void IsNotNullOrEmpty(string parameter, Expression<Func<string>> parameterExpression)
		{
			if (string.IsNullOrEmpty(parameter))
			{
				throw new ArgumentNullException(GetMemberName(parameterExpression));
			}
		}

		/// <summary>
		/// Validates that array is not null or empty.
		/// </summary>
		/// <param name="parameter">Parameter value.</param>
		/// <param name="parameterExpression">Expression to determine parameter name.</param>
		public static void IsNotNullOrEmpty<T>(IEnumerable<T> parameter, Expression<Func<IEnumerable<T>>> parameterExpression)
		{
			if (parameter == null || !parameter.Any())
			{
				throw new ArgumentNullException(GetMemberName(parameterExpression));
			}
		}

		/// <summary>
		/// Validates that string is not null, not empty and not whitespace.
		/// </summary>
		/// <param name="parameter">Parameter value.</param>
		/// <param name="parameterExpression">Expression to determine parameter name.</param>
		public static void IsNotNullOrWhiteSpace(string parameter, Expression<Func<string>> parameterExpression)
		{
			if (string.IsNullOrWhiteSpace(parameter))
			{
				throw new ArgumentNullException(GetMemberName(parameterExpression));
			}
		}

		/// <summary>
		/// Validates that Guid is not empty, i.e does not contains zeros only.
		/// </summary>
		/// <param name="parameter">Parameter value.</param>
		/// <param name="parameterExpression">Expression to determine parameter name.</param>
		public static void IsNotEmpty(Guid parameter, Expression<Func<Guid>> parameterExpression)
		{
			if (parameter == Guid.Empty)
			{
				throw new ArgumentOutOfRangeException(GetMemberName(parameterExpression));
			}
		}

		private static string GetMemberName(LambdaExpression expression)
		{
			var body = (MemberExpression)expression.Body;
			return body.Member.Name;
		}
		// ReSharper restore UnusedParameter.Global
	}
}
