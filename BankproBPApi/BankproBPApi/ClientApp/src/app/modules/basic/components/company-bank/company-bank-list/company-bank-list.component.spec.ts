import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyBankListComponent } from './company-bank-list.component';

describe('CompanyBankListComponent', () => {
  let component: CompanyBankListComponent;
  let fixture: ComponentFixture<CompanyBankListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompanyBankListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyBankListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
