import { ErrorHandler, Injectable } from '@angular/core';
import * as Sentry from '@sentry/browser';

@Injectable()
export class SentryErrorHandler extends ErrorHandler {
  handleError(error: any): void {
    Sentry.captureException(error.originalError || error);
    super.handleError(error);
  }
}
