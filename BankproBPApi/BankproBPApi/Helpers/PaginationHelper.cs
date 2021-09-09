using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankproBPApi.Helpers
{
	public class PaginationHelper
	{
		public static PaginationResponse<T> PaginationData<T>(IEnumerable<T> data, QueryOption queryOption, bool isCustomSorting = false)
		{
			var totalNumber = data.Count();
			data = SortHelper.OrderBy(data.AsQueryable(), queryOption, isCustomSorting);
			if (!queryOption.Page.Equals(0))
			{
				data = data.Skip((queryOption.Page - 1) * queryOption.PageSize).Take(queryOption.PageSize);
			}
			var paginationResponse = new PaginationResponse<T>
			{
				Data = data,
				PageNumber = queryOption.Page,
				PageSize = queryOption.PageSize,
				TotalNumber = totalNumber
			};

			return paginationResponse;
		}
	}
}
