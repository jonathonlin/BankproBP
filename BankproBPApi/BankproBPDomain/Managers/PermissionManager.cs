using BankproBPData;
using BankproBPData.Core;
using BankproBPData.Dtos;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPDomain.Managers
{
	public class PermissionManager
	{
		private readonly CurrentUser _currentUser;
		private readonly string _connectionString;

		public PermissionManager(BankproBpDbContext db, CurrentUser currentUser)
		{
			_currentUser = currentUser;
			_connectionString = db.Database.GetConnectionString();
		}

		public async Task<bool> SavePermission(List<Permission> items)
		{			
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (var transaction = conn.BeginTransaction())
				{
					try 
					{
						var roleId = items.First().RoleId;
						string deleteCmd = @"Delete Permission where RoleId = @RoleId";
						await conn.ExecuteAsync(deleteCmd, new { RoleId = roleId }, transaction);
						string insertCmd = @"Insert Into Permission (RoleId, ProgramId, CreateId, CreateDate) 
												values (@RoleId, @ProgramId, @CreateId, @CreateDate)";
						foreach (var item in items)
						{
							await conn.ExecuteAsync(insertCmd, new {
								RoleId = item.RoleId,
								ProgramId = item.ProgramId,
								CreateId = _currentUser.GetUserId,
								CreateDate = DateTime.UtcNow
							}, transaction);
						}

						transaction.Commit();
						return true;
					}
					catch(Exception ex)
					{
						transaction.Rollback();
						return false;
					}
				}
			}
		}

		public async Task<List<CompanyProgram>> GetRolePermission()
		{
			var createId = _currentUser.GetUserId;
			using (var conn = new SqlConnection(_connectionString))
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				var cmdStr = @"select a.* 
								from CompanyProgram a inner join Permission b
								on a.Id = b.ProgramId inner join ProgramUserRole c
								on b.RoleId = c.RoleId inner join ProgramUser d
								on c.UserId = d.Id
								where d.AspNetUserId = @AspNetUserId";

				var result = await conn.QueryAsync<CompanyProgram>(cmdStr, new { AspNetUserId = createId });				
				return result.ToList();
			}	
		}

		public async Task<List<Permission>> GetPermissions(int id)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				if(conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				var cmdStr = @"select * from Permission where RoleId = @RoleId";
				var result = await conn.QueryAsync<Permission>(cmdStr, new { RoleId = id });
				return result.ToList();
			}
		}

		public async Task<bool> CheckPermission(string model)
		{
			var aspNetUserId = _currentUser.GetUserId;
			using (var conn = new SqlConnection(_connectionString))
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				var cmdStr = @"select d.*
								from ProgramUser a inner join ProgramUserRole b
								on a.Id = b.UserId inner join Permission c
								on b.RoleId = c.RoleId inner join CompanyProgram d
								on c.ProgramId = d.Id
								where a.AspNetUserId = @AspNetUserId and d.ProgramUrl = @ProgramUrl";
				var result = await conn.QueryAsync<Permission>(cmdStr, new { AspNetUserId = aspNetUserId, ProgramUrl = model });
				return result.Any();
			}
		}
	}
}
