import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import { NETATMO_RETURN_URL, NETATMO_STATE } from '../models/consts';

import { ConfigService } from './config.service';

export function isAuthorizeUrl(value: any): value is { url: string; state: string } {
  return value != undefined && 'url' in value && 'state' in value;
}

@Injectable({ providedIn: 'root' })
export class NetatmoService {
  constructor(private readonly httpClient: HttpClient, private readonly configService: ConfigService) {}

  buildAuthorizeUrl(): Observable<{ url: string; state: string }> {
    return this.configService.config$.pipe(
      map(config => config.apiBaseUrl),
      switchMap(apiBaseUrl =>
        this.httpClient.get<{ url: string; state: string }>(`${apiBaseUrl}/api/netatmo?returnUrl=${NETATMO_RETURN_URL}`)
      )
    );
  }

  exchangeCodeForAccessToken(state: string | null, code: string | null, error: string | null): Observable<void> {
    const sessionStorageState = sessionStorage.getItem(NETATMO_STATE);
    sessionStorage.removeItem(NETATMO_STATE);
    if (error != undefined) {
      return throwError(new Error(error));
    } else if (state !== sessionStorageState) {
      return throwError(new Error('invalid_state'));
    }

    return this.configService.config$.pipe(
      map(config => config.apiBaseUrl),
      switchMap(apiBaseUrl =>
        this.httpClient.post<void>(`${apiBaseUrl}/api/netatmo/exchange-code`, { code, returnUrl: NETATMO_RETURN_URL })
      )
    );
  }
}
