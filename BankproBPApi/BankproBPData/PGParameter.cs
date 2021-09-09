using BankproBPData.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData
{
	public class PGParameter : IAuditable
	{
		public int Id { get; set; }
		[Column(TypeName = "varchar(30)")]
		public string KeyCode { get; set; }
		[Column(TypeName = "nvarchar(30)")]
		public string KeyName { get; set; }
		public int KeyValue { get; set; }
		public int Sort { get; set; }
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
