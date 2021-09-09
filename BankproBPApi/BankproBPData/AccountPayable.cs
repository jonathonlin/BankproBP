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
	public class AccountPayable: IAuditable
	{
		public int Id { get; set; }
		[Required]
		public int CustomerId { get; set; }
		[Column(TypeName = "varchar(6)")]
		public string YearMonth { get; set; }
		[Column(TypeName = "varchar(20)")]
		public string ApNo { get; set; }
		[Column(TypeName = "varchar(10)")]
		public string InvoiceNo { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime InvoiceDate { get; set; }
		[Column(TypeName = "decimal")]
		public decimal InvoiceAmount { get; set; }
		[Column(TypeName = "decimal")]
		public decimal ApAmount { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime ExpireDate { get; set; }
		[Required]
		public int ApStatus { get; set; }
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
		public List<AccountPayableDetail> AccountPayableDetails { get; set; }
	}
}
