export class FormField<T> {
    value?: T;
  key: string;
  label: string;
  required: boolean;
  validator: string;
  order: number;
  controlType: string;
  type: string;
  options: { key: any, value: string }[];  
  minlength?: number;
  pattern?: string;
  patternErrorMessage?: string;
  readonly?: boolean;
  constructor(
    options: {
      value?: T;
      key?: string;
      label?: string;
      required?: boolean;
      validator?: string;
      order?: number;
      controlType?: string;
      type?: string;
      options?: { key: any, value: string }[];
      minlength?: number;
      pattern?: string;
      patternErrorMessage?: string;
      readonly?: boolean;
    } = {}) {
    this.value = options.value;
    this.key = options.key || '';
    this.label = options.label || '';
    this.required = !!options.required;
    this.validator = options.validator || '';
    this.order = options.order === undefined ? 1 : options.order;
    this.controlType = options.controlType || '';
    this.type = options.type || '';
    this.options = options.options || [];
    this.minlength = options.minlength;
    this.pattern = options.pattern;
    this.patternErrorMessage = options.patternErrorMessage;
    this.readonly = options.readonly;
  }
}
