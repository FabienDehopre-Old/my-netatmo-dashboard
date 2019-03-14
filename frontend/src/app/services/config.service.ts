import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, Subject } from 'rxjs';
import { tap } from 'rxjs/operators';

import { AppConfig } from '../models/app-config';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  readonly config$: Observable<AppConfig>;
  config?: AppConfig;
  private readonly configSource: Subject<AppConfig> = new ReplaySubject<AppConfig>();

  constructor(private readonly httpClient: HttpClient) {
    this.config$ = this.configSource.asObservable();
  }

  init(): Promise<AppConfig> {
    return this.httpClient
      .get<AppConfig>(`${window.location.origin}/assets/config.json`)
      .pipe(
        tap(config => {
          this.configSource.next(config);
          this.config = config;
        })
      )
      .toPromise();
  }
}
