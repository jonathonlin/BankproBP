using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.QueryOptions
{
	public class CompanyProgramQueryOptions : QueryOption
	{
		public string ProgramName { get; set; }
		public int? ProgramType { get; set; }
		public int? Status { get; set; }
	}
}
