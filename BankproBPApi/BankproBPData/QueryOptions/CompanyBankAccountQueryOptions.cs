using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.QueryOptions
{
	public class CompanyBankAccountQueryOptions: QueryOption
	{
		public string BankCode { get; set; }		
		public int? CompanyId { get; set; }
	}
}
