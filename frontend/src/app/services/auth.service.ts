import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as Sentry from '@sentry/browser';
import * as auth0 from 'auth0-js';
import { Observable, of, Subscription, throwError, timer } from 'rxjs';
import { first, map, switchMap, withLatestFrom } from 'rxjs/operators';

import { ACCESS_TOKEN, EXPIRES_AT, ID_TOKEN, RETURN_URL } from '../models/consts';

import { ConfigService } from './config.service';
import { LoggerService } from './logger.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly webAuth$: Observable<auth0.WebAuth>;
  private refreshSubscription?: Subscription;

  constructor(
    private readonly configService: ConfigService,
    private readonly logger: LoggerService,
    private readonly router: Router,
    private readonly http: HttpClient
  ) {
    this.webAuth$ = this.configService.config$.pipe(
      map(
        config =>
          new auth0.WebAuth({
            clientID: config.auth0.clientId,
            domain: config.auth0.domain,
            responseType: 'token id_token',
            audience: 'https://netatmo.dehopre.com/api',
            redirectUri: `${window.location.origin}/callback`,
            scope: 'openid profile email update:users',
          })
      )
    );
    this.scheduleRenewal();
  }

  login(): void {
    this.webAuth$.pipe(first()).subscribe(webAuth => webAuth.authorize());
  }

  handleAuthentication(): void {
    this.webAuth$.pipe(first()).subscribe(webAuth =>
      webAuth.parseHash((err, authResult) => {
        if (err != undefined) {
          this.logger.error('Authentication error (see details for more info):', err);
          if (err instanceof Error) {
            Sentry.captureException(err);
          } else {
            const error = err as auth0.Auth0Error;
            Sentry.captureException(error.original || error);
          }

          return;
        }

        this.setSession(authResult || undefined, true); // the default is null so we need to convert it to undefined.
      })
    );
  }

  logout(returnTo: string): void {
    this.unscheduleRenewal();
    localStorage.removeItem(ACCESS_TOKEN);
    localStorage.removeItem(ID_TOKEN);
    localStorage.removeItem(EXPIRES_AT);
    localStorage.removeItem(RETURN_URL);
    this.webAuth$
      .pipe(
        withLatestFrom(this.configService.config$.pipe(map(config => config.auth0.clientId))),
        first()
      )
      .subscribe(([webAuth, clientID]) => {
        const options: auth0.LogoutOptions = { returnTo, clientID };
        webAuth.logout(options);
      });
  }

  getUserInfo(): Observable<auth0.Auth0UserProfile> {
    const accessToken = localStorage.getItem(ACCESS_TOKEN);
    if (!accessToken) {
      return throwError(new Error('Access token must exist to fetch user info.'));
    }

    return this.webAuth$.pipe(
      switchMap(
        webAuth =>
          new Observable<auth0.Auth0UserProfile>(observer =>
            webAuth.client.userInfo(accessToken, (err, userInfo) => {
              if (err) {
                observer.error(err);
              } else {
                observer.next(userInfo);
                observer.complete();
              }
            })
          )
      )
    );
  }

  isAuthenticated(): boolean {
    const expiresAt = JSON.parse(localStorage.getItem(EXPIRES_AT) || '0');
    return expiresAt > Date.now();
  }

  resendVerificationEmail(): Observable<void> {
    return this.configService.config$
      .pipe(
        switchMap(config => this.http.post(`${config.apiBaseUrl}/api/user/verification-email`, {})),
        map(() => void(0))
      );
  }

  private scheduleRenewal(): void {
    const expiresAt = JSON.parse(localStorage.getItem(EXPIRES_AT) || '0');
    if (expiresAt <= Date.now()) {
      // access token is already expired
      return;
    }

    this.logger.info('Schedule the refresh of the tokens.');
    this.unscheduleRenewal();
    this.refreshSubscription = of(expiresAt)
      .pipe(switchMap(value => timer(Math.max(1, (value - Date.now()) / 2))))
      .subscribe(() => this.renewAuthentication());
  }

  private unscheduleRenewal(): void {
    if (this.refreshSubscription != undefined) {
      this.logger.info('Unschedule the refresh of the tokens');
      this.refreshSubscription.unsubscribe();
      this.refreshSubscription = undefined;
    }
  }

  private renewAuthentication(): void {
    this.webAuth$.pipe(first()).subscribe(webAuth => {
      this.logger.info('Renewing access token using silent authentication.');
      const options: auth0.RenewAuthOptions = { redirectUri: `${window.location.origin}/silent-refresh.html`, usePostMessage: true };
      webAuth.renewAuth(options, (err, result) => {
        if (err != undefined) {
          this.logger.error('Failed to renew access token (see details for more info):', err);
          if (err instanceof Error) {
            Sentry.captureException(err);
          } else {
            const error = err;
            Sentry.captureMessage(`[${error.error}] ${error.errorDescription}`, Sentry.Severity.Error);
            // TODO: capture error.original ???
          }

          return;
        }

        this.setSession(result || undefined);
      });
    });
  }

  private setSession(authResult?: auth0.Auth0DecodedHash, redirect: boolean = false): void {
    if (authResult && authResult.idToken && authResult.accessToken && authResult.expiresIn) {
      const expiresAt = JSON.stringify(authResult.expiresIn * 1000 + Date.now());
      localStorage.setItem(ACCESS_TOKEN, authResult.accessToken);
      localStorage.setItem(ID_TOKEN, authResult.idToken);
      localStorage.setItem(EXPIRES_AT, expiresAt);
      this.scheduleRenewal();
      if (redirect) {
        this.redirect();
      }
    } else {
      this.logger.warn('No authentication has occurred?', authResult);
    }
  }

  private redirect(): void {
    const returnUrl = localStorage.getItem(RETURN_URL);
    if (returnUrl != undefined) {
      localStorage.removeItem(RETURN_URL);
      this.router.navigateByUrl(returnUrl);
    } else {
      this.router.navigate(['/']);
    }
  }
}
