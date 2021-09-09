using BankproBPData;
using BankproBPData.Core;
using BankproBPData.QueryOptions;
using BankproBPDomain.Core;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPDomain.Managers
{
	public class AccountPayableManager
	{
		protected EfRepository<AccountPayable> Repository;
		private readonly string _connectionString;
		private readonly CurrentUser _currentUser;
		public AccountPayableManager(BankproBpDbContext db, CurrentUser currentUser)
		{
			Repository = new EfRepository<AccountPayable>(db);
			_connectionString = db.Database.GetConnectionString();
			_currentUser = currentUser;
		}
		public async Task<IQueryable<AccountPayable>> GetAllAsyn(AccountPayableQueryOptions options)
		{
			var query = await Repository.GetAllAsyn();
			var result = query.AsQueryable();
			if (options.CustomerId.HasValue)
			{
				result = result.Where(w => w.CustomerId == options.CustomerId);
			}
			if (options.ApStatus.HasValue)
			{
				result = result.Where(w => w.ApStatus == options.ApStatus);
			}
			if (!string.IsNullOrWhiteSpace(options.YearMonth))
			{
				result = result.Where(w => w.YearMonth.Contains(options.YearMonth.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(options.ApNo))
			{
				result = result.Where(w => w.ApNo.Contains(options.ApNo.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(options.InvoiceNo))
			{
				result = result.Where(w => w.InvoiceNo.Contains(options.InvoiceNo.Trim()));
			}
			return result;
		}

		public async Task<AccountPayable> GetAsync(int id)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				string sqlCommand = @"select * from AccountPayable where Id = @Id";
				conn.Open();
				var query = await conn.QueryAsync<AccountPayable>(sqlCommand, new { Id = id });
				var result = query.FirstOrDefault();
				var details = await conn.QueryAsync<AccountPayableDetail>(@"select * from AccountPayableDetail where ApNo = @ApNo", new { ApNo = result.ApNo });
				result.AccountPayableDetails = details.ToList();
				return result;
			}
		}

		public async Task<AccountPayable> Create(AccountPayable entity)
		{
			entity.ApStatus = 1010;
			return await Repository.AddAsyn(entity);
		}

		public async Task<AccountPayable> Update(AccountPayable entity, int id)
		{
			var userId = _currentUser.GetUserId;
			using (var conn = new SqlConnection(_connectionString))
			{
				string updateCmd = @"Update AccountPayable 
											Set CustomerId = @CustomerId
												,YearMonth = @YearMonth
											     ,ApNo = @ApNo
											     ,InvoiceNo = @InvoiceNo
											     ,InvoiceDate = @InvoiceDate
											     ,InvoiceAmount = @InvoiceAmount
											     ,ApAmount = @ApAmount
											     ,ExpireDate = @ExpireDate
											     ,ApStatus = @ApStatus
											     ,Note = @Note
											     ,CreateId = @CreateId
											     ,CreateDate = @CreateDate
											     ,UpdateId = @UpdateId
											     ,UpdateDate = @UpdateDate
										Where Id = @Id";
				string deleteCmd = @"Delete AccountPayableDetail Where ApNo = @ApNo";
				string insertCmd = @"Insert into AccountPayableDetail 
										    Values (@ApNo
												     ,@ProductName
												     ,@UnitPrice
												     ,@Quantity
												     ,@TotalAmount
												     ,@Note
												     ,@CreateId
												     ,@CreateDate
												     ,@UpdateId
												     ,@UpdateDate
												     ,@AccountPayableId)";
				entity.UpdateId = userId;
				entity.UpdateDate = DateTime.UtcNow;
				entity.AccountPayableDetails.ForEach(e => {
					e.ApNo = entity.ApNo;					
					e.CreateId = entity.CreateId;
					e.CreateDate = entity.CreateDate;
					e.UpdateId = entity.UpdateId;
					e.UpdateDate = entity.UpdateDate;
					e.AccountPayableId = entity.Id;
				});

				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					var res = await conn.ExecuteAsync(updateCmd, entity, trans);
					res = await conn.ExecuteAsync(deleteCmd, new { ApNo = entity.ApNo }, trans);
					res = await conn.ExecuteAsync(insertCmd, entity.AccountPayableDetails, trans);
					trans.Commit();
				}

				var result = conn.QueryFirstOrDefault<AccountPayable>("select * from AccountPayable where Id = @Id", new { Id = id });
				result.AccountPayableDetails = conn.Query<AccountPayableDetail>("select * from AccountPayableDetail where ApNo = @ApNo", new { ApNo = result.ApNo }).ToList();

				return result;
			}
		}

		public async Task<int> DeleteAsyn(AccountPayable entity)
		{
			return await Repository.DeleteAsyn(entity);
		}
	}
}
