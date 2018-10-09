import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatrolcarsComponent } from './patrolcars.component';

describe('PatrolcarsComponent', () => {
  let component: PatrolcarsComponent;
  let fixture: ComponentFixture<PatrolcarsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatrolcarsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatrolcarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
