import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, ErrorHandler, NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import * as Sentry from '@sentry/browser';

import { environment } from '../environments/environment';
import { VERSION } from '../version';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './material.module';
import { CallbackComponent } from './pages/callback/callback.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { SentryErrorHandler } from './services/sentry-error-handler';
import { ConfigService } from './services/config.service';
import { LayoutComponent } from './components/layout/layout.component';

Sentry.init({
  dsn: 'https://2dac4e9cc7814299add137cbfd63b940@sentry.io/1413628',
  environment: environment.production ? 'production' : 'development',
  debug: !environment.production,
  release: environment.production ? VERSION : undefined
});

export function initApp(configService: ConfigService): () => void {
  return () => configService.init();
}

@NgModule({
  declarations: [AppComponent, HomeComponent, CallbackComponent, LoginComponent, LayoutComponent],
  imports: [BrowserAnimationsModule, AppRoutingModule, HttpClientModule, MaterialModule],
  providers: [
    { provide: ErrorHandler, useClass: SentryErrorHandler },
    { provide: APP_INITIALIZER, useFactory: initApp, deps: [ConfigService], multi: true }
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
