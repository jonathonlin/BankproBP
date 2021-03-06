using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.QueryOptions
{
	public class CustomerQueryOptions: QueryOption
	{
		public string CustomerName { get; set; }
		public string Tel { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public string ZipCode { get; set; }
		public int? CompanyId { get; set; }
	}
}
