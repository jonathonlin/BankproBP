import { CompaniesService, ProgramUserDTO } from './../../../../../services/api.client.generated';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProgramUserService, CustomerService } from 'src/app/services/api.client.generated';
import { FormField } from 'src/app/shared/dynamic/form-field';
import { map } from 'rxjs/operators';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-register-edit',
  templateUrl: './register-edit.component.html',
  styleUrls: ['./register-edit.component.css']
})
export class RegisterEditComponent implements OnInit {
  title:string = "使用者維護";
  formFields: FormField<any>[] = [
    new FormField<string>({controlType: "textbox",key: 'account',label: '帳號',required: true, order: 1, readonly: false}),
    new FormField<string>({controlType: "textbox",key: 'password',label: '密碼',required: false, order: 2, 
                           type: 'password', pattern: '(?=[a-zA-Z]*[0-9])(?=[0-9]*[a-zA-Z])[a-zA-Z0-9]{6,12}', 
                           patternErrorMessage: '格式錯誤，輸入6-12位的字母及數字'}),
    new FormField<string>({controlType: "textbox",key: 'userName',label: '使用者名稱',required: true, order: 3}),
    new FormField<string>({controlType: "textbox",key: 'email',label: 'Email',required: true,order: 4, type: 'email', validator: 'email'}),
    new FormField<string>({controlType: "dropdown",key: 'accountType',label: '帳戶類別',required: true,order: 6, options:[{key:undefined, value:'請選擇'},{key:1, value:'公司帳戶'},{key:2, value:'客戶帳戶'}]}),
    new FormField<string>({controlType: "dropdown",key: 'companyId',label: '公司',required: true,order: 6}),
    new FormField<string>({controlType: "dropdown",key: 'customerId',label: '客戶',required: false,order: 7}),
  ];
  id!: string;
  isAddMode!: boolean;
  data:any;
  errorMessage?:string;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private companiesService: CompaniesService,
    private customerService: CustomerService,
    private programUserService: ProgramUserService) { }
    
  
  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.programUserService.get(+this.id)
        .pipe(
          map((x:ProgramUserDTO) => this.data = x)
        ).subscribe();
      this.formFields.find(f=>f.key === 'account').readonly = true;
    } 

    
    this.companiesService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined)    
      .subscribe(res=>{
        let options = res.data?.map(x => ({key: x.id || undefined, value: x.companyName || ''})) || [];
        options.unshift({ key: undefined, value: '請選擇'});    
        this.formFields.find(x=>x.key === 'companyId')!.options = options;
      });
      
      this.customerService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined)    
      .subscribe(res=>{
        let options = res.data?.map(x => ({key: x.id || undefined, value: x.customerName || ''})) || [];
        options.unshift({ key: undefined, value: '請選擇'});    
        this.formFields.find(x=>x.key === 'customerId')!.options = options;
      });
    
  }

  onCancel(){
    this.router.navigate(['sys/register']);
  }

  onSave(value:any){    
    if(this.isAddMode){
      this.create(value);
    } else {
      this.update(value);
    }
  }
  create(value:any){
    this.programUserService.post(value).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['sys/register']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }
  update(value:any){
    this.programUserService.put(+this.id, value).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['sys/register']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }

}
