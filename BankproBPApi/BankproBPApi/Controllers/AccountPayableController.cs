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
	public class AccountPayableController : ControllerBase
	{
		private readonly AccountPayableManager _manager;
		private readonly FormNoCountManager _formNoCountManager;
		private IMapper _mapper;
		public AccountPayableController(
			FormNoCountManager formNoCountManager,
			AccountPayableManager accountPayableManager, 
			IMapper mapper)
		{
			_manager = accountPayableManager;
			_formNoCountManager = formNoCountManager;
			_mapper = mapper;
		}
		// GET: api/<AccountPayableController>
		[HttpGet]
		public async Task<ActionResult<PaginationResponse<AccountPayableReadDTO>>> GetAll([FromQuery] AccountPayableQueryOptions options)
		{
			var query = await _manager.GetAllAsyn(options);

			var data = _mapper.Map<IEnumerable<AccountPayable>, IEnumerable<AccountPayableReadDTO>>(query);

			var paginationResponse = PaginationHelper.PaginationData(data, options);

			return Ok(paginationResponse);
		}

		// GET api/<AccountPayableController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<AccountPayableDTO>> Get(int id)
		{
			var result = await _manager.GetAsync(id);
			if (result == null) return NotFound();
			return Ok(_mapper.Map<AccountPayable, AccountPayableDTO>(result));
		}

		// POST api/<AccountPayableController>
		[HttpPost]
		public async Task<ActionResult<Response<AccountPayableDTO>>> Post([FromBody] AccountPayableDTO value)
		{
			var now = DateTime.Now;
			var apNo = await _formNoCountManager.GetFormNo("AP", now.Year, now.Month, now.Day);
			value.ApNo = apNo;
			value.AccountPayableDetails.ForEach(e => e.ApNo = apNo);
			var data = _mapper.Map<AccountPayable>(value);
			var result = await _manager.Create(data);
			
			return Ok(new Response<AccountPayableDTO>
			{
				IsOk = true,
				StatusCode = StatusCodes.Status200OK,
				Data = _mapper.Map<AccountPayableDTO>(result)
			});
		}

		// PUT api/<AccountPayableController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<Response<AccountPayableDTO>>> Put(int id, [FromBody] AccountPayableDTO value)
		{
			var data = await _manager.GetAsync(id);
			if (data == null) return NotFound();
			_mapper.Map(value, data);
			await _manager.Update(data, id);

			return Ok(new Response<AccountPayableDTO> 
			{ 
				IsOk = true, 
				StatusCode = StatusCodes.Status200OK, 
				Data = _mapper.Map<AccountPayableDTO>(data) 
			});
		}

		// DELETE api/<AccountPayableController>/5
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
