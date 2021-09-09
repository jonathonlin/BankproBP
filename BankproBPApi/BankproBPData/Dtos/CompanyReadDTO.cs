using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Dtos
{
	public class CompanyReadDTO
	{
		public int Id { get; set; }

		public string CompanyName { get; set; }

		public string Tel { get; set; }

		public string Email { get; set; }

		public string ZipCode { get; set; }

		public string Address { get; set; }
	}
}
