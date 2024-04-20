import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MembershipRegistrationComponent } from './membership-registration.component';

describe('MembershipRegistrationComponent', () => {
  let component: MembershipRegistrationComponent;
  let fixture: ComponentFixture<MembershipRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MembershipRegistrationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MembershipRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
