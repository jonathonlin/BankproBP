import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankEdtiComponent } from './bank-edti.component';

describe('BankEdtiComponent', () => {
  let component: BankEdtiComponent;
  let fixture: ComponentFixture<BankEdtiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BankEdtiComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BankEdtiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
