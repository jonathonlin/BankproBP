using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPData.Core
{
	public class CurrentUser
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUser(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetUserId
		{
			get {
				var result = string.Empty;
				if (_httpContextAccessor.HttpContext.User.Identity != null)
				{
					var claims = _httpContextAccessor.HttpContext.User.Claims;
					foreach (var claim in claims)
					{
						if (claim.Type == ClaimTypes.Sid)
							result = claim.Value;
					}					
				}
				return result;
			}
		}

		public string GetUserName
		{
			get {
				var result = string.Empty;
				if(_httpContextAccessor.HttpContext.User.Identity != null)
				{
					var claims = _httpContextAccessor.HttpContext.User.Claims;
					foreach(var claim in claims)
					{
						if (claim.Type == ClaimTypes.Name)
						{
							result = claim.Value;
						}
					}
				}
				return result;
			}
		}
		
	}
}
