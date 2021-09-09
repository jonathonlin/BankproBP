using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Core
{
	public class PaginationResponse<T>
	{
		public PaginationResponse()
		{

		}
		public PaginationResponse(IEnumerable<T> data)
		{
			Data = data;
		}

		public IEnumerable<T> Data { get; set; }
		public int? PageNumber { get; set; }
		public int? PageSize { get; set; }
		public int? TotalNumber { get; set; }
	}
}
