import { BankEdtiComponent } from './components/bank/bank-edti/bank-edti.component';
import { BankListComponent } from './components/bank/bank-list/bank-list.component';
import { CompanyEditComponent } from './components/company/company-edit/company-edit.component';
import { CompanyListComponent } from './components/company/company-list/company-list.component';
import { CustomerBankEditComponent } from './components/customer-bank/customer-bank-edit/customer-bank-edit.component';
import { CustomerBankListComponent } from './components/customer-bank/customer-bank-list/customer-bank-list.component';
import { CustomerEditComponent } from './components/customer/customer-edit/customer-edit.component';
import { CustomerListComponent } from './components/customer/customer-list/customer-list.component';
import { CompanyBankEditComponent } from './components/company-bank/company-bank-edit/company-bank-edit.component';
import { CompanyBankListComponent } from './components/company-bank/company-bank-list/company-bank-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';

const routes: Routes = [
  { path: 'bank', component: BankListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/bank' }},
  { path: 'bank/add', component: BankEdtiComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/bank' }},
  { path: 'bank/edit/:id', component: BankEdtiComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/bank' }},
  { path: 'company', component: CompanyListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/company' }},
  { path: 'company/add', component: CompanyEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/company' }},
  { path: 'company/edit/:id', component: CompanyEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/company' }},
  { path: 'companybank', component: CompanyBankListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/companybank' }},
  { path: 'companybank/add', component: CompanyBankEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/companybank' }},
  { path: 'companybank/edit/:id', component: CompanyBankEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/companybank' }},
  { path: 'customer', component: CustomerListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/customer' }},
  { path: 'customer/add', component: CustomerEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/customer' }},
  { path: 'customer/edit/:id', component: CustomerEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/customer' }},
  { path: 'customerbank', component: CustomerBankListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/customerbank' }},
  { path: 'customerbank/add', component: CustomerBankEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/customerbank' }},
  { path: 'customerbank/edit/:id', component: CustomerBankEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'basic/customerbank' }},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasicRoutingModule { }
