import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { CompaniesService, PaginationResponseOfCompanyReadDTO } from 'src/app/services/api.client.generated';
import { DialogService } from 'src/app/services/dialog.service';
import { TableComponent } from 'src/app/shared/components/table/table.component';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css']
})
export class CompanyListComponent implements OnInit {
  title: string = '公司維護';
  queryForm!: FormGroup;

  @ViewChild(TableComponent) table: TableComponent;
  sort?: Sort;
  dataSource: any = null; 
  displayedColumns: {key:string, name:string}[] = [
    { key:'companyName', name:'公司名稱'},
    { key:'tel', name:'電話'},
    { key:'email', name:'Email'},
    { key:'zipCode', name:'郵遞區號'},
    { key:'address', name:'地址'},
    { key:'actions', name: ''}
  ]
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private companiesService: CompaniesService,
    private dialogService: DialogService,   
  ) { }

  ngOnInit(): void {
    this.queryForm = this.fb.group({
      companyName: ['', null],
      tel: ['', null],
	    email: ['', null],
		  zipCode: ['', null],
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
    let companyName = this.queryForm.get('companyName')?.value;
    let tel = this.queryForm.get('tel')?.value;
    let email = this.queryForm.get('email')?.value;
    let zipCode = this.queryForm.get('zipCode')?.value;   
    this.companiesService.getAll(
      companyName, 
      tel,
      email,
      zipCode,
      page, 
      pageSize, 
      sortField, 
      sortDirection)
      .pipe(        
        map((m: PaginationResponseOfCompanyReadDTO) => this.dataSource = m)
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
    this.router.navigate(['basic/company/edit',id])
  }

  onDelete(id: number){
    this.dialogService.confirm('刪除公司','確定要刪除嗎？')
    .subscribe(result => {
      if(result===true){
        this.companiesService.delete(id).subscribe(x=>{
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
