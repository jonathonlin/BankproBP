import { ApEditComponent } from './components/ap/ap-edit/ap-edit.component';
import { ApListComponent } from './components/ap/ap-list/ap-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';

const routes: Routes = [
  { path: 'ap', component: ApListComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'payable/ap' }},
  { path: 'ap/add', component: ApEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'payable/ap' }},
  { path: 'ap/edit/:id', component: ApEditComponent, canActivate:[AuthGuard], pathMatch: 'full', data: { flag: 'payable/ap' }},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PayableRoutingModule { }
