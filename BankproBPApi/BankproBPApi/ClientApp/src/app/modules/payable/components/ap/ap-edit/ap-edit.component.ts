import { DialogService } from './../../../../../services/dialog.service';
import { BehaviorSubject } from 'rxjs';
import { MatDatepicker } from '@angular/material/datepicker';
import { AccountPayableDTO, AccountPayableService, CustomerService } from './../../../../../services/api.client.generated';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Moment } from 'moment';
import * as moment from 'moment';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { map } from 'rxjs/operators';

export const MY_FORMATS = {
  parse: {
    dateInput: 'YYYY/MM/DD',
  },
  display: {
    dateInput: 'YYYY/MM/DD',
    monthYearLabel: 'YYYY MMM DD',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-ap-edit',
  templateUrl: './ap-edit.component.html',
  styleUrls: ['./ap-edit.component.css'],  
  providers:[
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }
  ]
})
export class ApEditComponent implements OnInit {
  title:string = '應收立帳';
  id!: string;
  isAddMode!: boolean;
  
  errorMessage?:string;
  form: FormGroup;
  customerOptions:{key:any, value:string}[] = [];
  displayColumns = ['actions', 'productName','unitPrice','quantity','totalAmount','note'];
  
  dataSource = new BehaviorSubject<AbstractControl[]>([]);

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dialogService: DialogService,
    private accountPayableService: AccountPayableService,
    private customerService: CustomerService) {
      this.form = this.fb.group({
        customerId: ['', Validators.required],
        yearMonth: [moment(new Date()), Validators.required],
        apNo: ['自動編號', Validators.required],
        invoiceNo: ['', Validators.required],
        invoiceDate: ['', Validators.required],
        invoiceAmount: ['', Validators.required],
        apAmount: ['', Validators.required],
        expireDate: ['', Validators.required],
        note: ['', null],
        accountPayableDetails: this.fb.array([])
      });

      this.customerService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined,undefined)
      .subscribe(res=>{
        let options = res.data?.map(x => ({key: x.id || undefined, value: x.customerName || ''})) || [];
        options.unshift({ key: undefined, value: '請選擇'});    
        this.customerOptions = options;
      });
    }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.accountPayableService.get(+this.id)
        .pipe(
          map((x:AccountPayableDTO) => {            
            x.accountPayableDetails.forEach(e=>this.addDetail());
            this.form.patchValue(x);          
          })
        )
        .subscribe();
    }
    this.dataSource.next(this.accountPayableDetails.controls);
  }

  get customerId() { return this.form.get('customerId'); }
  get yearMonth() { return this.form.get('yearMonth'); }
  get apNo() { return this.form.get('apNo'); }
  get invoiceNo() { return this.form.get('invoiceNo'); }
  get invoiceDate() { return this.form.get('invoiceDate'); }
  get invoiceAmount() { return this.form.get('invoiceAmount'); }
  get apAmount() { return this.form.get('apAmount'); }
  get expireDate() { return this.form.get('expireDate'); }
  get apStatus() { return this.form.get('apStatus'); }
  get note() { return this.form.get('note'); }
  get accountPayableDetails(): FormArray { return this.form.get('accountPayableDetails') as FormArray; }

 
  addDetail() {    
    this.accountPayableDetails.push(
      this.fb.group({
        ApNo:['', null],
        productName:['', Validators.required],
        unitPrice:[0,Validators.required],
        quantity:[0,Validators.required],
        totalAmount:[0,Validators.required],
        note:['',null]        
      })
    );  
    
    this.updateTableView();
  }

  removeDetail(index:number){
    this.accountPayableDetails.removeAt(index);
    this.updateTableView();
  }

  updateTableView(){    
    this.accountPayableDetails.controls.forEach(e=>{      
      let qty = e.get('quantity').value;
      let unitPrice = e.get('unitPrice').value;
      e.get('totalAmount').setValue(qty*unitPrice); 
    });    
    this.dataSource.next(this.accountPayableDetails.controls);
  }

  payload: string;
  
  onSubmit(){        
   
    this.payload = JSON.stringify(this.form.value);
    if(this.accountPayableDetails.length === 0){
      this.dialogService.alert("","請輸入明細資料！");
      return false;
    }

    
    if(this.isAddMode){
      this.create(this.form.value);
    }else{
      this.update(this.form.value);
    }
    

  }

  create(value:any) {
    const data = {...value, 
      yearMonth: value.yearMonth.format('YYYYMM'),
      invoiceDate: value.invoiceDate.format('YYYY-MM-DD'),
      expireDate: value.expireDate.format('YYYY-MM-DD')
     };
     
    this.accountPayableService.post(data).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['payable/ap']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }

  update(value:any) {    
    const data = {...value, 
      yearMonth: value.yearMonth instanceof moment ? value.yearMonth.format('YYYYMM'): value.yearMonth,
      invoiceDate: value.invoiceDate instanceof moment ? value.invoiceDate.format('YYYY-MM-DD'): value.invoiceDate,
      expireDate: value.expireDate instanceof moment ? value.expireDate.format('YYYY-MM-DD'): value.expireDate
     };

     this.accountPayableService.put(+this.id, data).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['payable/ap']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }

  onCancel(){
    this.router.navigate(['payable/ap']);
  }
}

