import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddvoterComponent } from './addvoter.component';

describe('AddvoterComponent', () => {
  let component: AddvoterComponent;
  let fixture: ComponentFixture<AddvoterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddvoterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddvoterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
