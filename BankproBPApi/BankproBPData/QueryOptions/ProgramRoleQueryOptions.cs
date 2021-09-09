using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.QueryOptions
{
	public class ProgramRoleQueryOptions: QueryOption
	{
		public string RoleName { get; set; }
		public int? Status { get; set; }
	}
}
