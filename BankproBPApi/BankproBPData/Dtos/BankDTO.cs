using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Dtos
{
	[AutoMap(typeof(Bank))]
	public class BankDTO
	{
		public string BankCode { get; set; }
		public string BankName { get; set; }
	}
}
