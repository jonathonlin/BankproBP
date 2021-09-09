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
	public class CompanyProgramButtonController : ControllerBase
	{
		private readonly CompanyProgramButtonManager _manager;
		private IMapper _mapper;

		public CompanyProgramButtonController(
			CompanyProgramButtonManager companyProgramButtonManager, IMapper mapper)
		{
			_manager = companyProgramButtonManager;
			_mapper = mapper;
		}

		// GET: api/<CompanyProgramButtonController>
		[HttpGet]
		public async Task<ActionResult<PaginationResponse<CompanyProgramButtonReadDTO>>> GetAll([FromQuery] CompanyProgramButtonQueryOptions options)
		{
			var query = await _manager.GetAllAsync(options);			
			var data = _mapper.Map<IEnumerable<CompanyProgramButton>, IEnumerable<CompanyProgramButtonReadDTO>>(query);

			var paginationResponse = PaginationHelper.PaginationData(data, options);
			return Ok(paginationResponse);

		}

		// GET api/<CompanyProgramButtonController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<CompanyProgramButtonDTO>> Get(int id)
		{
			var result = await _manager.GetAsync(id);
			if (result == null) return NotFound();
			return Ok(_mapper.Map<CompanyProgramButton, CompanyProgramButtonDTO>(result));
		}

		// POST api/<CompanyProgramButtonController>
		[HttpPost]
		public async Task<ActionResult<Response<CompanyProgramButtonDTO>>> Post([FromBody] CompanyProgramButtonDTO value)
		{
			var data = _mapper.Map<CompanyProgramButton>(value);
			var result = await _manager.Create(data);

			return Ok(new Response<CompanyProgramButtonDTO>
			{
				IsOk = true,
				StatusCode = StatusCodes.Status200OK,
				Data = _mapper.Map<CompanyProgramButtonDTO>(result)
			});
		}

		// PUT api/<CompanyProgramButtonController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<Response<CompanyProgramButtonDTO>>> Put(int id, [FromBody] CompanyProgramButtonDTO value)
		{
			var data = await _manager.GetAsync(id);
			if (data == null) return NotFound();
			_mapper.Map(value, data);
			await _manager.Update(data, id);

			return Ok(new Response<CompanyProgramButtonDTO> { IsOk = true, StatusCode = StatusCodes.Status200OK, Data = _mapper.Map<CompanyProgramButtonDTO>(data) });
		}

		// DELETE api/<CompanyProgramButtonController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Response<int>>> Delete(int id)
		{
			var data = await _manager.GetAsync(id);
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
