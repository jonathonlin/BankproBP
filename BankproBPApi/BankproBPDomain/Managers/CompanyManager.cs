using BankproBPData;
using BankproBPData.Core;
using BankproBPData.QueryOptions;
using BankproBPDomain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPDomain.Managers
{
	public class CompanyManager
	{
		protected EfRepository<Company> Repository;
		private ProgramUserManager _programUserManager;

		public CompanyManager(BankproBpDbContext db, ProgramUserManager programUserManager)
		{
			Repository = new EfRepository<Company>(db);
			_programUserManager = programUserManager;
		}

		public async Task<IQueryable<Company>> GetCompaniesAsyn(CompanyQueryOptions options)
		{
			var pgUser = await _programUserManager.FindProgramUserByCurrentUser();

			var query = await Repository.GetAllAsyn();
			var result = query.AsQueryable();
			result = result.Where(w => w.Id == pgUser.CompanyId);
			if (!string.IsNullOrWhiteSpace(options.CompanyName))
			{
				result = result.Where(w => w.CompanyName.Contains(options.CompanyName.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(options.Email))
			{
				result = result.Where(w => w.Email.Contains(options.Email.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(options.Tel))
			{
				result = result.Where(w => w.Tel.Contains(options.Tel.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(options.ZipCode))
			{
				result = result.Where(w => w.ZipCode.Contains(options.ZipCode.Trim()));
			}
			return result;			
		}

		public async Task<Company> GetCompanyAsyn(int id)
		{
			return await Repository.GetAsync(id);
		}

		public async Task<Company> Create(Company entity)
		{
			return await Repository.AddAsyn(entity);
		}

		public async Task<Company> Update(Company entity, int id)
		{
			return await Repository.UpdateAsyn(entity, id);
		}

		public async Task<int> DeleteAsyn(Company entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
