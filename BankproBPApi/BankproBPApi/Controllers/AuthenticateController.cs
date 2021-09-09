using BankproBPData;
using BankproBPData.Core;
using BankproBPData.Dtos;
using BankproBPDomain.Authentication;
using BankproBPDomain.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticateController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly ProgramUserManager _programUserManager;
		private readonly IConfiguration _configuration;
		private EmailManager _emailManager;
		private readonly PermissionManager _permissionManager;

		public AuthenticateController(
		    UserManager<ApplicationUser> userManager,
		    RoleManager<IdentityRole> roleManager,
		    ProgramUserManager programUserManager,
		    EmailManager emailManager,
		    PermissionManager permissionManager,
		    IConfiguration configuration)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			_programUserManager = programUserManager;
			_configuration = configuration;
			_emailManager = emailManager;
			_permissionManager = permissionManager;
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var user = await userManager.FindByNameAsync(model.Account);
			
			if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
			{
				JwtSecurityToken token = CreateToken(user);

				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
					expiration = token.ValidTo
				});
			}
			return Unauthorized(new Response<object> { IsOk = false, StatusCode = 500, Message = "登入失敗，請檢查帳號密碼是否正確。" });
		}

		private JwtSecurityToken CreateToken(ApplicationUser user)
		{
			var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(ClaimTypes.Sid, user.Id),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
				};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(
			    issuer: _configuration["JWT:ValidIssuer"],
			    audience: _configuration["JWT:ValidAudience"],
			    expires: DateTime.Now.AddHours(3),
			    claims: authClaims,
			    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			    );
			return token;
		}

		[HttpPost]
		[Route("register")]
		[Authorize]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			var userExists = await userManager.FindByNameAsync(model.Account);
			if (userExists != null)
				return Ok(new Response<object> { IsOk = false, StatusCode = StatusCodes.Status500InternalServerError, Message = "使用者已存在！" });
			var user = new ApplicationUser()
			{
				Email = model.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = model.Account
			};
			var result = await userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
				return Ok(new Response<object> { IsOk = false, StatusCode = StatusCodes.Status500InternalServerError, Message = "新增使用者失敗！" });

			await _programUserManager.Create(new ProgramUser
			{
				UserName = model.UserName,
				AspNetUserId = user.Id,
				CompanyId = model.CompanyId,
				Status = 1,
				Account = model.Account,
				Email = model.Email
			});

			return Ok(new Response<object>
			{
				IsOk = true,
				StatusCode = StatusCodes.Status200OK,
				Message = "使用者新增成功。"
			});
		}

		[HttpPost]
		[Route("registeradmin")]
		[Authorize]
		public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
		{
			var userExists = await userManager.FindByNameAsync(model.UserName);
			if (userExists != null)
				return Ok(new Response<object> { IsOk = false, StatusCode = StatusCodes.Status500InternalServerError, Message = "使用者已存在！" });
			var user = new ApplicationUser()
			{
				Email = model.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = model.UserName
			};
			var result = await userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
				return Ok(new Response<object> { IsOk = false, StatusCode = StatusCodes.Status500InternalServerError, Message = "新增使用者失敗！請檢查輸入內容，再試一次。" });

			if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
				await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
			if (!await roleManager.RoleExistsAsync(UserRoles.User))
				await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
			if (await roleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await userManager.AddToRoleAsync(user, UserRoles.User);
			}
			return Ok(new Response<object> { IsOk = true, StatusCode = 200, Message = "使用者新增成功。" });
		}

		[HttpPost]
		[Route("users")]
		[Authorize]
		public IEnumerable<UserDTO> GetUsers([FromBody] UserDTO model)
		{
			var result = userManager.Users.Select(s => new UserDTO
			{
				Id = s.Id,
				UserName = s.UserName,
				Email = s.Email,
				CompanyName = "金財通"
			});

			if (!string.IsNullOrEmpty(model.UserName))
			{
				result = result.Where(w => w.UserName.Contains(model.UserName));
			}

			return result;
		}

		[HttpPost]
		[Route("getuser")]
		[Authorize]
		public async Task<UserDTO> GetUser(string id)
		{
			var user = await userManager.FindByIdAsync(id);

			var resut = new UserDTO
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				CompanyName = "金財通"
			};

			return resut;
		}

		[HttpPost]
		[Route("forget")]
		public async Task<ActionResult<Response<object>>> Forget([FromBody]string account)
		{
			var user = await userManager.FindByNameAsync(account);
			if (user == null)
			{
				return Ok(new Response<object>
				{
					IsOk = false,
					StatusCode = StatusCodes.Status200OK,
					Message = "查無資料。"
				});
			}
			var token = await userManager.GeneratePasswordResetTokenAsync(user);
			var newPassword = GeneratePassword();
			var result = await userManager.ResetPasswordAsync(user, token, newPassword);
			if (result.Succeeded)
			{
				SendMail(user, newPassword);

				return Ok(new Response<object>
				{
					IsOk = true,
					StatusCode = StatusCodes.Status200OK,
					Message = "臨時密碼已寄送至註冊之信箱。"
				});
			}

			return Ok(new Response<object>
			{
				IsOk = false,
				StatusCode = StatusCodes.Status200OK,
				Message = "資料處理失敗，請重試。"
			});
		}

		private async void SendMail(ApplicationUser user, string newPassword)
		{
			var subject = "金財通電子發票繳費網-變更密碼通知信";
			var body = @"親愛的{0}會員您好
						   <br><br>您的臨時密碼：{1}
						   <br><br>請記得上線去更改密碼！
						   <br><br>如有任何問題，請E-mail到我們的客服信箱，我們將盡速會您處理！						   
						   <br><br><br><br>謝謝！						   
						   <br><br>客服E-mail：{2}
						   <br><br><br><br>**************************************************
						   <br>請注意：此郵件是系統自動傳送，請勿直接回覆此郵件！
						  <br>**************************************************<br>";
			var pgUser = await _programUserManager.FindProgramUser(x => x.AspNetUserId == user.Id);

			body = string.Format(body, pgUser.UserName, newPassword, _configuration["AppSetting:serviceEmail"]);
			await _emailManager.SendEmail(new List<string> { user.Email }, subject, body);			
		}
		
		private string GeneratePassword()
		{
			var en = "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";
			var num = "1234567890";
			var ran = new Random();
			var stringBuilder = new StringBuilder();
			for (int i = 0; i < 8; i++)
			{
				if (i < 4)
					stringBuilder.Append(en[ran.Next(0, en.Length)]);
				else
					stringBuilder.Append(num[ran.Next(0, num.Length)]);
			}
			return stringBuilder.ToString();
		}

		[HttpPost]
		[Route("IsPermision")]
		[Authorize]
		public async Task<ActionResult<bool>> IsPermision([FromBody] PermissionModel model)
		{
			var result = await _permissionManager.CheckPermission(model.Flag);
			return Ok(result);
		}
	}
}
