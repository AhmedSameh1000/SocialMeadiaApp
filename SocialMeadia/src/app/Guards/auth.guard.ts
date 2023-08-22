import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../Services/Auth/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
   return inject(AuthService).loggedIn()?true: inject(ToastrService).success("log in First")&&false
};
