using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SpTools.Validation
{
	/// <summary>
	/// Utility that simplify parameters validation
	/// </summary>
	public static class ParametersValidator
	{
		/// <summary>
		/// Validates that parameter is not null.
		/// Usage: Validator.IsNotNull(param, () => param).
		/// </summary>
		/// <typeparam name="T">Parameter type.</typeparam>
		/// <param name="parameter">Parameter value.</param>
		/// <param name="parameterExpression">Expression to determine parameter name.</param>
		public static void IsNotNull<T>(T parameter, Expression<Func<T>> parameterExpression) where T : class 
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(GetMemberName(parameterExpression));
			}
		}

        /// <summary>
        /// Validates that parameter is not null. Works slower that a version with 2 parameters.
        /// Usage: Validator.IsNotNull(() => param).
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="parameterExpression">Expression to determine parameter name.</param>
        public static void IsNotNull<T>(Expression<Func<T>> parameterExpression) where T : class
        {
            var value = GetMemberValue(parameterExpression);
            if (value == null)
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
        /// Validates that string is not null or empty. Works slower that a version with 2 parameters.
        /// </summary>
        /// <param name="parameterExpression">Expression to determine parameter name.</param>
        public static void IsNotNullOrEmpty(Expression<Func<string>> parameterExpression)
        {
            var value = GetMemberValue(parameterExpression);
            if (String.IsNullOrEmpty(value))
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
        /// Validates that array is not null or empty. Works slower that a version with 2 parameters.
        /// </summary>
        /// <param name="parameterExpression">Expression to determine parameter name.</param>
        public static void IsNotNullOrEmpty<T>(Expression<Func<IEnumerable<T>>> parameterExpression)
        {
            var value = GetMemberValue(parameterExpression);
            if (value == null || !value.Any())
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
			if (String.IsNullOrWhiteSpace(parameter))
			{
				throw new ArgumentNullException(GetMemberName(parameterExpression));
			}
		}

        /// <summary>
        /// Validates that string is not null, not empty and not whitespace. Works slower that a version with 2 parameters.
        /// </summary>
        /// <param name="parameterExpression">Expression to determine parameter name.</param>
        public static void IsNotNullOrWhiteSpace(Expression<Func<string>> parameterExpression)
        {
            var value = GetMemberValue(parameterExpression);
            if (String.IsNullOrWhiteSpace(value))
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

        /// <summary>
        /// Validates that Guid is not empty, i.e does not contains zeros only. Works slower that a version with 2 parameters.
        /// </summary>
        /// <param name="parameterExpression">Expression to determine parameter name.</param>
        public static void IsNotEmpty(Expression<Func<Guid>> parameterExpression)
        {
            var value = GetMemberValue(parameterExpression);
            if (value == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(GetMemberName(parameterExpression));
            }
        }

        private static string GetMemberName(LambdaExpression expression)
		{
			var body = (MemberExpression)expression.Body;
			return body.Member.Name;
		}

        private static T GetMemberValue<T>(Expression<Func<T>> expression) 
        {
            var memberSelector = expression.Body as MemberExpression;
            if (memberSelector != null)
            {
                var constantSelector = (ConstantExpression) memberSelector.Expression;
                var value = ((FieldInfo)memberSelector.Member).GetValue(constantSelector.Value);
                return (T)value;
            }
            else
            {
                var value = ((ConstantExpression)expression.Body).Value;
                return (T)value;
            }
        }
    }
}
