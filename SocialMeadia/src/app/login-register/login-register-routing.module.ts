import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { isloginGuard } from '../Guards/islogin.guard';

const routes: Routes = [

    {
    path: 'login',
    component:LoginComponent,
    canActivate:[isloginGuard]
  },
  {
    path: 'signup',
    component:RegisterComponent,
    canActivate:[isloginGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoginRegisterRoutingModule { }
