import { AuthenticateService, ResponseOfObject } from './../../../services/api.client.generated';
import { DialogService } from 'src/app/services/dialog.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { expand, first } from 'rxjs/operators';
import { AuthService } from 'src/app/services/auth.service';
import { concat, of } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginform!: FormGroup;
  returnUrl!: string;
  submitted = false;
  error = '';
  constructor(private fb: FormBuilder, 
    private router: Router,
    private authService: AuthService,
    private dialogService: DialogService,
    private authenticateService: AuthenticateService
   ) { }

  ngOnInit(): void {
    this.loginform = this.fb.group({
      account: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  get account() { return this.loginform.get('account'); }
  get password() { return this.loginform.get('password'); }
  
  
  onSubmit(){    
    if(!this.loginform.valid) return;
    this.authService.login(this.loginform.value)
      .pipe(first())
      .subscribe(
        data => {
        if(!!this.returnUrl)
          this.router.navigate([this.returnUrl]);
        else
          this.router.navigate(['/']);
      },
      error => {
          this.error = error;          
        }
      );
  }

  
  onForget(){
    
    if (!this.account.valid) {
      this.dialogService.alert("", "請輸入帳號");
      return;
    }
    
    this.authenticateService.forget(this.account.value)
      .subscribe((r:ResponseOfObject)=>{
        this.dialogService.alert("", r.message);
      });
    
  }
}
