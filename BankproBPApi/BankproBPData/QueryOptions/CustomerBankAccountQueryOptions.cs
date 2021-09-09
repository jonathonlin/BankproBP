using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.QueryOptions
{
	public class CustomerBankAccountQueryOptions:QueryOption
	{
		public int? CustomerId { get; set; }
		public string BankCode { get; set; }		
	}
}
