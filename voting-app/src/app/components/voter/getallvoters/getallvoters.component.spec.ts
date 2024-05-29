import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetallvotersComponent } from './getallvoters.component';

describe('GetallvotersComponent', () => {
  let component: GetallvotersComponent;
  let fixture: ComponentFixture<GetallvotersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetallvotersComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GetallvotersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
