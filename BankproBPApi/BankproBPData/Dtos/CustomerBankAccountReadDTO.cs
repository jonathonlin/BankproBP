using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Dtos
{
	public class CustomerBankAccountReadDTO
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public string BankCode { get; set; }
		public string BankAccountName { get; set; }
		public string BankAccount { get; set; }
		public decimal AllowDiffAmount { get; set; }
	}
}
