import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramButtonListComponent } from './program-button-list.component';

describe('ProgramButtonListComponent', () => {
  let component: ProgramButtonListComponent;
  let fixture: ComponentFixture<ProgramButtonListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProgramButtonListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramButtonListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
