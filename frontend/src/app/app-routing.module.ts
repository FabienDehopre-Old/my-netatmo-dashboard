import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthorizationGuard } from './authorization.guard';
import { HomeComponent } from './pages/home/home.component';

const routes: Routes = [{ path: '', pathMatch: 'full', component: HomeComponent, canActivate: [AuthorizationGuard] }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
