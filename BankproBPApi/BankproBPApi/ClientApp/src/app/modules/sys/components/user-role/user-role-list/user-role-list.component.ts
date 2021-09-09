import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationResponseOfProgramRoleReadDTO, PaginationResponseOfProgramUserReadDTO, PaginationResponseOfProgramUserRole, ProgramRoleService, ProgramUserRoleService, ProgramUserService } from 'src/app/services/api.client.generated';
import { DialogService } from 'src/app/services/dialog.service';
import { TableComponent } from 'src/app/shared/components/table/table.component';

@Component({
  selector: 'app-user-role-list',
  templateUrl: './user-role-list.component.html',
  styleUrls: ['./user-role-list.component.css']
})
export class UserRoleListComponent implements OnInit {
  title: string = "使用者角色維護";
  queryForm!: FormGroup;
  roleOptions: {key: any, value?: string}[] = [];
  userOptions: {key: any, value?: string}[] = [];

  @ViewChild(TableComponent) table?: TableComponent;
  sort?: Sort;
  dataSource: any = null; 
  displayedColumns: {key:string, name:string}[] = [
    { key:'userId', name:'使用者名稱'},
    { key:'roleId', name:'角色'},   
    { key:'actions', name: ''}
  ]

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private programUserRoleService: ProgramUserRoleService,
    private dialogService: DialogService,
    private programUserService: ProgramUserService,
    private programRoleService: ProgramRoleService
  ) { }

  ngOnInit(): void {
    this.queryForm = this.fb.group({
      userId: ['', null],
      roleId: ['', null]	    
    });


    forkJoin(
      [this.programUserService.getProgramUsers(undefined,undefined,undefined,undefined,undefined,undefined)
      .pipe(
        map((users: PaginationResponseOfProgramUserReadDTO) => {
          let data = users.data?.map(m => ({key: m.id || undefined, value: m.userName}));
          data?.unshift({ key: undefined, value: '請選擇'});
          return (data);
        })
      ),
      this.programRoleService.getAll(undefined,undefined,undefined,undefined,undefined,undefined)    
      .pipe(
        map((roles: PaginationResponseOfProgramRoleReadDTO) => {
          let data = roles.data?.map(m => ({key: m.id || undefined, value: m.roleName}));
          data?.unshift({ key: undefined, value: '請選擇'});
          return (data);
        })
      )]
    ).pipe(
      map(([a, b]) => {return { a, b }})
    ).subscribe(res => {
      this.userOptions = res.a??[];
      this.roleOptions = res.b??[];
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
    const {userId, roleId} = this.queryForm.value;
    this.programUserRoleService.getAll(userId, roleId, page, pageSize, sortField, sortDirection)
      .pipe(        
        map((m: PaginationResponseOfProgramUserRole) => (
          {...m, 
            data: m.data?.map(m=>(
              {...m, 
                userId: this.userOptions.find(f=>f.key === m.userId)?.value, 
                roleId: this.roleOptions.find(f=>f.key === m.roleId)?.value 
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
    this.router.navigate(['sys/userrole/edit',id])
  }

  onDelete(id: number){
    this.dialogService.confirm("刪除","確定要刪除嗎？")
    .subscribe(result => {
      if(result===true){
        this.programUserRoleService.delete(id).subscribe(x=>{
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
