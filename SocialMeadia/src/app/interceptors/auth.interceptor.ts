import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpParams,
  HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../Services/Auth/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private AuthServices:AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
      const userToken = this.AuthServices.getToken();
      if(this.AuthServices.loggedIn()){
        request = request.clone({
        setHeaders:{
          Authorization: `Bearer ${userToken}`
        },
        setParams:{
          "access_token": `${userToken}`
        }
        });
      }
    return next.handle(request);
  }
}
