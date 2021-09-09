import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { ProgramRoleService, ProgramUserRoleDTO, ProgramUserRoleService, ProgramUserService } from 'src/app/services/api.client.generated';
import { FormField } from 'src/app/shared/dynamic/form-field';

@Component({
  selector: 'app-user-role-edit',
  templateUrl: './user-role-edit.component.html',
  styleUrls: ['./user-role-edit.component.css']
})
export class UserRoleEditComponent implements OnInit {
  title:string = "使用者角色維護";
  formFields: FormField<any>[] = [
    new FormField<string>({controlType: "dropdown",key: 'userId',label: '使用者',required: true,order: 1 }),
    new FormField<string>({controlType: "dropdown",key: 'roleId',label: '角色',required: true,order: 2 } ),
   
  ];
  id!: string;
  isAddMode!: boolean;
  data:any;
  errorMessage?:string;
  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private programUserService: ProgramUserService,
    private programRoleService: ProgramRoleService,
    private programUserRoleService: ProgramUserRoleService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.programUserRoleService.get(+this.id)
        .pipe(
          map((x:ProgramUserRoleDTO) => this.data = x)
        ).subscribe();
    }

    this.programUserService.getProgramUsers(undefined,undefined,undefined,undefined,undefined,undefined)
    .subscribe(res=>{
      let options = res.data?.map(x => ({key: x.id || undefined, value: x.userName || ''})) || [];
      options.unshift({ key: undefined, value: '請選擇'});    
      this.formFields.find(x=>x.key === 'userId')!.options = options;
    });

    this.programRoleService.getAll(undefined,undefined,undefined,undefined,undefined,undefined)
    .subscribe(res=>{
      let options = res.data?.map(x => ({key: x.id || undefined, value: x.roleName || ''})) || [];
      options.unshift({ key: undefined, value: '請選擇'});    
      this.formFields.find(x=>x.key === 'roleId')!.options = options;
    });
  }

  onCancel(){
    this.router.navigate(['sys/userrole']);
  }

  onSave(value:any){
    if(this.isAddMode){
      this.create(value);
    }else{
      this.update(value);
    }
  }

  create(value:any){
    this.programUserRoleService.post(value).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['sys/userrole']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }

  update(value:any){
    this.programUserRoleService.put(+this.id, value).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['sys/userrole']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }
}
