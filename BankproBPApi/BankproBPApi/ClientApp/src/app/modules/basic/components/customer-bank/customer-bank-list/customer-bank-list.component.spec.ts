import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerBankListComponent } from './customer-bank-list.component';

describe('CustomerBankListComponent', () => {
  let component: CustomerBankListComponent;
  let fixture: ComponentFixture<CustomerBankListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerBankListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerBankListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
