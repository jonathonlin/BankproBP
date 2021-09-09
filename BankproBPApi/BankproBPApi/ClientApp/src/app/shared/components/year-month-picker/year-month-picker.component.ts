import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import * as moment from 'moment';
import { Moment } from 'moment';

@Component({
  selector: 'app-year-month-picker',
  templateUrl: './year-month-picker.component.html',
  styleUrls: ['./year-month-picker.component.css'],
  providers:[
    { provide: MAT_DATE_FORMATS, useValue: {
      parse: {
        dateInput: 'YYYYMM',
      },
      display: {
        dateInput: 'YYYYMM',
        monthYearLabel: 'YYYY MMM',
        dateA11yLabel: 'LL',
        monthYearA11yLabel: 'YYYY MMMM',
      },
    }},
  ]
})
export class YearMonthPickerComponent implements OnInit {
  @Input() label:string;
  @Input() form: FormGroup;
  @Input() name: string;
  constructor() { }

  ngOnInit(): void {
  }

  get input() { return this.form.get(this.name); } 

  yearMonthSelected(date:Moment, datepicker: MatDatepicker<Moment>){
    if(this.input.value instanceof moment === false){
      let year = parseInt(this.input.value.toString().substr(0,4));
      let month = parseInt(this.input.value.toString().substr(4,2));
      this.input.setValue(moment().set({'year': year, 'month': month}));
    }    
    const ctrlValue: Moment = this.input.value;
    ctrlValue.month(date.month());
    this.input.setValue(ctrlValue);    
    datepicker.close();
  }

}
