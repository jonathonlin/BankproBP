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
	public class AccountPayableDetail: IAuditable
	{
		public int Id { get; set; }
		[Column(TypeName = "varchar(20)")]
		public string ApNo { get; set; }
		[Column(TypeName = "nvarchar(200)")]
		public string ProductName { get; set; }
		[Column(TypeName = "decimal")]
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }
		[Column(TypeName = "decimal")]
		public decimal TotalAmount { get; set; }
		[Column(TypeName = "nvarchar(200)")]
		public string Note { get; set; }
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
		public int AccountPayableId { get; set; }
		public AccountPayable AccountPayable { get; set; }
	}
}
