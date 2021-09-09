using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Dtos
{
	public class CompanyProgramDTO
	{
		public string ProgramName { get; set; }
		public int? ParentId { get; set; }
		public int ProgramType { get; set; }
		public string ProgramUrl { get; set; }
		public int Status { get; set; }
		public int Sort { get; set; }
	}
}
