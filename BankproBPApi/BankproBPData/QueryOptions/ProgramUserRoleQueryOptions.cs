using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.QueryOptions
{
	public class ProgramUserRoleQueryOptions: QueryOption
	{
		public int? UserId { get; set; }
		public int? RoleId { get; set; }
	}
}
