using AutoMapper;
using BankproBPApi.Helpers;
using BankproBPData;
using BankproBPData.Core;
using BankproBPData.Dtos;
using BankproBPData.QueryOptions;
using BankproBPDomain.Authentication;
using BankproBPDomain.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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
	public class ProgramUserController : ControllerBase
	{
		private readonly ProgramUserManager _programUserManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private IMapper _mapper;

		public ProgramUserController(ProgramUserManager programUserManager, UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_programUserManager = programUserManager;
			_userManager = userManager;
			_mapper = mapper;
		}
		
		[HttpGet]		
		public async Task<ActionResult<PaginationResponse<ProgramUserReadDTO>>> GetProgramUsers([FromQuery]ProgramUserQueryOptions options)
		{			
			var query = await _programUserManager.GetProgramUsersAsync(options);			
						

			switch (options.SortField)
			{
				case "companyName":
					if (options.SortDirection == null) break;
					if (options.SortDirection.Equals("desc"))
					{
						query = query.OrderByDescending(o => o.Company.CompanyName);
					}
					else
					{
						query = query.OrderBy(o => o.Company.CompanyName);
					}
					break;
				default:
					query = SortHelper.OrderBy(query, options.SortField, options.SortDirection == "asc");
					break;
			}
			
			var programUsers = _mapper.Map<IEnumerable<ProgramUser>, IEnumerable<ProgramUserReadDTO>>(query);

			var paginationResponse = PaginationHelper.PaginationData(programUsers, options, true);
			
			return Ok(paginationResponse);
		}


		// GET api/<ProgramUserController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ProgramUserDTO>> Get(int id)
		{
			var result = await _programUserManager.GetProgramUserAsync(id);
			if (result == null) return NotFound();
			
			return Ok(_mapper.Map<ProgramUser, ProgramUserDTO>(result));		
		}
		
		// POST api/<ProgramUserController>
		[HttpPost]
		public async Task<ActionResult<Response<ProgramUserDTO>>> Post([FromBody] ProgramUserDTO value)
		{
			var userExists = await _userManager.FindByNameAsync(value.Account);
			if (userExists != null)
			{
				return Ok(new Response<object> { 
					IsOk = false,
					StatusCode = StatusCodes.Status500InternalServerError,
					Message = "使用者已存在"
				});				
			}
			var user = new ApplicationUser()
			{
				Email = value.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = value.Account
			};
			var result = await _userManager.CreateAsync(user, value.Password);

			if (!result.Succeeded)
			{				
				return Ok(new Response<object> { 
					IsOk = false, 
					StatusCode = StatusCodes.Status500InternalServerError, 
					Message = string.Join("，", result.Errors.Select(s=>s.Description).ToArray()) 
				});
			}
						
			var data = _mapper.Map<ProgramUser>(value);
			data.AspNetUserId = user.Id;
			data.Status = 1;

			var model = await _programUserManager.Create(data);
						
			return Ok(new Response<ProgramUserDTO> { 
				IsOk = true,
				StatusCode = StatusCodes.Status200OK,
				Data = 	_mapper.Map<ProgramUserDTO>(model) 
			});	
		}

		// PUT api/<ProgramUserController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<Response<ProgramUserDTO>>> Put(int id, [FromBody] ProgramUserDTO value)
		{
			var data = await _programUserManager.GetProgramUserAsync(id);
			if (data == null) return NotFound();

			var user = await _userManager.FindByNameAsync(data.Account);
			if (user == null) return NotFound();

			if (!string.IsNullOrEmpty(value.Password))
			{
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var result = await _userManager.ResetPasswordAsync(user, token, value.Password);
				if (!result.Succeeded)
				{
					return Ok(new Response<object>
					{
						IsOk = false,
						StatusCode = StatusCodes.Status500InternalServerError,
						Message = string.Join("，", result.Errors.Select(s => s.Description).ToArray())
					});					
				}
			}
			if (!string.IsNullOrEmpty(value.Email) && value.Email != user.Email)
			{
				var result = await _userManager.SetEmailAsync(user, value.Email);
				if (!result.Succeeded)
				{
					return Ok(new Response<object>
					{
						IsOk = false,
						StatusCode = StatusCodes.Status500InternalServerError,
						Message = "資料更新失敗。"
					});
				}
			}
						
			_mapper.Map(value, data);
			
			await _programUserManager.Update(data, id);
			return Ok(new Response<ProgramUserDTO> { IsOk = true, StatusCode = StatusCodes.Status200OK, Data = _mapper.Map<ProgramUserDTO>(data) });
		}

		// DELETE api/<ProgramUserController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Response<int>>> Delete(int id)
		{
			var data = await _programUserManager.GetProgramUserAsync(id);
			if (data == null) return NotFound();
			var user = await _userManager.FindByIdAsync(data.AspNetUserId);
			if (user != null)
			{
				var result = await _userManager.DeleteAsync(user);
				if (!result.Succeeded)
					return Ok(new Response<object>
					{
						IsOk = false,
						StatusCode = StatusCodes.Status500InternalServerError,
						Message = "刪除失敗，請稍後再試。"
					});
			}

			var res = await _programUserManager.Delete(data);
			return Ok(new Response<int> { IsOk = true, Data = res, StatusCode = StatusCodes.Status200OK });
		}

		[HttpPost]
		[Route("ResetPassword")]
		public async Task<ActionResult<Response<object>>> ResetPassword([FromBody] string password)
		{
			
			if (string.IsNullOrWhiteSpace(password))
			{
				return Ok(new Response<object>
				{
					IsOk = false,
					StatusCode = StatusCodes.Status500InternalServerError,
					Message = "密碼不得為空值。"
				});
			}
			
			var name = User.Identity.Name;
			var user = await _userManager.FindByNameAsync(name);
			if (user == null)
			{
				return Ok(new Response<object>
				{
					IsOk = false,
					StatusCode = StatusCodes.Status500InternalServerError,
					Message = "查無使用者。"
				});
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, token, password);
			if (!result.Succeeded)
			{
				return Ok(new Response<object>
				{
					IsOk = false,
					StatusCode = StatusCodes.Status500InternalServerError,
					Message = string.Join("，", result.Errors.Select(s => s.Description).ToArray())
				});
			}

			return Ok(new Response<object>
			{
				IsOk = true,
				StatusCode = StatusCodes.Status200OK
			});
		}
	}
}
