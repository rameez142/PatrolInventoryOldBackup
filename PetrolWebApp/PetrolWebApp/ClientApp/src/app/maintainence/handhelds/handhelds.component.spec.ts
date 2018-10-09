import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HandheldsComponent } from './handhelds.component';

describe('HandheldsComponent', () => {
  let component: HandheldsComponent;
  let fixture: ComponentFixture<HandheldsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HandheldsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HandheldsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
