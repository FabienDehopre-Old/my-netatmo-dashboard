import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, ErrorHandler, NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import * as Sentry from '@sentry/browser';

import { environment } from '../environments/environment';
import { VERSION } from '../version';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthorizeDialogComponent } from './components/authorize-dialog/authorize-dialog.component';
import { LayoutComponent } from './components/layout/layout.component';
import { NetatmoCallbackErrorDialogComponent } from './components/netatmo-callback-error-dialog/netatmo-callback-error-dialog.component';
import { MaterialModule } from './material.module';
import { CallbackComponent } from './pages/callback/callback.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { NetatmoCallbackComponent } from './pages/netatmo-callback/netatmo-callback.component';
import { SignupSuccessComponent } from './pages/signup-success/signup-success.component';
import { AltitudePipe } from './pipes/altitude.pipe';
import { AuthInterceptor } from './services/auth.interceptor';
import { ConfigService } from './services/config.service';
import { SentryErrorHandler } from './services/sentry-error-handler';

Sentry.init({
  dsn: 'https://2dac4e9cc7814299add137cbfd63b940@sentry.io/1413628',
  environment: environment.production ? 'production' : 'development',
  debug: !environment.production,
  release: environment.production ? VERSION : undefined,
});

export function initApp(configService: ConfigService): () => void {
  return () => configService.init();
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CallbackComponent,
    LoginComponent,
    LayoutComponent,
    AltitudePipe,
    SignupSuccessComponent,
    AuthorizeDialogComponent,
    NetatmoCallbackComponent,
    NetatmoCallbackErrorDialogComponent,
  ],
  imports: [BrowserAnimationsModule, AppRoutingModule, HttpClientModule, MaterialModule],
  providers: [
    { provide: ErrorHandler, useClass: SentryErrorHandler },
    { provide: APP_INITIALIZER, useFactory: initApp, deps: [ConfigService], multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
