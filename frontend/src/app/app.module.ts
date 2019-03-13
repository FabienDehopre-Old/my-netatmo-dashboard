import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import * as Sentry from '@sentry/browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { SentryErrorHandler } from './sentry-error-handler';
import { ConfigService } from './services/config.service';
import { CallbackComponent } from './pages/callback/callback.component';
import { LoginComponent } from './pages/login/login.component';

Sentry.init({
  dsn: 'https://2dac4e9cc7814299add137cbfd63b940@sentry.io/1413628',
});

export function initApp(configService: ConfigService): () => void {
  return () => configService.init();
}

@NgModule({
  declarations: [AppComponent, HomeComponent, CallbackComponent, LoginComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [
    { provide: ErrorHandler, useClass: SentryErrorHandler },
    { provide: APP_INITIALIZER, useFactory: initApp, deps: [ConfigService], multi: true }
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
