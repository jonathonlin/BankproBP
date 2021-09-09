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
	public class ProgramRoleController : ControllerBase
	{
		private readonly ProgramRoleManager _programRoleManager;
		private IMapper _mapper;

		public ProgramRoleController(ProgramRoleManager programRoleManager, IMapper mapper)
		{
			_programRoleManager = programRoleManager;
			_mapper = mapper;
		}

		// GET: api/<ProgramRoleController>
		[HttpGet]
		public async Task<ActionResult<PaginationResponse<ProgramRoleReadDTO>>> GetAll([FromQuery]ProgramRoleQueryOptions options)
		{
			var query = await _programRoleManager.GetProgramRolesAsyn(options);
			
			var data = _mapper.Map<IEnumerable<ProgramRole>, IEnumerable<ProgramRoleReadDTO>>(query);

			var paginationResponse = PaginationHelper.PaginationData(data, options);
			return Ok(paginationResponse);
		}

		// GET api/<ProgramRoleController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ProgramRoleDTO>> Get(int id)
		{
			var result = await _programRoleManager.GetProgramRoleAsyn(id);
			if (result == null) return NotFound();
			return Ok(_mapper.Map<ProgramRole, ProgramRoleDTO>(result));
		}

		// POST api/<ProgramRoleController>
		[HttpPost]
		public async Task<ActionResult<Response<ProgramRoleDTO>>> Post([FromBody] ProgramRoleDTO value)
		{
			var data = _mapper.Map<ProgramRole>(value);
			var result = await _programRoleManager.Create(data);

			return Ok(new Response<ProgramRoleDTO>
			{
				IsOk = true,
				StatusCode = StatusCodes.Status200OK,
				Data = _mapper.Map<ProgramRoleDTO>(result)
			});
		}

		// PUT api/<ProgramRoleController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<Response<ProgramRoleDTO>>> Put(int id, [FromBody] ProgramRoleDTO value)
		{
			var data = await _programRoleManager.GetProgramRoleAsyn(id);
			if (data == null) return NotFound();
			_mapper.Map(value, data);
			await _programRoleManager.Update(data, id);

			return Ok(new Response<ProgramRoleDTO> { IsOk = true, StatusCode = StatusCodes.Status200OK, Data = _mapper.Map<ProgramRoleDTO>(data) });
		}

		// DELETE api/<ProgramRoleController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Response<int>>> Delete(int id)
		{
			var data = await _programRoleManager.GetProgramRoleAsyn(id);
			if (data == null) return NotFound();
			var rows = await _programRoleManager.DeleteAsyn(data);
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
