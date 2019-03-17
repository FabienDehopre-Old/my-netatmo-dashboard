import { inject, TestBed } from '@angular/core/testing';

import { EmailVerifiedGuard } from './email-verified.guard';

describe('EmailVerifiedGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EmailVerifiedGuard],
    });
  });

  it('should ...', inject([EmailVerifiedGuard], (guard: EmailVerifiedGuard) => {
    expect(guard).toBeTruthy();
  }));
});
