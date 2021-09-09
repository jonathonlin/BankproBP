using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData
{
	public class CustomerBankAccount: IAuditable
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		[Column(TypeName = "varchar(5)")]
		public string BankCode { get; set; }
		[Column(TypeName = "nvarchar(50)")]
		public string BankAccountName { get; set; }
		[Column(TypeName = "varchar(30)")]
		public string BankAccount { get; set; }
		public decimal AllowDiffAmount { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		[Required]
		public string CreateId { get; set; }
		[Column(TypeName = "datetime")]
		[Required]
		public DateTime CreateDate { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		public string UpdateId { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime? UpdateDate { get; set; }
		public Customer Customer { get; set; }
	}
}
