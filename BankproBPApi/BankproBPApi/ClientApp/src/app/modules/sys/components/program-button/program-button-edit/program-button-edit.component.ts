import { CompanyProgramButtonService, CompanyProgramButtonDTO } from './../../../../../services/api.client.generated';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyProgramService } from 'src/app/services/api.client.generated';
import { DynamicFormComponent } from 'src/app/shared/dynamic/dynamic-form/dynamic-form.component';
import { FormField } from 'src/app/shared/dynamic/form-field';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-program-button-edit',
  templateUrl: './program-button-edit.component.html',
  styleUrls: ['./program-button-edit.component.css']
})
export class ProgramButtonEditComponent implements OnInit {
  title:string = "程式按鈕維護";
  formFields: FormField<any>[] = [
    new FormField<string>({controlType: "textbox",key: 'buttonText',label: '按鈕名稱',required: true,order: 1}),
    new FormField<string>({controlType: "textbox",key: 'buttonAction',label: '動作',required: false,type: 'number',order: 2}),
    new FormField<string>({controlType: "dropdown",key: 'programId',label: '程式',required: true,order: 3}),
    new FormField<string>({controlType: "dropdown",key: 'status',label: '狀態',required: true,order: 4,options: [{key:1, value:'啟用'}, {key:0, value:'停用'}]}),
    new FormField<string>({controlType: "textbox",key: 'sort',label: '排序',required: false,type: 'number',order: 5}),
  ];
  id!: string;
  isAddMode!: boolean;
  data:any;
  errorMessage?:string;

  @ViewChild(DynamicFormComponent) dynamicFormComponent!: DynamicFormComponent; 

  constructor(
    private route: ActivatedRoute,
    private router: Router, 
    private companyProgramButtonService: CompanyProgramButtonService,
    private companyProgramService:CompanyProgramService
  ) {  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.companyProgramButtonService.get(+this.id)
        .pipe(
          map((x:CompanyProgramButtonDTO) => this.data = x)
        ).subscribe();
    }

    this.companyProgramService.getAll(undefined, 1, 1,undefined,undefined,undefined,undefined)
      .pipe(        
        map(res => {
          let options = res.data.map(x=>({key: x.id, value: x.programName}));
          options.unshift({ key: undefined, value: '請選擇'});
          this.formFields.find(x=>x.key === 'programId')!.options = options;
        })
      ).subscribe();     

  }

  onSave(value:any){    
    if(this.isAddMode){
      this.create(value);
    }else{
      this.update(value);
    }
  }

  create(value:any){
    this.companyProgramButtonService.post(value).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['sys/programbutton']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }

  update(value:any){
    this.companyProgramButtonService.put(+this.id, value).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['sys/programbutton']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }

  onCancel(){
    this.router.navigate(['sys/programbutton']);
  }

}
