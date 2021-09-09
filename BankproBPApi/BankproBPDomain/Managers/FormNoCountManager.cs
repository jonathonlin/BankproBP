using BankproBPData;
using BankproBPData.Core;
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
	public class FormNoCountManager
	{
		private readonly string _connectionString;
		private readonly CurrentUser _currentUser;

		public FormNoCountManager(BankproBpDbContext db, CurrentUser currentUser)
		{
			_connectionString = db.Database.GetConnectionString();
			_currentUser = currentUser;
		}

		public async Task<string> GetFormNo(string formType, int year, int month, int day)
		{
			var userId = _currentUser.GetUserId;
			using (var conn = new SqlConnection(_connectionString))
			{
				string sqlCommand = @"BEGIN TRY
	                        BEGIN TRANSACTION
		                        Update FormNoCount 
			                        Set SerialNo = SerialNo + 1,
				                        UpdateId = @UserId,
				                        UpdateDate = GETUTCDATE()                            
		                        Where FormType = @FormType
                                  and Year = @Year
                                  and Month = @Month
                                  and Day = @Day

		                        If @@ROWCOUNT = 0
			                        Insert into FormNoCount values (@FormType, @Year, @Month, @Day, 1, @UserId, GETUTCDATE(), @UserId, GETUTCDATE())
		
	                        COMMIT TRANSACTION
	                        Select * From FormNoCount 
                                                    Where FormType = @FormType
                                                    and Year = @Year
                                                    and Month = @Month
                                                    and Day = @Day
                           END TRY
                           BEGIN CATCH
	                        ROLLBACK TRANSACTION
	                        SELECT  
                            ERROR_NUMBER() AS ErrorNumber  
                            ,ERROR_SEVERITY() AS ErrorSeverity  
                            ,ERROR_STATE() AS ErrorState  
                            ,ERROR_PROCEDURE() AS ErrorProcedure  
                            ,ERROR_LINE() AS ErrorLine  
                            ,ERROR_MESSAGE() AS ErrorMessage;
                           END CATCH";
				conn.Open();
				var query = await conn.QueryAsync<FormNoCount>(sqlCommand, new {
					UserId = userId,
					FormType = formType,
					Year = year,
					Month = month,
					Day = day
				});
				var record = query.FirstOrDefault();
				return record.FormType + record.Year.ToString().Substring(2) + record.Month.ToString().PadLeft(2,'0') + record.Day.ToString().PadLeft(2,'0') + record.SerialNo.ToString().PadLeft(6,'0');
			}
		}
	}
}
