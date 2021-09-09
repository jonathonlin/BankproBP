import { BankService, PaginationResponseOfBankReadDTO } from './../../../../../services/api.client.generated';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { DialogService } from 'src/app/services/dialog.service';
import { TableComponent } from 'src/app/shared/components/table/table.component';
import { map } from 'rxjs/operators';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-bank-list',
  templateUrl: './bank-list.component.html',
  styleUrls: ['./bank-list.component.css']
})
export class BankListComponent implements OnInit {
  title: string = '銀行維護';
  queryForm!: FormGroup;  

  @ViewChild(TableComponent) table: TableComponent;
  sort?: Sort;
  dataSource: any = null; 
  displayedColumns: {key:string, name:string}[] = [
    { key:'bankCode', name:'銀行代號'},
    { key:'bankName', name:'銀行名稱'},    
    { key:'actions', name: ''}
  ]
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private bankService: BankService,
    private dialogService: DialogService,   
  ) { }

  ngOnInit(): void {
    this.queryForm = this.fb.group({
      bankCode: ['', null],      
      bankName: ['', null]
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
    const {bankCode, bankName} = this.queryForm.value;
    this.bankService.getAll(
      bankCode, 
      bankName,
      page, 
      pageSize, 
      sortField, 
      sortDirection)
      .pipe(        
        map((m: PaginationResponseOfBankReadDTO) => this.dataSource = m)
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
    this.router.navigate(['basic/bank/edit',id])
  }

  onDelete(id: number){
    this.dialogService.confirm('刪除','確定要刪除嗎？')
    .subscribe(result => {
      if(result===true){
        this.bankService.delete(id).subscribe(x=>{
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
