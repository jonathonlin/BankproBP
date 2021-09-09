import { CustomerDTO, CustomerService } from './../../../../../services/api.client.generated';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompaniesService } from 'src/app/services/api.client.generated';
import { FormField } from 'src/app/shared/dynamic/form-field';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.css']
})
export class CustomerEditComponent implements OnInit {
  title:string = "客戶維護";
  formFields: FormField<any>[] = [
    new FormField<string>({controlType: "textbox",key: 'customerName',label: '客戶名稱',required: true,order: 1}),
    new FormField<string>({controlType: "textbox",key: 'tel',label: '電話',required: false,order: 2}),
    new FormField<string>({controlType: "textbox",key: 'mobile',label: '手機',required: false,order: 3}),
    new FormField<string>({controlType: "textbox",key: 'email',label: 'Email',required: false, validator: 'email',order: 4}),
    new FormField<string>({controlType: "textbox",key: 'zipCode',label: '郵遞區號',required: false,order: 5}),
    new FormField<string>({controlType: "textbox",key: 'address',label: '地址',required: true,order: 6}),
    new FormField<string>({controlType: "dropdown",key: 'companyId',label: '所屬公司',required: true,order: 7}),
  ];
  id!: string;
  isAddMode!: boolean;
  data:any;
  errorMessage?:string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private companiesService: CompaniesService,
    private customerService: CustomerService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.customerService.get(+this.id)
        .pipe(
          map((x:CustomerDTO) => this.data = x)
        ).subscribe();
    }

    this.companiesService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined)    
    .subscribe(res=>{
      let options = res.data?.map(x => ({key: x.id || undefined, value: x.companyName || ''})) || [];
      options.unshift({ key: undefined, value: '請選擇'});    
      this.formFields.find(x=>x.key === 'companyId')!.options = options;
    });
  }

  onCancel(){
    this.router.navigate(['basic/customer']);
  }

  onSave(value:any){
    if(this.isAddMode){
      this.create(value);
    }else{
      this.update(value);
    }
  }
  create(value:any){
    this.customerService.post(value).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['basic/customer']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }
  update(value:any){
    this.customerService.put(+this.id, value).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['basic/customer']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }
}
