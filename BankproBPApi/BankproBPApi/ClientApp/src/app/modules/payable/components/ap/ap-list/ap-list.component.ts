import { CustomerService } from 'src/app/services/api.client.generated';
import { AccountPayableService, PaginationResponseOfAccountPayableReadDTO, PGParameterService } from './../../../../../services/api.client.generated';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { DialogService } from 'src/app/services/dialog.service';
import { TableComponent } from 'src/app/shared/components/table/table.component';
import { map } from 'rxjs/operators';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-ap-list',
  templateUrl: './ap-list.component.html',
  styleUrls: ['./ap-list.component.css']
})
export class ApListComponent implements OnInit {
  title: string = '應收立帳';
  queryForm!: FormGroup;
  customerOptions: {key:any, value:string}[];
  apStatusOptions: {key:any, value:string}[];

  @ViewChild(TableComponent) table: TableComponent;
  sort?: Sort;
  dataSource: any = null; 
  displayedColumns: {key:string, name:string, width?:string}[] = [
    { key:'customerId', name:'客戶', width: '10%'},
    { key:'yearMonth', name:'帳單年月', width: '8%'},
    { key:'apNo', name:'帳單編號', width: '12%'},
    { key:'invoiceNo', name:'發票號碼', width: '10%'},
    { key:'invoiceDate', name:'發票日期', width: '10%'},
    { key:'invoiceAmount', name:'發票金額', width: '10%'},
    { key:'apAmount', name:'帳單金額', width: '10%'},
    { key:'expireDate', name:'繳費期限', width: '10%'},
    { key:'apStatus', name:'帳單狀態', width: '10%'},
    { key:'actions', name: '', width: '10%'}
  ];
  
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private accountPayableService: AccountPayableService,
    private dialogService: DialogService,   
    private customerService: CustomerService,
    private pGParameterService: PGParameterService,
  ){ 

    forkJoin([
      this.customerService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined)
      .pipe(
        map(m=>{
          let data = m.data?.map(m => ({key: m.id || undefined, value: m.customerName}));
          data?.unshift({ key: undefined, value: '請選擇'});
          return (data);
        })
      ),
      
      this.pGParameterService.getAll('APStatus')
      .pipe(
        map(m=>{
          let data = m.map(m => ({key: m.keyValue || undefined, value: m.keyName}));
          data?.unshift({ key: undefined, value: '請選擇'});
          return (data);
        })
      )
    ]).pipe(
      map(([a, b]) => {return { a, b }})
    ).subscribe(res => {
      this.customerOptions = res.a;
      this.apStatusOptions = res.b;
    });
  }

  ngOnInit(): void {
    this.queryForm = this.fb.group({
      customerId: ['', null],      
      yearMonth: ['', null],
      apNo: ['', null],
      invoiceNo: ['', null],
      apStatus: ['', null]
    });

    
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
    const {customerId, yearMonth, apNo, invoiceNo, apStatus} = this.queryForm.value;
    this.accountPayableService.getAll(
      customerId, 
      yearMonth,
      apNo,
      invoiceNo, 
      apStatus,
      page, 
      pageSize, 
      sortField, 
      sortDirection)
      .pipe(        
        map((m: PaginationResponseOfAccountPayableReadDTO) => {          
          const tmpData = m.data.map(x=>(
            {...x, 
              customerId: this.customerOptions.find(f=>f.key === x.customerId)?.value,
              expireDate: x.expireDate.toString().substring(0,10),
              invoiceDate: x.invoiceDate.toString().substring(0,10),
              apStatus: this.apStatusOptions.find(f=>f.key === x.apStatus)?.value
            }));          
          const source = {...m, data: tmpData};         
          this.dataSource = source;
        })
      )
      .subscribe();
  }
  
  onPaginateChange(event: PageEvent){
    let page = event.pageIndex;
    let size = event.pageSize;
    page = page +1;        
    this.query(page,size,this.sort?.active,this.sort?.direction); 
  }

  onEdit(id: number){
    this.router.navigate(['payable/ap/edit',id])
  }

  onDelete(id: number){
    this.dialogService.confirm('刪除','確定要刪除嗎？')
    .subscribe(result => {
      if(result===true){
        this.accountPayableService.delete(id).subscribe(x=>{
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
