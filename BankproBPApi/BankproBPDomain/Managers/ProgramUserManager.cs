using BankproBPData;
using BankproBPData.Core;
using BankproBPData.QueryOptions;
using BankproBPDomain.Core;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BankproBPDomain.Managers
{
	public class ProgramUserManager
	{
		protected EfRepository<ProgramUser> Repository;
		private string _connectionString;
		private CurrentUser _currentUser;

		public ProgramUserManager(BankproBpDbContext db, CurrentUser currentUser)
		{
			Repository = new EfRepository<ProgramUser>(db);
			_connectionString = db.Database.GetConnectionString();
			_currentUser = currentUser;
		}

		public async Task<IQueryable<ProgramUser>> GetProgramUsersAsync(ProgramUserQueryOptions value)
		{
			var result = await Repository.GetAllIncluding(x => x.Company).AsNoTracking().ToListAsync();
			if (!string.IsNullOrWhiteSpace(value.UserName))
				result = result.Where(w => w.UserName.Contains(value.UserName.Trim())).ToList();
			if (!string.IsNullOrWhiteSpace(value.CompanyName))
				result = result.Where(w => w.Company.CompanyName.Contains(value.CompanyName.Trim())).ToList();			
			return result.AsQueryable();
		}

		public async Task<ProgramUser> GetProgramUserAsync(int id)
		{			
			using (var conn = new SqlConnection(_connectionString))
			{
				string sqlCommand = @"SELECT * FROM ProgramUser where Id = @Id";
				conn.Open();
				var result = await conn.QueryAsync<ProgramUser>(sqlCommand, new { Id = id });
				return result.FirstOrDefault();
			}
		}

		public async Task<ProgramUser> FindProgramUserByCurrentUser()
		{
			var name = _currentUser.GetUserName;
			return await Repository.FindAsync(w => w.AspNetUserId == _currentUser.GetUserId);
		}

		public async Task<ProgramUser> FindProgramUser(Expression<Func<ProgramUser, bool>> match)
		{
			return await Repository.FindAsync(match);
		}

		public async Task<ProgramUser> Create(ProgramUser entity)
		{
			return await Repository.AddAsyn(entity);
		}

		public async Task<ProgramUser> Update(ProgramUser entity, int id)
		{
			return await Repository.UpdateAsyn(entity, id);
		}

		public async Task<int> Delete(ProgramUser entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
