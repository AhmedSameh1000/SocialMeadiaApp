import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../Services/Auth/auth.service';

export const isloginGuard: CanActivateFn = (route, state) => {
  return inject(AuthService).loggedIn()?false||inject(Router).navigate([""]):true;
};
