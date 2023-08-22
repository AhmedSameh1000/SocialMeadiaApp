import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
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
    const modifiedReq = request.clone({
      headers: request.headers.set('Authorization', `Bearer ${userToken}`),
    });
    return next.handle(modifiedReq);
  }
}
