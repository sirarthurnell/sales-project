import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoggedUserGuard } from '@/guards/logged-user.guard';
import { LoginComponent } from '@/pages/login/login.component';
import { RegisterComponent } from '@/pages/register/register.component';
import { SalesComponent } from '@/pages/sales/sales.component';
import { LogoutComponent } from '@/pages/logout/logout.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, data: { toolbar: false } },
  {
    path: 'register',
    component: RegisterComponent,
    data: { toolbar: false }
  },
  {
    path: 'sales',
    component: SalesComponent,
    canActivate: [LoggedUserGuard]
  },
  {
    path: 'logout',
    component: LogoutComponent,
    canActivate: [LoggedUserGuard]
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
