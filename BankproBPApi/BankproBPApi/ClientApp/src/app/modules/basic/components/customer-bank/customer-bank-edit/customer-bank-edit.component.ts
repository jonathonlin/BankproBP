import { CustomerBankAccountDTO, CustomerBankAccountService } from './../../../../../services/api.client.generated';
import { CustomerService } from 'src/app/services/api.client.generated';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormField } from 'src/app/shared/dynamic/form-field';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-customer-bank-edit',
  templateUrl: './customer-bank-edit.component.html',
  styleUrls: ['./customer-bank-edit.component.css']
})
export class CustomerBankEditComponent implements OnInit {
  title:string = "客戶銀行帳戶維護";
  formFields: FormField<any>[] = [
    new FormField<string>({controlType: "textbox",key: 'bankCode',label: '銀行代號',required: true,order: 1}),
    new FormField<string>({controlType: "textbox",key: 'bankAccount',label: '銀行帳號',required: true,order: 2}),
    new FormField<string>({controlType: "textbox",key: 'bankAccountName',label: '銀行帳號名稱',required: true,order: 3}),
    new FormField<string>({controlType: "textbox",key: 'allowDiffAmount',label: '允許差額範圍', type: 'number', required: false,order: 4}),
    new FormField<string>({controlType: "dropdown",key: 'customerId',label: '客戶',required: true,order: 5}),
  ];
  id!: string;
  isAddMode!: boolean;
  data:any;
  errorMessage?:string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService,
    private customerBankAccountService: CustomerBankAccountService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.customerBankAccountService.get(+this.id)
        .pipe(
          map((x:CustomerBankAccountDTO) => this.data = x)
        ).subscribe();
    }

    this.customerService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined)    
    .subscribe(res=>{
      let options = res.data?.map(x => ({key: x.id || undefined, value: x.customerName || ''})) || [];
      options.unshift({ key: undefined, value: '請選擇'});    
      this.formFields.find(x=>x.key === 'customerId')!.options = options;
    });
  }
  onCancel(){
    this.router.navigate(['basic/customerbank']);
  }

  onSave(value:any){
    if(this.isAddMode){
      this.create(value);
    }else{
      this.update(value);
    }
  }
  create(value:any){
    this.customerBankAccountService.post(value).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['basic/customerbank']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }
  update(value:any){
    this.customerBankAccountService.put(+this.id, value).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['basic/customerbank']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }

}
