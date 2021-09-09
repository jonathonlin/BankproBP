using BankproBPData;
using BankproBPData.Core;
using BankproBPData.Dtos;
using BankproBPDomain.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankproBPApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class PermissionController : ControllerBase
	{
		private readonly PermissionManager _manager;

		public PermissionController(PermissionManager manager)
		{
			_manager = manager;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<List<Permission>>> Get(int id)
		{
			var result = await _manager.GetPermissions(id);
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<Response<object>>> Post([FromBody] List<Permission> value)
		{		
			var result = await _manager.SavePermission(value);
			
			return Ok(new Response<object> { IsOk = result, StatusCode = StatusCodes.Status200OK, Data = result, Message = result ? "": "設定失敗，請稍後再試。" });
		}

		public async Task<ActionResult<List<CompanyProgram>>> GetRolePermission()
		{
			var result = await _manager.GetRolePermission();

			return Ok(result);			
		}

		
	}
}
