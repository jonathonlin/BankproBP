import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyBankEditComponent } from './company-bank-edit.component';

describe('CompanyBankEditComponent', () => {
  let component: CompanyBankEditComponent;
  let fixture: ComponentFixture<CompanyBankEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompanyBankEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyBankEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
