using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.QueryOptions
{
	public class ProgramUserQueryOptions: QueryOption
	{
		public string UserName { get; set; }
		public string CompanyName { get; set; }
	}
}
