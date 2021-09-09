using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Dtos
{
	public class CompanyBankAccountDTO
	{
		public string BankCode { get; set; }
		public string BankAccount { get; set; }
		public string BankAccountName { get; set; }
		public string CompanyBankAtmId { get; set; }
		public int CompanyId { get; set; }
	}
}
