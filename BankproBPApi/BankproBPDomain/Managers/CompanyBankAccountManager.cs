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
	public class CompanyBankAccountManager
	{
		protected EfRepository<CompanyBankAccount> Repository;
		public CompanyBankAccountManager(BankproBpDbContext db)
		{
			Repository = new EfRepository<CompanyBankAccount>(db);
		}

		public async Task<IQueryable<CompanyBankAccount>> GetAllAsync(CompanyBankAccountQueryOptions options)
		{
			var query = await Repository.GetAllAsyn();
			var result = query.AsQueryable();
			if (!string.IsNullOrWhiteSpace(options.BankCode))
			{
				result = result.Where(w => w.BankCode.Contains(options.BankCode.Trim()));
			}
			if (options.CompanyId.HasValue)
			{
				result = result.Where(w => w.CompanyId == options.CompanyId);
			}
			
			return result;
		}

		public async Task<CompanyBankAccount> GetAsync(int id)
		{
			return await Repository.GetAsync(id);
		}

		public async Task<CompanyBankAccount> Create(CompanyBankAccount entity)
		{
			return await Repository.AddAsyn(entity);
		}

		public async Task<CompanyBankAccount> Update(CompanyBankAccount entity, int id)
		{
			return await Repository.UpdateAsyn(entity, id);
		}

		public async Task<int> DeleteAsyn(CompanyBankAccount entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
