import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintainencePatrolComponent } from './maintainencepatrols.component';

describe('MaintainencePatrolComponent', () => {
  let component: MaintainencePatrolComponent;
  let fixture: ComponentFixture<MaintainencePatrolComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaintainencePatrolComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaintainencePatrolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
