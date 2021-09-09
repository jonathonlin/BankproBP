using BankproBPData;
using BankproBPData.QueryOptions;
using BankproBPDomain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPDomain.Managers
{
	public class ProgramUserRoleManager
	{
		private readonly EfRepository<ProgramUserRole> Repository;

		public ProgramUserRoleManager(BankproBpDbContext db)
		{
			Repository = new EfRepository<ProgramUserRole>(db);
		}
		public async Task<IQueryable<ProgramUserRole>> GetProgramUserRolesAsyn(ProgramUserRoleQueryOptions options)
		{
			var result = await Repository.GetAllIncluding(x => x.ProgramUser, y=>y.ProgramRole).AsNoTracking().ToListAsync();
			
			if (options.UserId.HasValue)
			{
				result = result.Where(w => w.UserId == options.UserId).ToList();
			}
			if (options.RoleId.HasValue)
			{
				result = result.Where(w => w.RoleId == options.RoleId).ToList();
			}
			return result.AsQueryable();
		}
				
		public async Task<ProgramUserRole> GetProgramUserRoleAsyn(int id)
		{
			return await Repository.GetAsync(id);
		}

		public async Task<bool> HasExists(int userId, int  roleId)
		{
			var result = await Repository.FindAllAsync(w => w.UserId == userId && w.RoleId == roleId);
			return result.Any();
		}

		public async Task<ProgramUserRole> Create(ProgramUserRole entity)
		{
			return await Repository.AddAsyn(entity);
		}

		public async Task<ProgramUserRole> Update(ProgramUserRole entity, int id)
		{
			return await Repository.UpdateAsyn(entity, id);
		}

		public async Task<int> DeleteAsyn(ProgramUserRole entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
