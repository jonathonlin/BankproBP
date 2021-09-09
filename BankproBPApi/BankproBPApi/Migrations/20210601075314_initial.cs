using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankproBPApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountPayable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    YearMonth = table.Column<string>(type: "varchar(6)", nullable: true),
                    ApNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    InvoiceNo = table.Column<string>(type: "varchar(10)", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InvoiceAmount = table.Column<decimal>(type: "decimal", nullable: false),
                    ApAmount = table.Column<decimal>(type: "decimal", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ApStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPayable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankCode = table.Column<string>(type: "varchar(6)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Tel = table.Column<string>(type: "varchar(30)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(6)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyProgram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ProgramType = table.Column<int>(type: "int", nullable: false),
                    ProgramUrl = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProgram", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormNoCount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    SerialNo = table.Column<int>(type: "int", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormNoCount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    ButtonId = table.Column<int>(type: "int", nullable: true),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PGParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyCode = table.Column<string>(type: "varchar(30)", nullable: true),
                    KeyName = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    KeyValue = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PGParameter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgramRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountPayableDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AccountPayableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPayableDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountPayableDetail_AccountPayable_AccountPayableId",
                        column: x => x.AccountPayableId,
                        principalTable: "AccountPayable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyBankAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankCode = table.Column<string>(type: "varchar(5)", nullable: true),
                    BankAccount = table.Column<string>(type: "varchar(30)", nullable: true),
                    BankAccountName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CompanyBankAtmId = table.Column<string>(type: "varchar(30)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyBankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyBankAccount_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Tel = table.Column<string>(type: "varchar(30)", nullable: true),
                    Mobile = table.Column<string>(type: "varchar(30)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(6)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspNetUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    Account = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramUser_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyProgramButton",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ButtonText = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ButtonAction = table.Column<string>(type: "varchar(100)", nullable: true),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompanyProgramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProgramButton", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyProgramButton_CompanyProgram_CompanyProgramId",
                        column: x => x.CompanyProgramId,
                        principalTable: "CompanyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerBankAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BankCode = table.Column<string>(type: "varchar(5)", nullable: true),
                    BankAccountName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    BankAccount = table.Column<string>(type: "varchar(30)", nullable: true),
                    AllowDiffAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerBankAccount_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramUserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProgramUserId = table.Column<int>(type: "int", nullable: true),
                    ProgramRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramUserRole_ProgramRole_ProgramRoleId",
                        column: x => x.ProgramRoleId,
                        principalTable: "ProgramRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramUserRole_ProgramUser_ProgramUserId",
                        column: x => x.ProgramUserId,
                        principalTable: "ProgramUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 0, "4a8f9d46-702b-4dee-ad4c-1006ad48a216", "jonathon0418@gmail.com", false, false, null, "JONATHON0418@GMAIL>COM", "ADMIN", "AQAAAAEAACcQAAAAEHDf2TydATz053YHVbrSI3t1htg0HF7xVerEhblU3C9WLskqZitt/fNNKWN1JRGKKg==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Address", "CompanyName", "CreateDate", "CreateId", "Email", "Tel", "UpdateDate", "UpdateId", "ZipCode" },
                values: new object[] { 1, "台北市松山區南京東路三段261號7樓", "金財通", new DateTime(2021, 6, 1, 7, 53, 13, 623, DateTimeKind.Utc).AddTicks(8241), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "", "02-87121298", null, null, "105" });

            migrationBuilder.InsertData(
                table: "CompanyProgram",
                columns: new[] { "Id", "CreateDate", "CreateId", "ParentId", "ProgramName", "ProgramType", "ProgramUrl", "Sort", "Status", "UpdateDate", "UpdateId" },
                values: new object[,]
                {
                    { 16, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(143), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 15, "應付立帳", 1, "payable/ap", 16, 1, null, null },
                    { 15, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(142), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", null, "應付帳款", 0, null, 15, 1, null, null },
                    { 14, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(141), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 9, "客戶銀行帳戶維護", 1, "basic/customerbank", 14, 1, null, null },
                    { 13, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(139), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 9, "客戶維護", 1, "basic/customer", 13, 1, null, null },
                    { 12, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(138), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 9, "公司銀行帳戶維護", 1, "basic/companybank", 12, 1, null, null },
                    { 11, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(137), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 9, "公司維護", 1, "basic/company", 11, 1, null, null },
                    { 9, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(133), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", null, "基本資料維護", 0, null, 9, 1, null, null },
                    { 10, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(135), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 9, "銀行維護", 1, "basic/bank", 10, 1, null, null },
                    { 7, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(130), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 1, "權限維護", 1, "sys/programsetting", 7, 1, null, null },
                    { 6, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(128), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 1, "程式按鈕維護", 1, "sys/programbutton", 6, 1, null, null },
                    { 5, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(127), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 1, "程式維護", 1, "sys/program", 5, 1, null, null },
                    { 4, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(125), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 1, "使用者角色維護", 1, "sys/userrole", 4, 1, null, null },
                    { 3, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(124), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 1, "角色維護", 1, "sys/role", 3, 1, null, null },
                    { 2, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(121), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 1, "使用者維護", 1, "sys/register", 2, 1, null, null },
                    { 1, new DateTime(2021, 6, 1, 7, 53, 13, 623, DateTimeKind.Utc).AddTicks(9809), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", null, "系統維護", 0, null, 1, 1, null, null },
                    { 8, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(131), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 1, "修改密碼", 1, "sys/changepassword", 8, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "PGParameter",
                columns: new[] { "Id", "CreateDate", "CreateId", "KeyCode", "KeyName", "KeyValue", "Sort", "UpdateDate", "UpdateId" },
                values: new object[,]
                {
                    { 8, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3060), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "PaymentType", "ATM-HNCB", 2203, 4, null, null },
                    { 12, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3065), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "AccountType", "客戶帳戶", 2, 2, null, null },
                    { 11, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3064), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "AccountType", "公司帳戶", 1, 1, null, null },
                    { 10, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3063), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "PrepaymentUseType", "退費", 3200, 2, null, null },
                    { 9, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3061), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "PrepaymentUseType", "沖帳", 3100, 1, null, null },
                    { 7, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3058), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "PaymentType", "ATM-ESUN", 2202, 3, null, null },
                    { 6, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3056), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "PaymentType", "ATM-CTBC", 2201, 2, null, null },
                    { 5, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3055), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "PaymentType", "信用卡-CTBC", 2101, 1, null, null },
                    { 4, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3054), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "APStatus", "結帳", 1040, 4, null, null },
                    { 3, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3052), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "APStatus", "呆帳", 1030, 3, null, null },
                    { 2, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(3051), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "APStatus", "銷帳", 1020, 2, null, null },
                    { 1, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(2877), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "APStatus", "立帳", 1010, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "ButtonId", "CreateDate", "CreateId", "ProgramId", "RoleId", "UpdateDate", "UpdateId" },
                values: new object[,]
                {
                    { 16, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1650), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 16, 1, null, null },
                    { 15, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1649), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 15, 1, null, null },
                    { 14, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1648), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 14, 1, null, null },
                    { 13, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1647), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 13, 1, null, null },
                    { 12, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1646), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 12, 1, null, null },
                    { 11, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1645), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 11, 1, null, null },
                    { 10, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1645), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 10, 1, null, null },
                    { 9, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1644), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 9, 1, null, null },
                    { 7, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1642), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 7, 1, null, null },
                    { 6, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1641), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 6, 1, null, null },
                    { 5, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1640), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 5, 1, null, null },
                    { 4, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1639), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 4, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "ButtonId", "CreateDate", "CreateId", "ProgramId", "RoleId", "UpdateDate", "UpdateId" },
                values: new object[,]
                {
                    { 3, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1638), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 3, 1, null, null },
                    { 2, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1636), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 2, 1, null, null },
                    { 1, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1468), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 1, 1, null, null },
                    { 8, null, new DateTime(2021, 6, 1, 7, 53, 13, 624, DateTimeKind.Utc).AddTicks(1643), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 8, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "ProgramRole",
                columns: new[] { "Id", "CreateDate", "CreateId", "RoleName", "Status", "UpdateDate", "UpdateId" },
                values: new object[] { 1, new DateTime(2021, 6, 1, 7, 53, 13, 623, DateTimeKind.Utc).AddTicks(5448), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", "admin", 1, null, null });

            migrationBuilder.InsertData(
                table: "ProgramUserRole",
                columns: new[] { "Id", "CreateDate", "CreateId", "ProgramRoleId", "ProgramUserId", "RoleId", "UpdateDate", "UpdateId", "UserId" },
                values: new object[] { 1, new DateTime(2021, 6, 1, 7, 53, 13, 623, DateTimeKind.Utc).AddTicks(6577), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", null, null, 1, null, null, 1 });

            migrationBuilder.InsertData(
                table: "ProgramUser",
                columns: new[] { "Id", "Account", "AccountType", "AspNetUserId", "CompanyId", "CreateDate", "CreateId", "CustomerId", "Email", "Status", "UpdateDate", "UpdateId", "UserName" },
                values: new object[] { 1, "admin", 1, "8e6ab833-52e1-49dc-942a-d7d5c0f40968", 1, new DateTime(2021, 6, 1, 7, 53, 13, 623, DateTimeKind.Utc).AddTicks(4186), "8e6ab833-52e1-49dc-942a-d7d5c0f40968", null, "jonathon0418@gmail.com", 1, null, null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountPayableDetail_AccountPayableId",
                table: "AccountPayableDetail",
                column: "AccountPayableId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccount_CompanyId",
                table: "CompanyBankAccount",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProgramButton_CompanyProgramId",
                table: "CompanyProgramButton",
                column: "CompanyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CompanyId",
                table: "Customer",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankAccount_CustomerId",
                table: "CustomerBankAccount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUser_CompanyId",
                table: "ProgramUser",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUserRole_ProgramRoleId",
                table: "ProgramUserRole",
                column: "ProgramRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUserRole_ProgramUserId",
                table: "ProgramUserRole",
                column: "ProgramUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPayableDetail");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "CompanyBankAccount");

            migrationBuilder.DropTable(
                name: "CompanyProgramButton");

            migrationBuilder.DropTable(
                name: "CustomerBankAccount");

            migrationBuilder.DropTable(
                name: "FormNoCount");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "PGParameter");

            migrationBuilder.DropTable(
                name: "ProgramUserRole");

            migrationBuilder.DropTable(
                name: "AccountPayable");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CompanyProgram");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "ProgramRole");

            migrationBuilder.DropTable(
                name: "ProgramUser");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
