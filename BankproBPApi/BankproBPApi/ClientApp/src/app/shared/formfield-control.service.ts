import { Injectable } from '@angular/core';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { FormField } from './dynamic/form-field';

@Injectable({
  providedIn: 'root'
})
export class FormfieldControlService {

  constructor() { }
  toFromGroup(inputs: FormField<string>[]): FormGroup {
    const group: any = {};
    inputs.forEach(input=>{
      let validator: ValidatorFn[] = input.required ? [Validators.required]: [];
      if(input.minlength) validator.push(Validators.minLength(input.minlength));
      if(input.pattern) validator.push(Validators.pattern(input.pattern));
      switch(input.validator){
        case 'email':
          validator.push(Validators.email);
          break;
        default:
          break;
      }

      group[input.key] = validator.length > 0 ? new FormControl(input.value||'',validator)
                                              : new FormControl(input.value||'');

    });
    return new FormGroup(group);
  }
}
