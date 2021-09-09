using BankproBPData;
using BankproBPData.QueryOptions;
using BankproBPDomain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPDomain.Managers
{
	public class ProgramRoleManager
	{
		private readonly EfRepository<ProgramRole> Repository;

		public ProgramRoleManager(BankproBpDbContext db)
		{
			Repository = new EfRepository<ProgramRole>(db);
		}

		public async Task<IQueryable<ProgramRole>> GetProgramRolesAsyn(ProgramRoleQueryOptions options)
		{
			var query = await Repository.GetAllAsyn();
			var result = query.AsQueryable();
			if (!string.IsNullOrWhiteSpace(options.RoleName))
			{
				result = result.Where(w => w.RoleName.Contains(options.RoleName.Trim()));
			}
			if (options.Status.HasValue)
			{
				result = result.Where(w => w.Status == options.Status);
			}			
			return result;
		}

		public async Task<ProgramRole> GetProgramRoleAsyn(int id)
		{
			return await Repository.GetAsync(id);
		}

		public async Task<ProgramRole> Create(ProgramRole entity)
		{
			return await Repository.AddAsyn(entity);
		}

		public async Task<ProgramRole> Update(ProgramRole entity, int id)
		{
			return await Repository.UpdateAsyn(entity, id);
		}

		public async Task<int> DeleteAsyn(ProgramRole entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
