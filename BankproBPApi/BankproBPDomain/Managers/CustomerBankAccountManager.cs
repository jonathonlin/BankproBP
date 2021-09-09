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
	public class CustomerBankAccountManager
	{
		protected EfRepository<CustomerBankAccount> Repository;
		public CustomerBankAccountManager(BankproBpDbContext db)
		{
			Repository = new EfRepository<CustomerBankAccount>(db);
		}

		public async Task<IQueryable<CustomerBankAccount>> GetAllAsync(CustomerBankAccountQueryOptions options)
		{
			var query = await Repository.GetAllAsyn();
			var result = query.AsQueryable();
			if (!string.IsNullOrWhiteSpace(options.BankCode))
			{
				result = result.Where(w => w.BankCode.Contains(options.BankCode.Trim()));
			}
			if (options.CustomerId.HasValue)
			{
				result = result.Where(w => w.CustomerId == options.CustomerId);
			}

			return result;
		}

		public async Task<CustomerBankAccount> GetAsync(int id)
		{
			return await Repository.GetAsync(id);
		}

		public async Task<CustomerBankAccount> Create(CustomerBankAccount entity)
		{
			return await Repository.AddAsyn(entity);
		}

		public async Task<CustomerBankAccount> Update(CustomerBankAccount entity, int id)
		{
			return await Repository.UpdateAsyn(entity, id);
		}

		public async Task<int> DeleteAsyn(CustomerBankAccount entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
