import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VotemachineComponent } from './votemachine.component';

describe('VotemachineComponent', () => {
  let component: VotemachineComponent;
  let fixture: ComponentFixture<VotemachineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VotemachineComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VotemachineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
