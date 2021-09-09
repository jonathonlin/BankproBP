using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Core
{
	public class QueryOption
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public string SortField { get; set; }
		public string SortDirection { get; set; }
	}
}
