import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormField } from '../form-field';

@Component({
  selector: 'app-dynamic-form-input',
  templateUrl: './dynamic-form-input.component.html',
  styleUrls: ['./dynamic-form-input.component.css']
})
export class DynamicFormInputComponent implements OnInit {
  @Input() input!: FormField<string>;
  @Input() form!: FormGroup;
  @Output() change: EventEmitter<any> = new EventEmitter();

  constructor() { }
  
  onChange() {    
    this.change.emit({ key: this.input.key, value: this.form.get(this.input.key)?.value });
  }

  get isValid() { return this.form.controls[this.input.key].valid; }

  get control() { return this.form.get(this.input.key); }

  get errorMessage(): string { 
    let errs: string[] = [];
    if(this.form.get(this.input.key)?.getError('required')){
      errs.push(this.input.label + '必輸');
    }
    if(this.form.get(this.input.key)?.getError('email')){
      errs.push(this.input.label + '格式錯誤');
    }
    if(this.form.get(this.input.key)?.getError('minlength')){
      errs.push(`${this.input.label} 最少需有 ${this.input.minlength} 位數`);
    }   
    if(this.form.get(this.input.key)?.getError('pattern')){      
      errs.push(`${this.input.patternErrorMessage}`);
    }
    return errs.join('，') + '。';
  }
  ngOnInit(): void {
  }

}
