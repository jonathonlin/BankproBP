import { BankDTO, BankService } from './../../../../../services/api.client.generated';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormField } from 'src/app/shared/dynamic/form-field';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-bank-edti',
  templateUrl: './bank-edti.component.html',
  styleUrls: ['./bank-edti.component.css']
})
export class BankEdtiComponent implements OnInit {
  title:string = "銀行維護";
  formFields: FormField<any>[] = [
    new FormField<string>({controlType: "textbox",key: 'bankCode',label: '銀行代號',required: true,order: 1}),
    new FormField<string>({controlType: "textbox",key: 'bankName',label: '銀行名稱',required: true,order: 2})    
  ];
  id!: string;
  isAddMode!: boolean;
  data:any;
  errorMessage?:string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bankService: BankService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.bankService.get(+this.id)
        .pipe(
          map((x:BankDTO) => this.data = x)
        ).subscribe();
    }
    
  }

  onCancel(){
    this.router.navigate(['basic/bank']);
  }

  onSave(value:any){
    if(this.isAddMode){
      this.create(value);
    }else{
      this.update(value);
    }
  }
  create(value:any){
    this.bankService.post(value).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['basic/bank']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }
  update(value:any){
    this.bankService.put(+this.id, value).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['basic/bank']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }

}
