using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData
{
	public class FormNoCount
	{
		public int Id { get; set; }
		public string FormType { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public int Day { get; set; }
		public int SerialNo { get; set; }
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
	}
}
