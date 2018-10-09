import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceinventoryComponent } from './deviceinventory.component';

describe('DeviceinventoryComponent', () => {
  let component: DeviceinventoryComponent;
  let fixture: ComponentFixture<DeviceinventoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeviceinventoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeviceinventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
