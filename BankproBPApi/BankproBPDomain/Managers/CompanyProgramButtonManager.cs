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
	public class CompanyProgramButtonManager
	{
		protected EfRepository<CompanyProgramButton> Repository;

		public CompanyProgramButtonManager(BankproBpDbContext db)
		{
			Repository = new EfRepository<CompanyProgramButton>(db);
		}

		public async Task<IQueryable<CompanyProgramButton>> GetAllAsync(CompanyProgramButtonQueryOptions options)
		{
			var query = await Repository.GetAllAsyn();
			var result = query.AsQueryable();
			if (!string.IsNullOrWhiteSpace(options.ButtonText))
			{
				result = result.Where(w => w.ButtonText.Contains(options.ButtonText));
			}
			if(options.ProgramId.HasValue)
			{
				result = result.Where(w => w.ProgramId == options.ProgramId);
			}
			if(options.Status.HasValue)
			{
				result = result.Where(w => w.Status == options.Status);
			}
			return result;
		}

		public async Task<CompanyProgramButton> GetAsync(int id)
		{
			return await Repository.GetAsync(id);
		}

		public async Task<CompanyProgramButton> Create(CompanyProgramButton entity)
		{
			return await Repository.AddAsyn(entity);
		}

		public async Task<CompanyProgramButton> Update(CompanyProgramButton entity, int id)
		{
			return await Repository.UpdateAsyn(entity, id);
		}

		public async Task<int> DeleteAsyn(CompanyProgramButton entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
