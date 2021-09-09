using BankproBPData;
using BankproBPDomain.Managers;
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
	public class PGParameterController : ControllerBase
	{

		private readonly PGParameterManager _manager;

		public PGParameterController(PGParameterManager manager)
		{
			_manager = manager;
		}

		// GET: api/<PGParameterController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PGParameter>>> GetAll([FromQuery] string keyCode)
		{
			var query = await _manager.GetAllAsyn(keyCode);
			return Ok(query);
		}		
	}
}
