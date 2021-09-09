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
	public class Customer: IAuditable
	{
		public int Id { get; set; }
		[Column(TypeName = "nvarchar(100)")]
		public string CustomerName { get; set; }
		[Column(TypeName = "varchar(30)")]
		public string Tel { get; set; }
		[Column(TypeName = "varchar(30)")]
		public string Mobile { get; set; }
		[Column(TypeName = "varchar(100)")]
		public string Email { get; set; }
		[Column(TypeName = "varchar(6)")]
		public string ZipCode { get; set; }
		[Column(TypeName = "nvarchar(200)")]
		public string Address { get; set; }
		public int CompanyId { get; set; }
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
		public Company Company { get; set; }
		public List<CustomerBankAccount> CustomerBankAccounts { get; set; }
	}
}
