using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Core
{
	public class Response<T>
	{
		public Response()
		{

		}

		public Response(T data)
		{
			Data = data;
		}
		public T Data { get; set; }
		public int StatusCode { get; set; }		
		public string Message { get; set; }
		public bool IsOk { get; set; }
	}
}
