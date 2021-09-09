import { Component, OnInit, ViewChild } from '@angular/core';
import { Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { map, catchError } from 'rxjs/operators';
import { CompanyProgramDTO, CompanyProgramService } from 'src/app/services/api.client.generated';
import { DynamicFormComponent } from 'src/app/shared/dynamic/dynamic-form/dynamic-form.component';
import { FormField } from 'src/app/shared/dynamic/form-field';

@Component({
  selector: 'app-program-edit',
  templateUrl: './program-edit.component.html',
  styleUrls: ['./program-edit.component.css']
})
export class ProgramEditComponent implements OnInit {
  title:string = "程式維護";
  formFields: FormField<any>[] = [
    new FormField<string>({controlType: "textbox",key: 'programName',label: '程式名稱',required: true,order: 1}),
    new FormField<string>({controlType: "dropdown",key: 'parentId',label: '父目錄',required: false,type: 'number',order: 2}),
    new FormField<string>({controlType: "dropdown",key: 'programType',label: '類別',required: true,order: 3,options: [{key: 0, value:'目錄'}, {key:1, value:'程式'}]}),
    new FormField<string>({controlType: "textbox",key: 'programUrl',label: '程式路徑',required: false,order: 4}),
    new FormField<string>({controlType: "dropdown",key: 'status',label: '狀態',required: true,order: 5,options: [{key:1, value:'啟用'}, {key:0, value:'停用'}]}),
    new FormField<string>({controlType: "textbox",key: 'sort',label: '排序',required: false,type: 'number',order: 6}),
  ];
  id!: string;
  isAddMode!: boolean;
  data:any;
  errorMessage?:string;

  @ViewChild(DynamicFormComponent) dynamicFormComponent!: DynamicFormComponent; 

  constructor(
    private route: ActivatedRoute,
    private router: Router, 
    private companyProgramService:CompanyProgramService) {
    
   }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.companyProgramService.get(+this.id)
        .pipe(
          map((x:CompanyProgramDTO) => this.data = x)          
        ).subscribe();
    }

    this.companyProgramService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined)
      .pipe(
        map(res => res.data?.filter(f=>f.programType === 0 && f.status === 1))
      )
      .subscribe(res => {
        let options = res?.map(x => ({key: x.id || undefined, value: x.programName || ''}));
        options?.unshift({ key: undefined, value: '請選擇'});    
        this.formFields.find(x=>x.key === 'parentId')!.options = options || [];
      });     
  }

   
  onSave(value:any){
    if(this.isAddMode){
      this.create(value);
    }else{
      this.update(value);
    }
  }

  create(value:any){
    this.companyProgramService.post(value).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['sys/program']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }

  update(value:any){
    this.companyProgramService.put(+this.id, value).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['sys/program']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }

  onCancel(){
    this.router.navigate(['sys/program']);
  }

  onFormValueChange(value: any){
    if(value.key === 'programType'){    
      let validators = value.value === 1 ? Validators.required : [];
      this.dynamicFormComponent.form.get('programUrl')?.setValidators(validators);
      this.dynamicFormComponent.form.get('programUrl')?.updateValueAndValidity();
    }
  }

}
