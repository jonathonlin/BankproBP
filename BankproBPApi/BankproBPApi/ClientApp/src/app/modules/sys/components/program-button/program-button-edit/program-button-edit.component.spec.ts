import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramButtonEditComponent } from './program-button-edit.component';

describe('ProgramButtonEditComponent', () => {
  let component: ProgramButtonEditComponent;
  let fixture: ComponentFixture<ProgramButtonEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProgramButtonEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramButtonEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
