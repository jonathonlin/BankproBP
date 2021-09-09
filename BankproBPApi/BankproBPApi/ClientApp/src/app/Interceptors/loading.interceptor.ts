import { SpinnerOverlayService } from './../services/spinner-overlay.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,  
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  private _count = 0;
  constructor(private spinnerOverlayService: SpinnerOverlayService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this._count++;
    this.spinnerOverlayService.show();
    return next
      .handle(request)
      .pipe(
        finalize(() => {
          this._count--;
          if(this._count === 0){
            this.spinnerOverlayService.hide();
          }
        })
      );
  }
}
