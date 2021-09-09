import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { CompaniesService, CompanyDTO } from 'src/app/services/api.client.generated';
import { FormField } from 'src/app/shared/dynamic/form-field';

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrls: ['./company-edit.component.css']
})
export class CompanyEditComponent implements OnInit {
  title:string = "公司維護";
  formFields: FormField<any>[] = [
    new FormField<string>({controlType: "textbox",key: 'companyName',label: '公司名稱',required: true,order: 1}),
    new FormField<string>({controlType: "textbox",key: 'tel',label: '電話',required: false,order: 2}),
    new FormField<string>({controlType: "textbox",key: 'email',label: 'Email',required: false, validator: 'email',order: 3}),
    new FormField<string>({controlType: "textbox",key: 'zipCode',label: '郵遞區號',required: false,order: 4}),
    new FormField<string>({controlType: "textbox",key: 'address',label: '地址',required: true,order: 5}),
  ];
  id!: string;
  isAddMode!: boolean;
  data:any;
  errorMessage?:string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private companiesService: CompaniesService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.companiesService.get(+this.id)
        .pipe(
          map((x:CompanyDTO) => this.data = x)
        ).subscribe();
    }
  }

  onCancel(){
    this.router.navigate(['basic/company']);
  }

  onSave(value:any){
    if(this.isAddMode){
      this.create(value);
    }else{
      this.update(value);
    }
  }
  create(value:any){
    this.companiesService.post(value).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['basic/company']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }
  update(value:any){
    this.companiesService.put(+this.id, value).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['basic/company']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }
}
