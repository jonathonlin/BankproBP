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
	public class CompanyProgramManager
	{
		protected IRepository<CompanyProgram> Repository { get; private set; }
		public CompanyProgramManager(BankproBpDbContext db)
		{
			Repository = new EfRepository<CompanyProgram>(db);
		}

		public async Task<IQueryable<CompanyProgram>> GetCompanyProgramsAsync(CompanyProgramQueryOptions options)
		{
			var query = await Repository.GetAllAsyn();
			var result = query.AsQueryable();
			if (!string.IsNullOrEmpty(options.ProgramName))
			{
				result = result.Where(w => w.ProgramName.Contains(options.ProgramName.Trim()));
			}
			if (options.ProgramType.HasValue)
			{
				result = result.Where(w => w.ProgramType == options.ProgramType);
			}
			if (options.Status.HasValue)
			{
				result = result.Where(w => w.Status == options.Status);
			}
			return result;
		}

		public async Task<CompanyProgram> GetCompanyProgramAsyn(int id)
		{
			return await Repository.GetAsync(id);
		}

		public async Task<CompanyProgram> Create(CompanyProgram entity)
		{
			return await Repository.AddAsyn(entity);
		}

		public async Task<CompanyProgram> Update(CompanyProgram entity, int id)
		{
			return await Repository.UpdateAsyn(entity, id);
		}

		public async Task<int> DeleteAsyn(CompanyProgram entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
