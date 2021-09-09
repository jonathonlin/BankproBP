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
	public class CompanyProgramController : ControllerBase
	{
		private readonly CompanyProgramManager _companyProgramManager;
		private IMapper _mapper;

		public CompanyProgramController(CompanyProgramManager companyProgramManager, IMapper mapper)
		{
			_companyProgramManager = companyProgramManager;
			_mapper = mapper;
		}

		// GET: api/<CompanyProgramController>
		[HttpGet]		
		public async Task<ActionResult<PaginationResponse<CompanyProgramReadDTO>>> GetAll([FromQuery] CompanyProgramQueryOptions options)
		{
				
			var query = await _companyProgramManager.GetCompanyProgramsAsync(options);
				

			var data = _mapper.Map<IEnumerable<CompanyProgram>, IEnumerable<CompanyProgramReadDTO>>(query);

			var paginationResponse = PaginationHelper.PaginationData(data, options);
			return Ok(paginationResponse); 
		}
				
		// GET api/<CompanyProgramController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<CompanyProgramDTO>> Get(int id)
		{
			var result = await _companyProgramManager.GetCompanyProgramAsyn(id);
			if (result == null)
				return NotFound();

			return Ok(_mapper.Map<CompanyProgramDTO>(result));
		}

		// POST api/<CompanyProgramController>
		[HttpPost]
		public async Task<ActionResult<Response<CompanyProgramDTO>>> Post([FromBody] CompanyProgramDTO value)
		{
			var data = _mapper.Map<CompanyProgram>(value);
			var result = await _companyProgramManager.Create(data);
			
			return Ok(new Response<CompanyProgramDTO>
			{
				IsOk = true,
				StatusCode = StatusCodes.Status200OK,
				Data = _mapper.Map<CompanyProgramDTO>(result)
			});
			
		}

		// PUT api/<CompanyProgramController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<Response<CompanyProgramDTO>>>  Put(int id, [FromBody] CompanyProgramDTO value)
		{
			var data = await _companyProgramManager.GetCompanyProgramAsyn(id);
			if (data == null) return NotFound();
			_mapper.Map(value, data);			
			await _companyProgramManager.Update(data, id);

			return Ok(new Response<CompanyProgramDTO> { IsOk = true, StatusCode = StatusCodes.Status200OK, Data = _mapper.Map<CompanyProgramDTO>(data) });
		}

		// DELETE api/<CompanyProgramController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Response<int>>> Delete(int id)
		{
			var data = await _companyProgramManager.GetCompanyProgramAsyn(id);
			if (data == null) return NotFound();
			var rows = await _companyProgramManager.DeleteAsyn(data);
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
