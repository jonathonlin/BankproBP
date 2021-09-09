using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData
{
	public class ProgramRole : IAuditable
	{
		public int Id { get; set; }
		[Column(TypeName = "nvarchar(20)")]
		public string RoleName { get; set; }
		public int Status { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		public string CreateId { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime CreateDate { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		public string UpdateId { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime? UpdateDate { get; set; }

		public List<ProgramUserRole> ProgramUserRoles { get; set; }
	}
}
