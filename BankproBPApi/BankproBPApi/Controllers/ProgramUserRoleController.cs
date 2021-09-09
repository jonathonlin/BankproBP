using AutoMapper;
using BankproBPApi.Helpers;
using BankproBPData;
using BankproBPData.Core;
using BankproBPData.Dtos;
using BankproBPData.QueryOptions;
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
	public class ProgramUserRoleController : ControllerBase
	{
		private readonly ProgramUserRoleManager _manager;
		private IMapper _mapper;
		public ProgramUserRoleController(ProgramUserRoleManager manager, IMapper mapper)
		{
			_manager = manager;
			_mapper = mapper;
		}
		//GET: api/<ProgramUserRoleController>
		[HttpGet]
		public async Task<ActionResult<PaginationResponse<ProgramUserRole>>> GetAll([FromQuery] ProgramUserRoleQueryOptions options)
		{
			var query = await _manager.GetProgramUserRolesAsyn(options);
			
			var paginationResponse = PaginationHelper.PaginationData(query, options);
			return Ok(paginationResponse);
		}

		// GET api/<ProgramUserRoleController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ProgramUserRoleDTO>> Get(int id)
		{
			var result = await _manager.GetProgramUserRoleAsyn(id);
			if (result == null) return NotFound();
			return Ok(_mapper.Map<ProgramUserRoleDTO>(result));
		}

		// POST api/<ProgramUserRoleController>
		[HttpPost]
		public async Task<ActionResult<Response<ProgramUserRole>>> Post([FromBody] ProgramUserRoleDTO value)
		{
			var hasExists = await  _manager.HasExists(value.UserId, value.RoleId);
			if (hasExists == true) {
				return Ok(new Response<ProgramUserRoleDTO>
				{
					IsOk = false,
					StatusCode = StatusCodes.Status500InternalServerError,
					Message = "資料已存在，請勿重複輸入。"
				});
			}
			var data = _mapper.Map<ProgramUserRole>(value);

			var result = await _manager.Create(data);

			return Ok(new Response<ProgramUserRoleDTO>
			{
				IsOk = true,
				StatusCode = StatusCodes.Status200OK,
				Data = _mapper.Map<ProgramUserRoleDTO>(result)
			});
		}

		// PUT api/<ProgramUserRoleController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<Response<ProgramUserRoleDTO>>> Put(int id, [FromBody] ProgramUserRoleDTO value)
		{
			var hasExists = await _manager.HasExists(value.UserId, value.RoleId);
			if (hasExists == true)
			{
				return Ok(new Response<ProgramUserRoleDTO>
				{
					IsOk = false,
					StatusCode = StatusCodes.Status200OK,
					Message = "資料已存在，請勿重複輸入。"
				});
			}

			var data = await _manager.GetProgramUserRoleAsyn(id);
			if (data == null) return NotFound();
			_mapper.Map(value, data);
			await _manager.Update(data, id);

			return Ok(new Response<ProgramUserRoleDTO> { IsOk = true, StatusCode = StatusCodes.Status200OK, Data = _mapper.Map<ProgramUserRoleDTO>(data) });
		}

		// DELETE api/<ProgramUserRoleController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Response<int>>> Delete(int id)
		{
			var data = await _manager.GetProgramUserRoleAsyn(id);
			if (data == null) return NotFound();
			var rows = await _manager.DeleteAsyn(data);
			var response = new Response<int>
			{
				IsOk = (rows > 0) ? true : false,
				Message = rows > 0 ? "" : "刪除失敗",
				Data = rows,
				StatusCode = StatusCodes.Status200OK
			};
			return Ok(response);
		}
	}
}
