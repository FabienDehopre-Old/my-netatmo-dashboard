import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, CanLoad, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class EmailVerifiedGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private readonly authService: AuthService, private readonly router: Router) {}

  canActivate(): Observable<boolean> {
    return this.canActivateChild();
  }

  canActivateChild(): Observable<boolean> {
    return this.checkEmailValidated();
  }

  canLoad(): Observable<boolean> {
    return this.checkEmailValidated();
  }

  checkEmailValidated(): Observable<boolean> {
    return this.authService.getUserInfo().pipe(
      map(userInfo => userInfo.email_verified),
      map(emailVerified => (emailVerified == undefined ? true : emailVerified)),
      tap(emailVerified => {
        if (!emailVerified) {
          this.router.navigate(['/signup-success']);
        }
      })
    );
  }
}
