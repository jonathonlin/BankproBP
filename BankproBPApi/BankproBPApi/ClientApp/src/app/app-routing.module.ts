import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path:'', loadChildren: () => import('./modules/home/home.module').then(r=>r.HomeModule) },
  { path:'sys', loadChildren: () => import('./modules/sys/sys.module').then(r=>r.SysModule) },
  { path:'basic', loadChildren: () => import('./modules/basic/basic.module').then(r=>r.BasicModule) },
  { path:'payable', loadChildren: () => import('./modules/payable/payable.module').then(r=>r.PayableModule) },
  { path:'pagenotfound', component: PageNotFoundComponent },
  { path:'**', redirectTo: 'pagenotfound'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
