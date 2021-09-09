import { ProgramRoleService, ProgramUserService, ProgramUserRoleService, CompanyProgramButtonService, PermissionService, CompanyProgramService } from './../../services/api.client.generated';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SysRoutingModule } from './sys-routing.module';
import { CompaniesService, CustomerService } from 'src/app/services/api.client.generated';
import { RegisterListComponent } from './components/register/register-list/register-list.component';
import { RegisterEditComponent } from './components/register/register-edit/register-edit.component';
import { RoleListComponent } from './components/role/role-list/role-list.component';
import { RoleEditComponent } from './components/role/role-edit/role-edit.component';
import { ProgramListComponent } from './components/program/program-list/program-list.component';
import { ProgramEditComponent } from './components/program/program-edit/program-edit.component';
import { UserRoleListComponent } from './components/user-role/user-role-list/user-role-list.component';
import { UserRoleEditComponent } from './components/user-role/user-role-edit/user-role-edit.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { ProgramSettingComponent } from './components/program-setting/program-setting.component';
import { ProgramButtonListComponent } from './components/program-button/program-button-list/program-button-list.component';
import { ProgramButtonEditComponent } from './components/program-button/program-button-edit/program-button-edit.component';


@NgModule({
  declarations: [
    RegisterListComponent, 
    RegisterEditComponent, 
    RoleListComponent, 
    RoleEditComponent, 
    ProgramListComponent, 
    ProgramEditComponent, 
    UserRoleListComponent, 
    UserRoleEditComponent, 
    ChangePasswordComponent, 
    ProgramSettingComponent, 
    ProgramButtonListComponent, 
    ProgramButtonEditComponent],
  imports: [
    CommonModule,
    SysRoutingModule,
    SharedModule,
  ],
  providers:[
    CompaniesService,    
    ProgramUserService,    
    ProgramRoleService,
    ProgramUserRoleService,
    CompanyProgramButtonService,
    PermissionService,
    CompanyProgramService,
    CustomerService,
  ]
})
export class SysModule { }
