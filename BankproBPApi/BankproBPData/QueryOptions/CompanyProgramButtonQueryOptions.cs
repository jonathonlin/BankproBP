using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.QueryOptions
{
	public class CompanyProgramButtonQueryOptions : QueryOption
	{
		public string ButtonText { get; set; }
		public int? ProgramId { get; set; }
		public int? Status { get; set; }
	}
}
