using BankproBPData.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankproBPData
{
      public class BankproBpDbContext : IdentityDbContext<ApplicationUser>
      {
		private readonly CurrentUser _currentUser;

		public BankproBpDbContext(DbContextOptions<BankproBpDbContext> options, CurrentUser currentUser) : base(options)
            {
                  _currentUser = currentUser;
            }		
		public DbSet<CompanyProgram> CompanyPrograms { get; set; }
		public DbSet<CompanyProgramButton> CompanyProgramButtons { get; set; }
            public DbSet<Company> Companies { get; set; }
            public DbSet<PGParameter> PGParameters { get; set; }
		public DbSet<ProgramRole> ProgramRoles { get; set; }
		public DbSet<ProgramUser> ProgramUsers { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<ProgramUserRole> ProgramUserRoles { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<CompanyBankAccount> CompanyBankAccounts { get; set; }
		public DbSet<CustomerBankAccount> CustomerBankAccounts { get; set; }
            public DbSet<Bank> Banks { get; set; }
		public DbSet<AccountPayable> AccountPayables { get; set; }
            public DbSet<AccountPayableDetail> AccountPayableDetails { get; set; }
		public DbSet<FormNoCount> FormNoCounts { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
            {
                  base.OnModelCreating(builder);
                  builder.Entity<CompanyProgram>().ToTable("CompanyProgram");
                  builder.Entity<CompanyProgramButton>().ToTable("CompanyProgramButton");
                  builder.Entity<Company>().ToTable("Company");                  
                  builder.Entity<PGParameter>().ToTable("PGParameter");
                  builder.Entity<ProgramRole>().ToTable("ProgramRole");
                  builder.Entity<ProgramUser>().ToTable("ProgramUser");
                  builder.Entity<Permission>().ToTable("Permission");
                  builder.Entity<ProgramUserRole>().ToTable("ProgramUserRole");
                  builder.Entity<Customer>().ToTable("Customer");
                  builder.Entity<CompanyBankAccount>().ToTable("CompanyBankAccount");
                  builder.Entity<CustomerBankAccount>().ToTable("CustomerBankAccount");
                  builder.Entity<Bank>().ToTable("Bank");
                  builder.Entity<AccountPayable>().ToTable("AccountPayable");
                  builder.Entity<AccountPayableDetail>().ToTable("AccountPayableDetail");
                  builder.Entity<FormNoCount>().ToTable("FormNoCount");

                  var adminId = Guid.NewGuid().ToString();
			var hasher = new PasswordHasher<ApplicationUser>();
			builder.Entity<ApplicationUser>().HasData(
				new ApplicationUser
                        {                              
					Id = adminId,
					UserName = "admin",
					NormalizedUserName = "ADMIN",
					Email = "jonathon0418@gmail.com",
					NormalizedEmail = "JONATHON0418@GMAIL>COM",
					EmailConfirmed = false,
					PasswordHash = hasher.HashPassword(null, "!QAZ2wsx"),
					SecurityStamp = string.Empty,                              
				}
			);

			builder.Entity<ProgramUser>().HasData(
				new ProgramUser
				{
					Id = 1,
					AspNetUserId = adminId,
					UserName = "admin",
					Email = "jonathon0418@gmail.com",
					Account = "admin",
                              AccountType = 1,
					CompanyId = 1,                              
					Status = 1,
					CreateId = adminId,
					CreateDate = DateTime.UtcNow
				}
			);

			builder.Entity<ProgramRole>().HasData(new ProgramRole { Id = 1, RoleName = "admin", Status = 1, CreateId = adminId, CreateDate = DateTime.UtcNow });

			builder.Entity<ProgramUserRole>().HasData(new ProgramUserRole { Id =1, RoleId = 1, UserId = 1, CreateId = adminId, CreateDate = DateTime.UtcNow });

			builder.Entity<Company>().HasData(
                        new Company { 
                              Id = 1,
                              CompanyName = "金財通", 
                              Email = "", 
                              Tel = "02-87121298", 
                              ZipCode="105", 
                              Address = "台北市松山區南京東路三段261號7樓",
                              CreateId = adminId,
                              CreateDate = DateTime.UtcNow
                        });
                  builder.Entity<CompanyProgram>().HasData(
                        new CompanyProgram { Id = 1, ProgramName = "系統維護", ParentId = null, ProgramType = 0, Status = 1, Sort = 1, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 2, ProgramName = "使用者維護", ParentId = 1, ProgramType = 1, ProgramUrl = "sys/register", Status = 1, Sort = 2, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 3, ProgramName = "角色維護", ParentId = 1, ProgramType = 1, ProgramUrl = "sys/role", Status = 1, Sort = 3, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 4, ProgramName = "使用者角色維護", ParentId = 1, ProgramType = 1, ProgramUrl = "sys/userrole", Status = 1, Sort = 4, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 5, ProgramName = "程式維護", ParentId = 1, ProgramType = 1, ProgramUrl = "sys/program", Status = 1, Sort = 5, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 6, ProgramName = "程式按鈕維護", ParentId = 1, ProgramType = 1, ProgramUrl = "sys/programbutton", Status = 1, Sort = 6, CreateId = adminId, CreateDate = DateTime.UtcNow },                                                
                        new CompanyProgram { Id = 7, ProgramName = "權限維護", ParentId = 1, ProgramType = 1, ProgramUrl = "sys/programsetting", Status = 1, Sort = 7, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 8, ProgramName = "修改密碼", ParentId = 1, ProgramType = 1, ProgramUrl = "sys/changepassword", Status = 1, Sort = 8, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 9, ProgramName = "基本資料維護", ParentId = null, ProgramType = 0, Status = 1, Sort = 9, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 10, ProgramName = "銀行維護", ParentId = 9, ProgramType = 1, ProgramUrl = "basic/bank", Status = 1, Sort = 10, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 11, ProgramName = "公司維護", ParentId = 9, ProgramType = 1, ProgramUrl = "basic/company", Status = 1, Sort = 11, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 12, ProgramName = "公司銀行帳戶維護", ParentId = 9, ProgramType = 1, ProgramUrl = "basic/companybank", Status = 1, Sort = 12, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 13, ProgramName = "客戶維護", ParentId = 9, ProgramType = 1, ProgramUrl = "basic/customer", Status = 1, Sort = 13, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 14, ProgramName = "客戶銀行帳戶維護", ParentId = 9, ProgramType = 1, ProgramUrl = "basic/customerbank", Status = 1, Sort = 14, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 15, ProgramName = "應付帳款", ParentId = null, ProgramType = 0, Status = 1, Sort = 15, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new CompanyProgram { Id = 16, ProgramName = "應付立帳", ParentId = 15, ProgramType = 1, ProgramUrl = "payable/ap", Status = 1, Sort = 16, CreateId = adminId, CreateDate = DateTime.UtcNow }
                        );

                  builder.Entity<Permission>().HasData(
                        new Permission { Id = 1, RoleId = 1, ProgramId = 1, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 2, RoleId = 1, ProgramId = 2, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 3, RoleId = 1, ProgramId = 3, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 4, RoleId = 1, ProgramId = 4, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 5, RoleId = 1, ProgramId = 5, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 6, RoleId = 1, ProgramId = 6, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 7, RoleId = 1, ProgramId = 7, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 8, RoleId = 1, ProgramId = 8, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 9, RoleId = 1, ProgramId = 9, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 10, RoleId = 1, ProgramId = 10, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 11, RoleId = 1, ProgramId = 11, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 12, RoleId = 1, ProgramId = 12, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 13, RoleId = 1, ProgramId = 13, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 14, RoleId = 1, ProgramId = 14, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 15, RoleId = 1, ProgramId = 15, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new Permission { Id = 16, RoleId = 1, ProgramId = 16, CreateId = adminId, CreateDate = DateTime.UtcNow }
                        );

                  builder.Entity<PGParameter>().HasData(                        
                        new PGParameter { Id = 1, KeyCode = "APStatus", KeyName = "立帳", KeyValue = 1010, Sort = 1, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 2, KeyCode = "APStatus", KeyName = "銷帳", KeyValue = 1020, Sort = 2, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 3, KeyCode = "APStatus", KeyName = "呆帳", KeyValue = 1030, Sort = 3, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 4, KeyCode = "APStatus", KeyName = "結帳", KeyValue = 1040, Sort = 4, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 5, KeyCode = "PaymentType", KeyName = "信用卡-CTBC", KeyValue = 2101, Sort = 1, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 6, KeyCode = "PaymentType", KeyName = "ATM-CTBC", KeyValue = 2201, Sort = 2, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 7, KeyCode = "PaymentType", KeyName = "ATM-ESUN", KeyValue = 2202, Sort = 3, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 8, KeyCode = "PaymentType", KeyName = "ATM-HNCB", KeyValue = 2203, Sort = 4, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 9, KeyCode = "PrepaymentUseType", KeyName = "沖帳", KeyValue = 3100, Sort = 1, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 10, KeyCode = "PrepaymentUseType", KeyName = "退費", KeyValue = 3200, Sort = 2, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 11, KeyCode = "AccountType", KeyName = "公司帳戶", KeyValue = 1, Sort = 1, CreateId = adminId, CreateDate = DateTime.UtcNow },
                        new PGParameter { Id = 12, KeyCode = "AccountType", KeyName = "客戶帳戶", KeyValue = 2, Sort = 2, CreateId = adminId, CreateDate = DateTime.UtcNow }
                        );
            }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
                  TrackChanges();
			return base.SaveChangesAsync(cancellationToken);
		}
            	

            private void TrackChanges()
            {
                  foreach (var entry in this.ChangeTracker.Entries().Where(w => w.State == EntityState.Added || w.State == EntityState.Modified))
                  {
                        var type = entry.Entity.GetType();
                        if (entry.Entity is IAuditable)
                        {
                              var createBy = type.GetProperty("CreateId");
                              var createDate = type.GetProperty("CreateDate");
                              var updateBy = type.GetProperty("UpdateId");
                              var updateDate = type.GetProperty("UpdateDate");
                              if (updateBy != null) updateBy.SetValue(entry.Entity, _currentUser.GetUserId);
                              if (updateDate != null) updateDate.SetValue(entry.Entity, DateTime.UtcNow);
                              if (entry.State == EntityState.Added)
                              {
                                    if (createBy != null) createBy.SetValue(entry.Entity, _currentUser.GetUserId);
                                    if (createDate != null) createDate.SetValue(entry.Entity, DateTime.UtcNow);
                              }
                              else if(entry.State == EntityState.Modified)
                              {
                                    if (createBy != null)
                                          entry.Property("CreateId").IsModified = false;
                                    if (createDate != null)
                                          entry.Property("CreateDate").IsModified = false;
                              }
                        }
                  }
            }
      }
}
