import { LoadingInterceptor } from './../Interceptors/loading.interceptor';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule} from '@angular/material/toolbar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule} from '@angular/material/sidenav';
import { MatTreeModule} from '@angular/material/tree';
import { TableComponent } from './components/table/table.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from './components/dialog/dialog.component';
import { JwtInterceptor } from '../Interceptors/jwt.interceptor';
import { ErrorInterceptor } from '../Interceptors/error.interceptor';
import { DynamicFormInputComponent } from './dynamic/dynamic-form-input/dynamic-form-input.component';
import { DynamicFormComponent } from './dynamic/dynamic-form/dynamic-form.component';
import { MatSelectModule } from '@angular/material/select';
import { MatListModule } from '@angular/material/list';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { SpinnerOverlayComponent } from './components/spinner-overlay/spinner-overlay.component';
import { MatSortModule } from '@angular/material/sort';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { YearMonthPickerComponent } from './components/year-month-picker/year-month-picker.component';

const materials = [
  MatFormFieldModule,
  MatInputModule,
  MatButtonModule,
  MatToolbarModule,
  MatCardModule,  
  MatIconModule,
  MatSidenavModule,
  MatTreeModule,
  MatTableModule,
  MatPaginatorModule,
  MatDialogModule,
  MatSelectModule,
  MatListModule,
  MatCheckboxModule,
  MatProgressSpinnerModule,
  MatSortModule,
  MatDatepickerModule,
  MatMomentDateModule,
];

@NgModule({
  declarations: [
    TableComponent, 
    DialogComponent, 
    DynamicFormInputComponent, 
    DynamicFormComponent, 
    SpinnerOverlayComponent, 
    YearMonthPickerComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,    
    FormsModule,
    FlexLayoutModule,
    HttpClientModule,
    materials
  ],
  exports: [   
    CommonModule,
    DynamicFormInputComponent, 
    DynamicFormComponent,     
    SpinnerOverlayComponent,
    YearMonthPickerComponent,
    ReactiveFormsModule,
    FormsModule,
    TableComponent,
    FlexLayoutModule,
    HttpClientModule,
    materials,
  ],
  providers: [
    MatDatepickerModule,
    MatMomentDateModule,
    {provide: MAT_DATE_LOCALE, useValue: 'zh-TW'},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
  ]
})
export class SharedModule { }
