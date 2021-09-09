import { BankService } from './../../services/api.client.generated';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BasicRoutingModule } from './basic-routing.module';
import { CompanyBankListComponent } from './components/company-bank/company-bank-list/company-bank-list.component';
import { CompanyBankEditComponent } from './components/company-bank/company-bank-edit/company-bank-edit.component';
import { CustomerListComponent } from './components/customer/customer-list/customer-list.component';
import { CustomerEditComponent } from './components/customer/customer-edit/customer-edit.component';
import { CustomerBankListComponent } from './components/customer-bank/customer-bank-list/customer-bank-list.component';
import { CustomerBankEditComponent } from './components/customer-bank/customer-bank-edit/customer-bank-edit.component';
import { CompanyListComponent } from './components/company/company-list/company-list.component';
import { CompanyEditComponent } from './components/company/company-edit/company-edit.component';
import { CompaniesService, CompanyBankAccountService, CustomerBankAccountService, CustomerService } from 'src/app/services/api.client.generated';
import { BankListComponent } from './components/bank/bank-list/bank-list.component';
import { BankEdtiComponent } from './components/bank/bank-edti/bank-edti.component';


@NgModule({
  declarations: [CompanyBankListComponent, CompanyBankEditComponent, CustomerListComponent, CustomerEditComponent, CustomerBankListComponent, CustomerBankEditComponent, CompanyListComponent, CompanyEditComponent, BankListComponent, BankEdtiComponent],
  imports: [
    CommonModule,
    BasicRoutingModule,
    SharedModule,
  ],
  providers:[
    CompaniesService,
    CompanyBankAccountService,
    CustomerService,
    CustomerBankAccountService,
    BankService,
  ]
})
export class BasicModule { }
