using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankproBPData
{	
	public class CompanyProgram: IAuditable
	{		
		public int Id { get; set; }
		[Column(TypeName = "nvarchar(50)")]
		public string ProgramName { get; set; }
		public int? ParentId { get; set; }
		public int ProgramType { get; set; }

		[Column(TypeName = "nvarchar(100)")]
		public string ProgramUrl { get; set; }	
		public int Status { get; set; }
		public int Sort { get; set; }
		[Column(TypeName = "nvarchar(450)")]		
		public string CreateId { get; set; }
		[Column(TypeName = "datetime")]
		[Required]
		public DateTime CreateDate { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		public string UpdateId { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime? UpdateDate { get; set; }
		public ICollection<CompanyProgramButton> CompanyProgramButtons { get; set; }
	}
}
