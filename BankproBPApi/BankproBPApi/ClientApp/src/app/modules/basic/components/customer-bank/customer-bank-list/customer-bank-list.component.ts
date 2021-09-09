import { CustomerBankAccountService, PaginationResponseOfCustomerBankAccountReadDTO } from './../../../../../services/api.client.generated';
import { CustomerService } from 'src/app/services/api.client.generated';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { DialogService } from 'src/app/services/dialog.service';
import { TableComponent } from 'src/app/shared/components/table/table.component';

@Component({
  selector: 'app-customer-bank-list',
  templateUrl: './customer-bank-list.component.html',
  styleUrls: ['./customer-bank-list.component.css']
})
export class CustomerBankListComponent implements OnInit {
  title: string = "客戶銀行帳戶維護";
  queryForm!: FormGroup;
  customerOptions: {key: any, value?: string}[] = [];

  @ViewChild(TableComponent) table?: TableComponent;
  sort?: Sort;
  dataSource: any = null; 
  displayedColumns: {key:string, name:string}[] = [
    { key:'bankCode', name:'銀行代號'},
    { key:'bankAccount', name:'銀行帳號'},
    { key:'bankAccountName', name:'銀行帳號名稱'},
    { key:'allowDiffAmount', name:'虛擬帳號代碼'},
    { key:'customerId', name:'公司'},
    { key:'actions', name: ''}
  ]

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private customerService: CustomerService,
    private customerBankAccountService: CustomerBankAccountService,
    private dialogService: DialogService
  ) {     
  }

  ngOnInit(): void {
    this.queryForm = this.fb.group({
      bankCode: ['', null],
      customerId: ['', null]
    });
    this.customerService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined)
    .pipe(
      map(m => {
        const data = m.data.map(x=>({ key: x.id, value: x.customerName }));
        this.customerOptions = data;
      })
    ).subscribe();
    this.initialQuery();
  }

  initialQuery(){
    let page = this.table?.paginator?.pageIndex || 1;
    let size = this.table?.paginator?.pageSize || 10;   
    this.table?.paginator?.firstPage();
    this.table?.matSort?.sort({id:'', start: 'desc', disableClear: false})
    this.query(page,size,undefined,undefined);    
  }

  query(page: any, pageSize: any, sortField: any, sortDirection: any){    
    const {bankCode, customerId} = this.queryForm.value;
    this.customerBankAccountService.getAll(bankCode, customerId, page, pageSize, sortField, sortDirection)
      .pipe(        
        map((m: PaginationResponseOfCustomerBankAccountReadDTO) => (
          {...m, 
            data: m.data?.map(m=>(
              {...m, 
                customerId: this.customerOptions.find(f=>f.key === m.customerId)?.value                
              }))
          }))
      )
      .subscribe(res => this.dataSource = res);
  }

  onPaginateChange(event: PageEvent){
    let page = event.pageIndex;
    let size = event.pageSize;
    page = page +1;        
    this.query(page,size,this.sort?.active,this.sort?.direction); 
  }

  onEdit(id: number){
    this.router.navigate(['basic/customerbank/edit',id])
  }

  onDelete(id: number){
    this.dialogService.confirm("刪除","確定要刪除嗎？")
    .subscribe(result => {
      if(result===true){
        this.customerBankAccountService.delete(id).subscribe(x=>{
          if(!x.isOk){
            this.dialogService.alert("", x.message);
          }else{
            this.initialQuery();
          }
        });
      }
    });
  }

  onSortData(event: Sort){
    this.sort = event;
    let page = 1
    let size = this.table?.paginator?.pageSize || 10;
    this.table?.paginator?.firstPage();
    this.query(page,size,this.sort?.active,this.sort?.direction);   
  }


}
