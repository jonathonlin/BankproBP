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
	public class CustomerController : ControllerBase
	{
		private readonly CustomerManager _manager;
		private IMapper _mapper;

		public CustomerController(CustomerManager customerManager, IMapper mapper)
		{
			_manager = customerManager;
			_mapper = mapper;
		}

		// GET: api/<CustomerController>
		[HttpGet]
		public async Task<ActionResult<PaginationResponse<CustomerReadDTO>>> GetAll([FromQuery] CustomerQueryOptions options)
		{
			var query = await _manager.GetAllAsyn(options);

			var data = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerReadDTO>>(query);

			var paginationResponse = PaginationHelper.PaginationData(data, options);

			return Ok(paginationResponse);
		}

		// GET api/<CustomerController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerDTO>> Get(int id)
		{
			var result = await _manager.GetAsync(id);
			if (result == null) return NotFound();
			return Ok(_mapper.Map<Customer, CustomerDTO>(result));
		}

		// POST api/<CustomerController>
		[HttpPost]
		public async Task<ActionResult<Response<CustomerDTO>>> Post([FromBody] CustomerDTO value)
		{
			var data = _mapper.Map<Customer>(value);
			var result = await _manager.Create(data);

			return Ok(new Response<CustomerDTO>
			{
				IsOk = true,
				StatusCode = StatusCodes.Status200OK,
				Data = _mapper.Map<CustomerDTO>(result)
			});
		}

		// PUT api/<CustomerController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<Response<CustomerDTO>>> Put(int id, [FromBody] CustomerDTO value)
		{
			var data = await _manager.GetAsync(id);
			if (data == null) return NotFound();
			_mapper.Map(value, data);
			await _manager.Update(data, id);

			return Ok(new Response<CustomerDTO> { IsOk = true, StatusCode = StatusCodes.Status200OK, Data = _mapper.Map<CustomerDTO>(data) });
		}

		// DELETE api/<CustomerController>/5
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
