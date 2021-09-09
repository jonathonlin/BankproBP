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
	public class CustomerManager
	{
		protected EfRepository<Customer> Repository;
		public CustomerManager(BankproBpDbContext db)
		{
			Repository = new EfRepository<Customer>(db);
		}
		public async Task<IQueryable<Customer>> GetAllAsyn(CustomerQueryOptions options)
		{
			var query = await Repository.GetAllAsyn();
			var result = query.AsQueryable();
			if (!string.IsNullOrWhiteSpace(options.CustomerName))
			{
				result = result.Where(w => w.CustomerName.Contains(options.CustomerName.Trim()));
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
			if (!string.IsNullOrWhiteSpace(options.Mobile))
			{
				result = result.Where(w => w.Mobile.Contains(options.Mobile.Trim()));
			}
			if (options.CompanyId.HasValue)
			{
				result = result.Where(w => w.CompanyId == options.CompanyId);
			}
			return result;
		}

		public async Task<Customer> GetAsync(int id)
		{
			return await Repository.GetAsync(id);
		}

		public async Task<Customer> Create(Customer entity)
		{
			return await Repository.AddAsyn(entity);
		}

		public async Task<Customer> Update(Customer entity, int id)
		{
			return await Repository.UpdateAsyn(entity, id);
		}

		public async Task<int> DeleteAsyn(Customer entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
