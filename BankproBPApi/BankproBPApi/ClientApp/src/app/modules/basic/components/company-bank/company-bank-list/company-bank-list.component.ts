import { DialogService } from './../../../../../services/dialog.service';
import { Router } from '@angular/router';
import { CompanyBankAccountService, PaginationResponseOfCompanyBankAccountReadDTO } from './../../../../../services/api.client.generated';
import { CompaniesService } from 'src/app/services/api.client.generated';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { map } from 'rxjs/operators';
import { TableComponent } from 'src/app/shared/components/table/table.component';
import { Sort } from '@angular/material/sort';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-company-bank-list',
  templateUrl: './company-bank-list.component.html',
  styleUrls: ['./company-bank-list.component.css']
})
export class CompanyBankListComponent implements OnInit {
  title: string = "公司銀行帳戶維護";
  queryForm!: FormGroup;
  companyOptions: {key: any, value?: string}[] = [];

  @ViewChild(TableComponent) table?: TableComponent;
  sort?: Sort;
  dataSource: any = null; 
  displayedColumns: {key:string, name:string}[] = [
    { key:'bankCode', name:'銀行代號'},
    { key:'bankAccount', name:'銀行帳號'},
    { key:'bankAccountName', name:'銀行帳號名稱'},
    { key:'companyBankAtmId', name:'虛擬帳號代碼'},
    { key:'companyId', name:'公司'},
    { key:'actions', name: ''}
  ]

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private companiesService: CompaniesService,
    private companyBankAccountService: CompanyBankAccountService,
    private dialogService: DialogService
  ) {     
  }

  ngOnInit(): void {
    this.queryForm = this.fb.group({
      bankCode: ['', null],
      companyId: ['', null]
    });
    this.companiesService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined)
    .pipe(
      map(m => {
        const data = m.data.map(x=>({ key: x.id, value: x.companyName }));
        this.companyOptions = data;
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
    const {bankCode, companyId} = this.queryForm.value;
    this.companyBankAccountService.getAll(bankCode, companyId, page, pageSize, sortField, sortDirection)
      .pipe(        
        map((m: PaginationResponseOfCompanyBankAccountReadDTO) => (
          {...m, 
            data: m.data?.map(m=>(
              {...m, 
                companyId: this.companyOptions.find(f=>f.key === m.companyId)?.value                
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
    this.router.navigate(['basic/companybank/edit',id])
  }

  onDelete(id: number){
    this.dialogService.confirm("刪除","確定要刪除嗎？")
    .subscribe(result => {
      if(result===true){
        this.companyBankAccountService.delete(id).subscribe(x=>{
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
