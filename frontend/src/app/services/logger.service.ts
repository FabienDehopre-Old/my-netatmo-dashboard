import { Injectable } from '@angular/core';
import { first, map } from 'rxjs/operators';

import { convertToLogLevel, LogLevel } from '../models/log-level';

import { ConfigService } from './config.service';

// tslint:disable:no-console

@Injectable({
  providedIn: 'root'
})
export class LoggerService {
  private level: LogLevel = LogLevel.off;
  
  constructor(configService: ConfigService) {
    configService.config$
      .pipe(
        first(),
        map(config => config.logLevel),
        map(convertToLogLevel)
      )
      .subscribe(level => (this.level = level));
  }

  clear(): void {
    console.clear();
  }

  trace(message: string, ...optionalParams: any[]): void {
    if (this.level <= LogLevel.trace) {
      console.trace(message, ...optionalParams);
    }
  }

  debug(message: string, ...optionalParams: any[]): void {
    if (this.level <= LogLevel.debug) {
      console.debug(message, ...optionalParams);
    }
  }

  info(message: string, ...optionalParams: any[]): void {
    if (this.level <= LogLevel.info) {
      console.info(message, ...optionalParams);
    }
  }

  warn(message: string, ...optionalParams: any[]): void {
    if (this.level <= LogLevel.warn) {
      console.warn(message, ...optionalParams);
    }
  }

  error(message: string, ...optionalParams: any[]): void {
    if (this.level <= LogLevel.error) {
      console.error(message, ...optionalParams);
    }
  }
}
