import { Component, ViewChild } from '@angular/core';
import { User } from './models/user';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {  
  currentUser?: User;

  constructor(private authService: AuthService){
    this.authService.currentUser.subscribe(x=> this.currentUser = x);
    
  }

  
}
