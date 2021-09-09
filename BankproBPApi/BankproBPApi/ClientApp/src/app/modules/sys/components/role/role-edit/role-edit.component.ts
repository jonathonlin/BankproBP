import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { ProgramRoleDTO, ProgramRoleService } from 'src/app/services/api.client.generated';
import { FormField } from 'src/app/shared/dynamic/form-field';

@Component({
  selector: 'app-role-edit',
  templateUrl: './role-edit.component.html',
  styleUrls: ['./role-edit.component.css']
})
export class RoleEditComponent implements OnInit {
  title:string = "角色維護";
  formFields: FormField<any>[] = [
    new FormField<string>({controlType: "textbox",key: 'roleName',label: '角色名稱',required: true,order: 1}),
    new FormField<string>({controlType: "dropdown",key: 'status',label: '狀態',required: true,order: 2,options: [{key:1, value:'啟用'}, {key:0, value:'停用'}]}),
  ];
  id!: string;
  isAddMode!: boolean;
  data:any;
  errorMessage?:string;

  
  constructor(
    private route: ActivatedRoute,
    private router: Router, 
    private programRoleService:ProgramRoleService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.title = this.title + (this.isAddMode ? ' - 新增': ' - 修改');
    if(!this.isAddMode){
      this.programRoleService.get(+this.id)
        .pipe(
          map((x:ProgramRoleDTO) => this.data = x)
        ).subscribe();
    }
  }

  onSave(value:any){
    if(this.isAddMode){
      this.create(value);
    }else{
      this.update(value);
    }
  }

  create(value:any){
    this.programRoleService.post(value).subscribe(x=>{     
      if(!x.isOk){   
        this.errorMessage = x.message;     
      }else{
        this.router.navigate(['sys/role']);  
      }              
    },
    error => {           
      this.errorMessage = error.message;
    });
  }

  update(value:any){
    this.programRoleService.put(+this.id, value).subscribe(x=>{
      if(!x.isOk){
        this.errorMessage = x.message;
      }else{
        this.router.navigate(['sys/role']);
      }
    },
    error =>{
      this.errorMessage = error.message;
    });
  }

  onCancel(){
    this.router.navigate(['sys/role']);
  }
}
