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
	public class CompaniesController : ControllerBase
	{
		private readonly CompanyManager _manager;
		private IMapper _mapper;

		public CompaniesController(CompanyManager companyManager, IMapper mapper)
		{
			_manager = companyManager;
			_mapper = mapper;
		}

		// GET: api/<CompaniesController>
		[HttpGet]
		public async Task<ActionResult<PaginationResponse<CompanyReadDTO>>> GetAll([FromQuery] CompanyQueryOptions options)
		{
			var query = await _manager.GetCompaniesAsyn(options);
			
			var data = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyReadDTO>>(query);

			var paginationResponse = PaginationHelper.PaginationData(data, options);

			return Ok(paginationResponse);
		}

		// GET api/<CompaniesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<CompanyDTO>> Get(int id)
		{
			var result = await _manager.GetCompanyAsyn(id);
			if (result == null) return NotFound();
			return Ok(_mapper.Map<Company, CompanyDTO>(result));
		}

		// POST api/<CompaniesController>
		[HttpPost]
		public async Task<ActionResult<Response<CompanyDTO>>> Post([FromBody] CompanyDTO value)
		{
			var data = _mapper.Map<Company>(value);
			var result = await _manager.Create(data);

			return Ok(new Response<CompanyDTO>
			{
				IsOk = true,
				StatusCode = StatusCodes.Status200OK,
				Data = _mapper.Map<CompanyDTO>(result)
			});
		}

		// PUT api/<CompaniesController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<Response<CompanyDTO>>> Put(int id, [FromBody] CompanyDTO value)
		{
			var data = await _manager.GetCompanyAsyn(id);
			if (data == null) return NotFound();
			_mapper.Map(value, data);
			await _manager.Update(data, id);			

			return Ok(new Response<CompanyDTO> { IsOk = true, StatusCode = StatusCodes.Status200OK, Data = _mapper.Map<CompanyDTO>(data) });
		}

		// DELETE api/<CompaniesController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Response<int>>> Delete(int id)
		{
			var data = await _manager.GetCompanyAsyn(id);
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
