import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private authService:AuthService, private router:Router) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {      
      if(err.status === 401){
        this.authService.logout();        
        if(!err.error.message)
          location.reload();
      }
      if(err.status === 404){
        this.router.navigate(['pagenotfound']);
      }
      const error = err.error.message || err.error;
      return throwError(error);           
    }));
  }
}
