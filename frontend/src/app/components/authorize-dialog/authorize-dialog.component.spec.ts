import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizeDialogComponent } from './authorize-dialog.component';

describe('AuthorizeDialogComponent', () => {
  let component: AuthorizeDialogComponent;
  let fixture: ComponentFixture<AuthorizeDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AuthorizeDialogComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizeDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
