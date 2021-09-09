using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Dtos
{
	public class AccountPayableReadDTO
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public string YearMonth { get; set; }
		public string ApNo { get; set; }
		public string InvoiceNo { get; set; }
		public DateTime InvoiceDate { get; set; }
		public decimal InvoiceAmount { get; set; }
		public decimal ApAmount { get; set; }
		public DateTime ExpireDate { get; set; }
		public int ApStatus { get; set; }
		public string Note { get; set; }
	}
}
