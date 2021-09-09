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
	public class ProgramUser : IAuditable
	{
		public int Id { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		public string AspNetUserId { get; set; }
		[Column(TypeName = "nvarchar(30)")]		
		public string UserName { get; set; }
		[Column(TypeName = "nvarchar(256)")]
		public string Email { get; set; }
		[Column(TypeName = "nvarchar(256)")]
		public string Account { get; set; }
		public int AccountType { get; set; }
		public int CompanyId { get; set; }
		public int? CustomerId { get; set; }
		public  int Status { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		public string CreateId { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime CreateDate { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		public string UpdateId { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime? UpdateDate { get; set; }

		public Company Company { get; set; }		
		public List<ProgramUserRole> ProgramUserRoles { get; set; }
	}
}
