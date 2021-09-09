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
	public class CompanyProgramButton : IAuditable
	{
		public int Id { get; set; }
		[Column(TypeName = "nvarchar(50)")]
		public string ButtonText { get; set; }
		[Column(TypeName = "varchar(100)")]
		public string ButtonAction { get; set; }
		public int ProgramId { get; set; }
		public int Status { get; set; }
		public int Sort { get; set; }
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
		public CompanyProgram CompanyProgram { get; set; }
	}
}
