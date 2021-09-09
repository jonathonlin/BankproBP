import { CustomerService, PaginationResponseOfCustomerReadDTO } from './../../../../../services/api.client.generated';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { CompaniesService } from 'src/app/services/api.client.generated';
import { DialogService } from 'src/app/services/dialog.service';
import { TableComponent } from 'src/app/shared/components/table/table.component';
import { map } from 'rxjs/operators';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  title: string = '客戶維護';
  queryForm!: FormGroup;
  companyOptions: {key: any, value?: string}[] = [];

  @ViewChild(TableComponent) table: TableComponent;
  sort?: Sort;
  dataSource: any = null; 
  displayedColumns: {key:string, name:string}[] = [
    { key:'customerName', name:'客戶名稱'},
    { key:'tel', name:'電話'},
    { key:'mobile', name:'手機'},
    { key:'email', name:'Email'},
    { key:'zipCode', name:'郵遞區號'},
    { key:'address', name:'地址'},
    { key:'companyId', name:'所屬公司'},
    { key:'actions', name: ''}
  ]
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private companiesService: CompaniesService,
    private customerService: CustomerService,
    private dialogService: DialogService,   
  ) { }

  ngOnInit(): void {
    this.queryForm = this.fb.group({
      customerName: ['', null],
      tel: ['', null],
      mobile: ['', null],
      email: ['', null],
      zipCode:['', null],
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
    const {customerName,tel, mobile, email,zipCode, companyId } = this.queryForm.value;
    this.customerService.getAll(
      customerName, 
      tel,
      mobile,
      email,
      zipCode,
      companyId,
      page, 
      pageSize, 
      sortField, 
      sortDirection)
      .pipe(        
        map((m: PaginationResponseOfCustomerReadDTO) => ({
          ...m,
          data: m.data.map(x=>({
            ...x,
            companyId: this.companyOptions.find(f=>f.key === x.companyId)?.value
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
    this.router.navigate(['basic/customer/edit',id])
  }

  onDelete(id: number){
    this.dialogService.confirm('刪除','確定要刪除嗎？')
    .subscribe(result => {
      if(result===true){
        this.customerService.delete(id).subscribe(x=>{
          if(!x.isOk){
            this.dialogService.alert("", x.message);
          } else {
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
