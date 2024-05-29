import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetallcandidatesComponent } from './getallcandidates.component';

describe('GetallcandidatesComponent', () => {
  let component: GetallcandidatesComponent;
  let fixture: ComponentFixture<GetallcandidatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetallcandidatesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GetallcandidatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
