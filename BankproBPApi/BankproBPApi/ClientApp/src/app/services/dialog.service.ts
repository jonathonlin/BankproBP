import { DialogComponent } from './../shared/components/dialog/dialog.component';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private dialog: MatDialog) { }

  alert(title:string, message?:string) : Observable<boolean> {
    const dialogRef = this.dialog.open(DialogComponent, { width: '300px', data: { title: title, content: message, type: 'alert', result: true } });
    return dialogRef.afterClosed().pipe(mergeMap(result => of(!!result)));
  }

  confirm(title:string, message?:string): Observable<boolean> {
    const dialogRef = this.dialog.open(DialogComponent, { width: '300px', data: { title: title, content: message, type: 'confirm', result: true } });
    return dialogRef.afterClosed().pipe(mergeMap(result => of(!!result)));
  }

  prompt(title:string, result: any, message?:string): Observable<any> {    
    const dialogRef = this.dialog.open(DialogComponent, { width: '300px', data: { title: title, content: message, type: 'prompt', result: result } });
    return dialogRef.afterClosed().pipe(mergeMap(result => of(result)));
  }

}
