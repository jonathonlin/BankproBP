using AutoMapper;
using BankproBPData.Core;
using BankproBPData.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData
{
	[AutoMap(typeof(BankDTO)), AutoMap(typeof(BankReadDTO))]
	public class Bank: IAuditable
	{
		public int Id { get; set; }
		[Column(TypeName = "varchar(6)")]
		public string BankCode { get; set; }
		[Column(TypeName = "nvarchar(20)")]
		public string BankName { get; set; }
		
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
