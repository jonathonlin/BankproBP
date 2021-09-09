import { CompanyProgramService, PaginationResponseOfCompanyProgramReadDTO } from 'src/app/services/api.client.generated';
import { CompanyProgramButtonService, PaginationResponseOfCompanyProgramButtonReadDTO } from './../../../../../services/api.client.generated';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { DialogService } from 'src/app/services/dialog.service';
import { TableComponent } from 'src/app/shared/components/table/table.component';
import { map } from 'rxjs/operators';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-program-button-list',
  templateUrl: './program-button-list.component.html',
  styleUrls: ['./program-button-list.component.css']
})
export class ProgramButtonListComponent implements OnInit {
  queryForm!: FormGroup;
  statusCodes: {name:string, value:number | null}[] = [
    {name: '請選擇',  value: null},
    {name: '啟用', value: 1},
    {name: '停用', value: 0},
  ];

  programOptions: {name:string, value: any}[];

  @ViewChild(TableComponent) table: TableComponent;
  sort?: Sort;
  dataSource: any = null; 
  displayedColumns: {key:string, name:string}[] = [
    { key:'buttonText', name:'文字'},
    { key:'buttonAction', name:'動作'},
    { key:'programId', name:'程式'},
    { key:'status', name:'狀態'},
    { key:'sort', name:'排序'},
    { key:'actions', name:''},    
  ]
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private companyProgramButtonService: CompanyProgramButtonService,
    private companyProgramService: CompanyProgramService,
    private dialogService: DialogService
  ) { 
  }

  ngOnInit(): void {
    this.queryForm = this.fb.group({
      buttonText: ['', null],
      programId: ['', null],
	    status: ['', null],
    });

    this.companyProgramService.getAll(undefined, 1,undefined,undefined,undefined,undefined,undefined)
        .pipe(
          map((m: PaginationResponseOfCompanyProgramReadDTO)=> {
        let data = m.data.map(x => ({ name: x.programName, value: x.id }));
        data.unshift({name: '請選擇', value: undefined});
        this.programOptions = data;
      })        
    ).subscribe(res=>{
      this.initialQuery();
    });
  }

  initialQuery(){
    let page = this.table?.paginator?.pageIndex || 1;
    let size = this.table?.paginator?.pageSize || 10;   
    this.table?.paginator?.firstPage();
    this.table?.matSort?.sort({id:'', start: 'desc', disableClear: false})
    this.query(page,size,undefined,undefined);    
  }

  query(page: any, pageSize: any, sortField: any, sortDirection: any){
    let buttonText = this.queryForm.get('buttonText')?.value;
    let programId = this.queryForm.get('programId')?.value;
    let status = this.queryForm.get('status')?.value;    
    this.companyProgramButtonService.getAll(
      buttonText,
      programId,
      status,
      page, 
      pageSize, 
      sortField, 
      sortDirection)
      .pipe(        
        map((m: PaginationResponseOfCompanyProgramButtonReadDTO) => {
          const data = m.data.map(x=>({
            ...x,
            programId: this.programOptions.find(f=>f.value === x.programId).name,
            status: this.statusCodes.find(f=>f.value === x.status).name
          }));
          return ({...m, data: data});
        })
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
    this.router.navigate(['sys/programbutton/edit',id])
  }

  onDelete(id: number){
    this.dialogService.confirm('刪除程式按鈕','確定要刪除嗎？')
    .subscribe(result => {
      if(result===true){
        this.companyProgramButtonService.delete(id).subscribe(x=>{
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
