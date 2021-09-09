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
	public class BankManager
	{
		protected EfRepository<Bank> Repository;
		public BankManager(BankproBpDbContext db)
		{
			Repository = new EfRepository<Bank>(db);
		}

		public async Task<IQueryable<Bank>> GetAllAsyn(BankQueryOptions options)
		{
			var query = await Repository.GetAllAsyn();
			var result = query.AsQueryable();
			if (!string.IsNullOrWhiteSpace(options.BankCode))
			{
				result = result.Where(w => w.BankCode.Contains(options.BankCode.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(options.BankName))
			{
				result = result.Where(w => w.BankName.Contains(options.BankName.Trim()));
			}
			
			return result;
		}

		public async Task<Bank> GetAsync(int id)
		{
			return await Repository.GetAsync(id);
		}

		public async Task<Bank> Create(Bank entity)
		{
			return await Repository.AddAsyn(entity);
		}

		public async Task<Bank> Update(Bank entity, int id)
		{
			return await Repository.UpdateAsyn(entity, id);
		}

		public async Task<int> DeleteAsyn(Bank entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
