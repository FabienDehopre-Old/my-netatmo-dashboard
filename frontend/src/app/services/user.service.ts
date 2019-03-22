import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import { ConfigService } from './config.service';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private readonly httpClient: HttpClient, private readonly configService: ConfigService) {}

  getProfile(): Observable<any> {
    return this.configService.config$
      .pipe(
        map(config => config.apiBaseUrl),
        switchMap(apiBaseUrl =>
          this.httpClient.get<any>(`${apiBaseUrl}/api/user`)
        )
      );
  }

  ensureCreated(): Observable<boolean> {
    return this.configService.config$
      .pipe(
        map(config => config.apiBaseUrl),
        switchMap(apiBaseUrl =>
          this.httpClient.post<boolean>(`${apiBaseUrl}/api/user/ensure`, {})
        )
      );
  }

  resendVerificationEmail(): Observable<string> {
    return this.configService.config$
      .pipe(
        map(config => config.apiBaseUrl),
        switchMap(apiBaseUrl =>
          this.httpClient.post<string>(`${apiBaseUrl}/api/user/verification-email`, {})
        )
      );
  }

  toggleFetchAndUpdateJob(): Observable<boolean> {
    return this.configService.config$
      .pipe(
        map(config => config.apiBaseUrl),
        switchMap(apiBaseUrl =>
          this.httpClient.post<boolean>(`${apiBaseUrl}/api/user/toggle-update-job`, {})
        )
      );
  }
}
