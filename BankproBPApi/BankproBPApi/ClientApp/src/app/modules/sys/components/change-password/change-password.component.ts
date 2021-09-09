import { DialogService } from './../../../../services/dialog.service';
import { ProgramUserService } from './../../../../services/api.client.generated';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  title:string = "密碼變更";
  form: FormGroup;

  errorMessage?: string;
  
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private programUserService: ProgramUserService,
    private dialogService: DialogService
  ) { 
    this.form = this.fb.group({
      password: ['', Validators.required]
    });

  }

  get errMsg(): string {
    let errMsg =  '';
    if(this.form.get('password').getError('required'))
      errMsg = "密碼欄位 必輸"; 
    return errMsg;
  }
  
  get isValid() { return this.form.get('password').valid; }

  ngOnInit(): void {
  }

  onSubmit(){
    this.programUserService.resetPassword(this.form.get('password').value).subscribe(r=>{      
      if(r.isOk){
        this.dialogService.alert('','密碼變更完成。');
      }
      this.errorMessage = r.message;
    });
  }
}
