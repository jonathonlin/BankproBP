import { ProgramButtonListComponent } from './components/program-button/program-button-list/program-button-list.component';
import { ProgramSettingComponent } from './components/program-setting/program-setting.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';

import { RegisterListComponent } from './components/register/register-list/register-list.component';
import { AuthGuard } from './../../guards/auth.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterEditComponent } from './components/register/register-edit/register-edit.component';
import { RoleEditComponent } from './components/role/role-edit/role-edit.component';
import { RoleListComponent } from './components/role/role-list/role-list.component';
import { ProgramListComponent } from './components/program/program-list/program-list.component';
import { ProgramEditComponent } from './components/program/program-edit/program-edit.component';
import { UserRoleListComponent } from './components/user-role/user-role-list/user-role-list.component';
import { UserRoleEditComponent } from './components/user-role/user-role-edit/user-role-edit.component';
import { ProgramButtonEditComponent } from './components/program-button/program-button-edit/program-button-edit.component';

const routes: Routes = [
  { path: 'programbutton', component: ProgramButtonListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/programbutton' }},
  { path: 'programbutton/add', component: ProgramButtonEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/programbutton' }},
  { path: 'programbutton/edit/:id', component: ProgramButtonEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/programbutton' }},
  { path: 'programsetting', component: ProgramSettingComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/programsetting' }},
  { path: 'changepassword', component: ChangePasswordComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/programsetting' }},
  { path: 'userrole', component: UserRoleListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/userrole' }},
  { path: 'userrole/add', component: UserRoleEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/userrole' }},
  { path: 'userrole/edit/:id', component: UserRoleEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/userrole' }},
  { path: 'program', component: ProgramListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/program' }},
  { path: 'program/add', component: ProgramEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/program' }},
  { path: 'program/edit/:id', component: ProgramEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/program' }},
  { path: 'role', component: RoleListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/role' }},
  { path: 'role/add', component: RoleEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/role' }},
  { path: 'role/edit/:id', component: RoleEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/role' }},
  { path: 'register', component: RegisterListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/register' }},
  { path: 'register/add', component: RegisterEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/register' }},
  { path: 'register/edit/:id', component: RegisterEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'sys/register' }},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SysRoutingModule { }
