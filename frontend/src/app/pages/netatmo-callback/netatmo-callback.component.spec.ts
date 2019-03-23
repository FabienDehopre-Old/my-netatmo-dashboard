import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NetatmoCallbackComponent } from './netatmo-callback.component';

describe('NetatmoCallbackComponent', () => {
  let component: NetatmoCallbackComponent;
  let fixture: ComponentFixture<NetatmoCallbackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [NetatmoCallbackComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NetatmoCallbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
