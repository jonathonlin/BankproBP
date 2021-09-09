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
	public class ProgramUserRole: IAuditable
	{
		public int Id { get; set; }
		public int UserId { get; set; }		
		public int RoleId { get; set; }
		public string CreateId { get; set; }
		public DateTime CreateDate { get; set; }
		public string UpdateId { get; set; }
		public DateTime? UpdateDate { get; set; }
		public ProgramUser ProgramUser { get; set; }
		public ProgramRole ProgramRole { get; set; }
	}
}
