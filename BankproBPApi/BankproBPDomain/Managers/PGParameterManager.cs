using BankproBPData;
using BankproBPDomain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPDomain.Managers
{
	public class PGParameterManager
	{
		protected EfRepository<PGParameter> Repository;
		public PGParameterManager(BankproBpDbContext db)
		{
			Repository = new EfRepository<PGParameter>(db);
		}

		public async Task<IQueryable<PGParameter>> GetAllAsyn(string keyCode)
		{
			var query = await Repository.GetAllAsyn();
			var result = query.AsQueryable();
			if (!string.IsNullOrWhiteSpace(keyCode))
			{
				result = result.Where(w => w.KeyCode == keyCode);
			}
			return result;
		}
	}
}
