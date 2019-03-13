import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import * as Sentry from "@sentry/browser";
import {
  AuthModule, AuthWellKnownEndpoints,
  OidcConfigService,
  OidcSecurityService,
  OpenIDImplicitFlowConfiguration,
} from 'angular-auth-oidc-client';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { SentryErrorHandler } from './sentry-error-handler';

Sentry.init({
  dsn: "https://2dac4e9cc7814299add137cbfd63b940@sentry.io/1413628"
});

export function loadConfig(oidcConfigService: OidcConfigService): () => void {
  return () => oidcConfigService.load(`${window.location.origin}/assets/oidc-implicit-flow-config.json`);
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule.forRoot()
  ],
  providers: [
    { provide: ErrorHandler, useClass: SentryErrorHandler },
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: loadConfig,
      deps: [OidcConfigService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(
    private readonly oidcSecurityService: OidcSecurityService,
    private readonly oidcConfigService: OidcConfigService
  ) {
    this.oidcConfigService.onConfigurationLoaded.subscribe(() => {
      const oidcImplicitFlowConfig = new OpenIDImplicitFlowConfiguration();
      oidcImplicitFlowConfig.stsServer = this.oidcConfigService.clientConfiguration.stsServer;
      oidcImplicitFlowConfig.redirect_url = this.oidcConfigService.clientConfiguration.redirect_url;
      oidcImplicitFlowConfig.client_id = this.oidcConfigService.clientConfiguration.client_id;
      oidcImplicitFlowConfig.response_type = this.oidcConfigService.clientConfiguration.response_type;
      oidcImplicitFlowConfig.scope = this.oidcConfigService.clientConfiguration.scope;
      oidcImplicitFlowConfig.post_logout_redirect_uri = this.oidcConfigService.clientConfiguration.post_logout_redirect_uri;
      oidcImplicitFlowConfig.start_checksession = this.oidcConfigService.clientConfiguration.start_checksession;
      oidcImplicitFlowConfig.silent_renew = this.oidcConfigService.clientConfiguration.silent_renew;
      oidcImplicitFlowConfig.silent_renew_url = this.oidcConfigService.clientConfiguration.silent_renew_url;
      oidcImplicitFlowConfig.post_login_route = this.oidcConfigService.clientConfiguration.post_login_route;
      oidcImplicitFlowConfig.forbidden_route = this.oidcConfigService.clientConfiguration.forbidden_route;
      oidcImplicitFlowConfig.unauthorized_route = this.oidcConfigService.clientConfiguration.unauthorized_route;
      oidcImplicitFlowConfig.log_console_debug_active = this.oidcConfigService.clientConfiguration.log_console_debug_active;
      oidcImplicitFlowConfig.log_console_warning_active = this.oidcConfigService.clientConfiguration.log_console_warning_active;
      oidcImplicitFlowConfig.max_id_token_iat_offset_allowed_in_seconds = this.oidcConfigService.clientConfiguration.max_id_token_iat_offset_allowed_in_seconds;

      const authWellKnownEndpoints = new AuthWellKnownEndpoints();
      authWellKnownEndpoints.setWellKnownEndpoints(this.oidcConfigService.wellKnownEndpoints);

      this.oidcSecurityService.setupModule(
        oidcImplicitFlowConfig,
        authWellKnownEndpoints
      );
    });
  }
}
