using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData
{
	public class Permission: IAuditable
	{
		public int Id { get; set; }
		public int RoleId { get; set; }
		public int ProgramId { get; set; }
		public int? ButtonId { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		public string CreateId { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime CreateDate { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		public string UpdateId { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime? UpdateDate { get; set; }
	}
}
