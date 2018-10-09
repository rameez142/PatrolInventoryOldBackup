import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessoryinventoryComponent } from './accessoryinventory.component';

describe('AccessoryinventoryComponent', () => {
  let component: AccessoryinventoryComponent;
  let fixture: ComponentFixture<AccessoryinventoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccessoryinventoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessoryinventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
