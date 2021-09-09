import { AuthenticateService } from './../../services/api.client.generated';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';


@NgModule({
  declarations: [HomeComponent, LoginComponent],
  imports: [    
    HomeRoutingModule,
    SharedModule,
  ],
  providers:[AuthenticateService]
})
export class HomeModule { }
