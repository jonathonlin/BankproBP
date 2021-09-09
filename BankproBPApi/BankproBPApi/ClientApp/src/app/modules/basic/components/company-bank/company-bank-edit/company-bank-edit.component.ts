import { CompanyBankAccountService, CompanyBankAccountDTO } from './../../../../../services/api.client.generated';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompaniesService } from 'src/app/services/api.client.generated';
import { FormField } from 'src/app/shared/dynamic/form-field';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-company-bank-edit',
  templateUrl: './company-bank-edit.component.html',
  styleUrls: ['./company-bank-edit.component.css']
})
export class CompanyBankEditComponent implements OnInit {
  title:string = "公司銀行帳戶維護";
  formFields: FormField<any>[] = [
    new FormField<string>({controlType: "textbox",key: 'bankCode',label: '銀行代號',required: true,order: 1}),
    new FormField<string>({controlType: "textbox",key: 'bankAccount',label: '銀行帳號',required: true,order: 2}),
    new FormField<string>({controlType: "textbox",key: 'bankAccountName',label: '銀行帳號名稱',required: true,order: 3}),
    new FormField<string>({controlType: "textbox",key: 'companyBankAtmId',label: '虛擬帳號代碼',required: false,order: 4}),
    new FormField<string>({controlType: "dropdown",key: 'companyId',label: '公司',required: true,order: 5}),
  ];
  id!: string;
  isAddMode!: boolean;
  data:any;
  errorMessage?:string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private companiesService: CompaniesService,
    private companyBankAccountService: CompanyBankAccountService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.companyBankAccountService.get(+this.id)
        .pipe(
          map((x:CompanyBankAccountDTO) => this.data = x)
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
    this.router.navigate(['basic/companybank']);
  }

  onSave(value:any){
    if(this.isAddMode){
      this.create(value);
    }else{
      this.update(value);
    }
  }
  create(value:any){
    this.companyBankAccountService.post(value).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['basic/companybank']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }
  update(value:any){
    this.companyBankAccountService.put(+this.id, value).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['basic/companybank']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }
}
