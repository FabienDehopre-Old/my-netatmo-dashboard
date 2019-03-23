import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from './guards/auth.guard';
import { EmailVerifiedGuard } from './guards/email-verified.guard';
import { CallbackComponent } from './pages/callback/callback.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { NetatmoCallbackComponent } from './pages/netatmo-callback/netatmo-callback.component';
import { SignupSuccessComponent } from './pages/signup-success/signup-success.component';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: '', pathMatch: 'full', component: HomeComponent, canActivate: [EmailVerifiedGuard] },
      { path: 'signup-success', component: SignupSuccessComponent },
      { path: 'netatmo-callback', component: NetatmoCallbackComponent, canActivate: [EmailVerifiedGuard] },
    ],
    canActivateChild: [AuthGuard],
    canActivate: [AuthGuard],
  },
  { path: 'login', component: LoginComponent },
  { path: 'callback', component: CallbackComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
