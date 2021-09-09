import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApEditComponent } from './ap-edit.component';

describe('ApEditComponent', () => {
  let component: ApEditComponent;
  let fixture: ComponentFixture<ApEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
