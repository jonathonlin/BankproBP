import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { PaginationResponseOfProgramRoleReadDTO, ProgramRoleService } from 'src/app/services/api.client.generated';
import { DialogService } from 'src/app/services/dialog.service';
import { TableComponent } from 'src/app/shared/components/table/table.component';


@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css']
})
export class RoleListComponent implements OnInit {
  title: string = "角色維護";
  queryForm!: FormGroup;
  
  statusCodes: {name:string, value:number | null}[] = [
    {name: '請選擇',  value: null},
    {name: '啟用', value: 1},
    {name: '停用', value: 0},
  ];

  @ViewChild(TableComponent) table?: TableComponent;
  sort?: Sort;
  dataSource: any = null; 
  displayedColumns: {key:string, name:string}[] = [
    { key:'roleName', name:'角色名稱'},
    { key:'status', name:'狀態'},
    { key:'actions', name:'' }
  ]

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private programRoleService: ProgramRoleService,
    private dialogService: DialogService) { }

  ngOnInit(): void {
    this.queryForm = this.fb.group({
      roleName: ['', null],
      status: ['', null]
    });
    this.initialQuery();
  }

  initialQuery(){
    let page = 1;
    let size = this.table?.paginator?.pageSize || 10;
    this.table?.paginator?.firstPage();
    this.query(page, size, undefined, undefined);
  }

  query(page: any, pageSize:any, sortField:any, sortDirection:any){
    let roleName = this.queryForm.get('roleName')?.value;
    let status = this.queryForm.get('status')?.value;
    this.programRoleService.getAll(roleName, status, page, pageSize, sortField, sortDirection)
      .pipe(
        map((roles: PaginationResponseOfProgramRoleReadDTO) => {
          let data = roles.data?.map(m=> ({...m,           
            status: this.statusCodes.find(f=>f.value === m.status)?.name
           }))          
          return ({...roles, data: data })
        })
      )
      .subscribe(x=> this.dataSource = x);
  }

  onPaginateChange(event: PageEvent){
    let page = event.pageIndex;
    let size = event.pageSize;
    page = page +1;        
    this.query(  
      page,
      size,
      this.sort?.active,
      this.sort?.direction); 
  }

  onEdit(id: number){
    this.router.navigate(['sys/role/edit',id])
  }

  onDelete(id: number){
    this.dialogService.confirm('刪除角色', '確定要刪除嗎?')
        .subscribe(result => {
          if(result===true){
            this.programRoleService.delete(id).subscribe(x=>{
              if(!x.isOk){
                this.dialogService.alert("刪除失敗",  x.message);
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
    this.query(
      page,
      size,
      this.sort?.active,
      this.sort?.direction);   
  }


}
