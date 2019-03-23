import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NetatmoCallbackErrorDialogComponent } from './netatmo-callback-error-dialog.component';

describe('NetatmoCallbackErrorDialogComponent', () => {
  let component: NetatmoCallbackErrorDialogComponent;
  let fixture: ComponentFixture<NetatmoCallbackErrorDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [NetatmoCallbackErrorDialogComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NetatmoCallbackErrorDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
