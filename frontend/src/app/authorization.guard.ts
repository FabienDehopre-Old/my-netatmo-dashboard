import { Injectable } from '@angular/core';
import {
  CanActivate,
  CanLoad,
  Router
} from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationGuard implements CanActivate, CanLoad {
  constructor(private readonly router: Router, private readonly oidcSecurityService: OidcSecurityService) {}

  canActivate(): Observable<boolean> {
    return this.checkUser();
  }

  canLoad(): Observable<boolean> {
    return this.checkUser();
  }

  private checkUser(): Observable<boolean> {
    return this.oidcSecurityService.getIsAuthorized()
      .pipe(
        tap(isAuthorized => {
          if (!isAuthorized) {
            this.router.navigate(['/unauthorized']);
          }
        })
      )
  }
}
