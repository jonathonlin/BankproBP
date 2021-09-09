import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, ValidatorFn } from '@angular/forms';
import { FormfieldControlService } from '../../formfield-control.service';
import { FormField } from '../form-field';

@Component({
  selector: 'app-dynamic-form',
  templateUrl: './dynamic-form.component.html',
  styleUrls: ['./dynamic-form.component.css']
})
export class DynamicFormComponent implements OnInit {
  @Input() formFields: FormField<string>[] = [];
  form!: FormGroup;
  payLoad = '';
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() submitData: EventEmitter<any> = new EventEmitter();
  @Input() data:any;  
  @Output() formValueChange: EventEmitter<any> = new EventEmitter();

  constructor(private formfieldControlService: FormfieldControlService) { }

  ngOnInit(): void {
    this.form = this.formfieldControlService.toFromGroup(this.formFields);   
  }
  
  ngOnChanges(): void {   
    if(this.data){
      this.form.patchValue(this.data);
    }    
  }

  onSubmit() {    
    // this.payLoad = JSON.stringify(this.form?.getRawValue());
    this.submitData.emit(this.form.value);
  } 

  onCancel(){
    this.cancel.emit(null);
  }

  setValidator(key:string, validators: ValidatorFn | ValidatorFn[]){    
    this.form.get(key)?.setValidators(validators);
    this.form.get(key)?.updateValueAndValidity();
  }

  onChange(val: any){
    this.formValueChange.emit(val);
  }
}
