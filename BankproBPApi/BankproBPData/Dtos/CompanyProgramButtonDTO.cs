using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Dtos
{
	public class CompanyProgramButtonDTO
	{
		public string ButtonText { get; set; }
		public string ButtonAction { get; set; }
		public int ProgramId { get; set; }
		public int Status { get; set; }
		public int Sort { get; set; }
	}
}
