import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerBankEditComponent } from './customer-bank-edit.component';

describe('CustomerBankEditComponent', () => {
  let component: CustomerBankEditComponent;
  let fixture: ComponentFixture<CustomerBankEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerBankEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerBankEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
