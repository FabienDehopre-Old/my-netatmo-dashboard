import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  CanLoad,
  Route,
  Router,
  RouterStateSnapshot,
  UrlSegment,
} from '@angular/router';

import { AuthService, RETURN_URL } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private readonly authService: AuthService, private readonly router: Router) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.canActivateChild(next, state);
  }

  canActivateChild(_: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.isAuthenticated(state.url);
  }

  canLoad(route: Route, segments: UrlSegment[]): boolean {
    // tslint:disable-next-line:no-console
    console.log('canLoad', route, segments);
    return this.isAuthenticated('/');
  }

  private isAuthenticated(returnUrl: string): boolean {
    if (!this.authService.isAuthenticated()) {
      localStorage.setItem(RETURN_URL, returnUrl);
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
}
