using AutoMapper;
using BankproBPData;
using BankproBPData.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankproBPApi.Profiles
{
	public class BankproProfile: Profile
	{
		public BankproProfile()
		{
			//source => target
			CreateMap<CompanyProgram, CompanyProgramDTO>();
			CreateMap<CompanyProgramDTO, CompanyProgram>();
			CreateMap<CompanyProgram, CompanyProgramReadDTO>();			
			CreateMap<Company, CompanyDTO>();
			CreateMap<CompanyDTO, Company>();			
			CreateMap<Company, CompanyReadDTO>();
			CreateMap<ProgramUser, ProgramUserDTO>();
			CreateMap<ProgramUserDTO, ProgramUser>();
			CreateMap<ProgramUser, ProgramUserReadDTO>()
				.ForMember(a => a.Company, b => b.MapFrom(x => x.Company));
			CreateMap<ProgramRole, ProgramRoleDTO>();
			CreateMap<ProgramRoleDTO, ProgramRole>();
			CreateMap<ProgramRole, ProgramRoleReadDTO>();
			CreateMap<ProgramUserRole, ProgramUserRoleDTO>();
			CreateMap<ProgramUserRoleDTO,ProgramUserRole>();
			CreateMap<CompanyProgramButton, CompanyProgramButtonReadDTO>();
			CreateMap<CompanyProgramButton, CompanyProgramButtonDTO>();
			CreateMap<CompanyProgramButtonDTO, CompanyProgramButton>();
			CreateMap<Customer, CustomerReadDTO>();
			CreateMap<Customer, CustomerDTO>();
			CreateMap<CustomerDTO, Customer>();
			CreateMap<CompanyBankAccount, CompanyBankAccountReadDTO>();
			CreateMap<CompanyBankAccount, CompanyBankAccountDTO>();
			CreateMap<CompanyBankAccountDTO, CompanyBankAccount>();
			CreateMap<CustomerBankAccount, CustomerBankAccountReadDTO>();
			CreateMap<CustomerBankAccount, CustomerBankAccountDTO>();
			CreateMap<CustomerBankAccountDTO, CustomerBankAccount>();
			//CreateMap<Bank, BankReadDTO>();
			//CreateMap<Bank, BankDTO>();
			//CreateMap<BankDTO, Bank>();
			CreateMap<AccountPayable, AccountPayableReadDTO>();
			CreateMap<AccountPayable, AccountPayableDTO>();
			CreateMap<AccountPayableDTO, AccountPayable>();
			CreateMap<AccountPayableDetailDTO, AccountPayableDetail>();
			CreateMap<AccountPayableDetail, AccountPayableDetailDTO>();
		}
	}
}
