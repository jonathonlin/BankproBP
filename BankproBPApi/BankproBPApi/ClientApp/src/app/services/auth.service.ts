import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { LoginModel } from './api.client.generated';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;

  constructor(private http: HttpClient) {
    const userJson = localStorage.getItem('currentUser');
    this.currentUserSubject = new BehaviorSubject<any>(userJson?JSON.parse(userJson): null);
    this.currentUser = this.currentUserSubject.asObservable();
   }

   public get currentUserValue(): User{
     return this.currentUserSubject.value;
   }

  login(model: LoginModel){
    return this.http.post<User>('/api/Authenticate/login', model)
      .pipe(map(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }))
  }

  logout(){
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }  
  
  isPermision(data: any){
    if(data.flag === 'home') return of(true);
    return this.http.post<boolean>('/api/Authenticate/IsPermision', data);
  }

}
