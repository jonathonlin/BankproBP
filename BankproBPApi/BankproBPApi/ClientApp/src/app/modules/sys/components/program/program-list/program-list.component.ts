import { DialogService } from 'src/app/services/dialog.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { CompanyProgramReadDTO, CompanyProgramService, PaginationResponseOfCompanyProgramReadDTO } from 'src/app/services/api.client.generated';
import { TableComponent } from 'src/app/shared/components/table/table.component';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-program-list',
  templateUrl: './program-list.component.html',
  styleUrls: ['./program-list.component.css']
})
export class ProgramListComponent implements OnInit {
  queryForm!: FormGroup;
  statusCodes: {name:string, value:number | null}[] = [
    {name: '請選擇',  value: null},
    {name: '啟用', value: 1},
    {name: '停用', value: 0},
  ];
  
  programTypes: {name:string, value:number | null}[] = [
    {name: '請選擇',  value: null},
    {name:'目錄', value: 0},
    {name:'程式', value: 1},
  ]
  
  companyProgramReadDTOs: CompanyProgramReadDTO[] = [];
  
  pageEvent?: PageEvent;
  dataSource: any = null;  
  displayedColumns: {key:string, name:string}[] = [
    { key:'programName', name:'程式名稱'},
    { key:'parentId', name:'父目錄'},
    { key:'programType', name:'型態'},
    { key:'programUrl', name:'程式路徑'},
    { key:'status', name:'狀態'},
    { key:'sort', name:'排序'},
    { key:'actions', name: ''}
  ]
 
  sort?: Sort;
 
  @ViewChild(TableComponent) table?: TableComponent;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private companyProgramService: CompanyProgramService,
    private dialogService: DialogService
  ) { }

  ngOnInit(): void {
    this.queryForm = this.fb.group({
      programName: ['', null],
      programType: ['', null],
      status: ['', null]     
    });
    

    this.companyProgramService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined)
      .pipe(
        map(data => this.companyProgramReadDTOs = data.data??[])
      ).subscribe();

    this.initialQuery();
  }

  initialQuery(){     
    let page = this.table?.paginator?.pageIndex || 1;
    let size = this.table?.paginator?.pageSize || 10;   
    this.table?.paginator?.firstPage();
    this.table?.matSort?.sort( { id: '', start: 'desc', disableClear: false})
    this.query(page,size,undefined,undefined);    
  }

  query(page:any, pageSize:any, sortField:any, sortDirection:any){   
    const { programName, programType, status } = this.queryForm.value;    
    this.companyProgramService.getAll(programName,programType,status,page,pageSize,sortField,sortDirection)
    .pipe(      
      map((programs: PaginationResponseOfCompanyProgramReadDTO) => {
        let data = programs.data?.map(m=> ({...m, 
          parentId: this.companyProgramReadDTOs.find(f=>f.id === m.parentId)?.programName,
          programType: this.programTypes.find(f=>f.value===m.programType)?.name,
          status: this.statusCodes.find(f=>f.value === m.status)?.name
         }))
        return ({...programs, data: data });
      })
    ).subscribe(res => this.dataSource = res)
  }

  onPaginateChange(event: PageEvent){    
    let page = event.pageIndex;
    let size = event.pageSize;
    page = page +1;        
    this.query(page,size,this.sort?.active,this.sort?.direction);
  }

  onSortData(event:Sort){
    this.sort = event;
    let page = 1
    let size = this.table?.paginator?.pageSize || 10;
    this.table?.paginator?.firstPage();
    this.query(page,size,this.sort?.active,this.sort?.direction);
  }

  onDelete(id: number){
    this.dialogService.confirm('刪除程式','確定要刪除嗎？')
    .subscribe(result => {
      if(result===true){
        this.companyProgramService.delete(id).subscribe(x=>{
          if(!x.isOk){
            this.dialogService.alert("", x.message);
          } else {
            this.initialQuery();
          }
        });
      }
    });     
  }

  onEdit(id: number){
    this.router.navigate(['sys/program/edit', id]);
  }
}
