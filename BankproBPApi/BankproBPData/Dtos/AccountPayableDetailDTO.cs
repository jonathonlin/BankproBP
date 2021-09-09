using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Dtos
{
	public class AccountPayableDetailDTO
	{
		public string ApNo { get; set; }
		public string ProductName { get; set; }
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }
		public decimal TotalAmount { get; set; }
		public string Note { get; set; }
	}
}
