using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BankproBPApi.Helpers
{
	public static class SortHelper
	{
		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string columnName, bool isAscending = true)
		{
			if (string.IsNullOrEmpty(columnName))
			{
				return source;
			}

			ParameterExpression parameter = Expression.Parameter(source.ElementType, "");
			MemberExpression property = Expression.Property(parameter, columnName);
			LambdaExpression lambda = Expression.Lambda(property, parameter);
			string methodName = isAscending ? "OrderBy" : "OrderByDescending";
			Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
				new Type[] { source.ElementType, property.Type },
				source.Expression, Expression.Quote(lambda));
			return source.Provider.CreateQuery<T>(methodCallExpression);
		}

		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, QueryOption queryOption, bool isCustom = false)
		{
			if (string.IsNullOrEmpty(queryOption.SortField) || isCustom)
			{
				return source;
			}			
			ParameterExpression parameter = Expression.Parameter(source.ElementType, "");
			MemberExpression property = Expression.Property(parameter, queryOption.SortField);
			LambdaExpression lambda = Expression.Lambda(property, parameter);
			string methodName = (queryOption.SortDirection == "asc") ? "OrderBy" : "OrderByDescending";
			Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
				new Type[] { source.ElementType, property.Type },
				source.Expression, Expression.Quote(lambda));
			return source.Provider.CreateQuery<T>(methodCallExpression);
		}		
	}
}
