using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Dtos
{
	public class ProgramUserDTO
	{		
		public string Account { get; set; }		
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public int AccountType { get; set; }
		public int CompanyId { get; set; }
		public int? CustomerId { get; set; }

	}
}
