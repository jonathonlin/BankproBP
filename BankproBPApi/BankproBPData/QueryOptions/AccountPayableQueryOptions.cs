using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.QueryOptions
{
	public class AccountPayableQueryOptions: QueryOption
	{
		public int? CustomerId { get; set; }
		public string YearMonth { get; set; }
		public string ApNo { get; set; }
		public string InvoiceNo { get; set; }
		public int? ApStatus { get; set; }
	}
}
