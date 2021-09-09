import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { PaginationResponseOfProgramUserReadDTO, ProgramUserService } from 'src/app/services/api.client.generated';
import { DialogService } from 'src/app/services/dialog.service';
import { TableComponent } from 'src/app/shared/components/table/table.component';

@Component({
  selector: 'app-register-list',
  templateUrl: './register-list.component.html',
  styleUrls: ['./register-list.component.css']
})
export class RegisterListComponent implements OnInit {
  title: string = "使用者維護";
  queryForm!: FormGroup;
 
  @ViewChild(TableComponent) table: TableComponent;
  sort?: Sort;
  dataSource: any = null; 
  displayedColumns: {key:string, name:string}[] = [
    { key:'account', name:'帳號'},
    { key:'userName', name:'使用者名稱'},
    { key:'email', name:'Email'},
    { key:'accountType', name: '帳戶類別' },
    { key:'companyName', name:'公司'},
    { key:'actions', name: ''}
  ]
  
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private programUserService: ProgramUserService,
    private dialogService: DialogService) { }
 
  ngOnInit(): void {
    this.queryForm = this.fb.group({
      username: ['', null],
      companyname: ['', null],
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
    let username = this.queryForm.get('username')?.value;
    let companyname = this.queryForm.get('companyname')?.value;
    this.programUserService.getProgramUsers(username,companyname,page,pageSize,sortField, sortDirection)
    .pipe(
      map((users: PaginationResponseOfProgramUserReadDTO) => {
        let data = users.data?.map(m => ({...m,
          companyName: m.company.companyName
        }));        
        return ({...users, data: data});
      })
    ).subscribe(res => this.dataSource = res);
  }

  onPaginateChange(event: PageEvent){
    let page = event.pageIndex;
    let size = event.pageSize;
    page = page +1;        
    this.query(page,size,this.sort?.active,this.sort?.direction); 
  }

  onEdit(id: number){
    this.router.navigate(['sys/register/edit',id])
  }

  onDelete(id: number){
    this.dialogService.confirm('刪除使用者','確定要刪除嗎？')
    .subscribe(result => {
      if(result===true){
        this.programUserService.delete(id).subscribe(x=>{
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
