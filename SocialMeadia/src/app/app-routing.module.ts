import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { isloginGuard } from './Guards/islogin.guard';

const routes: Routes = [

  {
    path: '',
    component:HomeComponent
  },
  {
    path: 'loginApp',
    loadChildren: () => import('./login-register/login-register.module').then(m => m.LoginRegisterModule),
 
  },
  {
    path: 'User',
    loadChildren: () => import('./user/user.module').then(m => m.UserModule),
  },
  {
    path: 'membersModule',
    loadChildren: () => import('./members/members.module').then(m => m.MembersModule),
  
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
