import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HandheldinventoryComponent } from './handheldinventory.component';

describe('HandheldinventoryComponent', () => {
  let component: HandheldinventoryComponent;
  let fixture: ComponentFixture<HandheldinventoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HandheldinventoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HandheldinventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
