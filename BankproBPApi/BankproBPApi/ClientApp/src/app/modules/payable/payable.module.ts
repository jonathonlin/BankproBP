import { AccountPayableService, CustomerService, PGParameterService } from './../../services/api.client.generated';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PayableRoutingModule } from './payable-routing.module';
import { ApListComponent } from './components/ap/ap-list/ap-list.component';
import { ApEditComponent } from './components/ap/ap-edit/ap-edit.component';


@NgModule({
  declarations: [ApListComponent, ApEditComponent],
  imports: [
    CommonModule,
    PayableRoutingModule,
    SharedModule,    
  ],
  providers: [
    AccountPayableService,
    CustomerService,
    PGParameterService
  ]
})
export class PayableModule { }
