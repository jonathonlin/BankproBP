using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.QueryOptions
{
	public class BankQueryOptions: QueryOption
	{
		public string BankCode { get; set; }
		public string BankName { get; set; }
	}
}
