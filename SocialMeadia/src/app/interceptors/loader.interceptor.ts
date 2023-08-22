import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable, finalize } from 'rxjs';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {

  constructor(private spinner:NgxSpinnerService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.spinner.show();
    return next.handle(request).pipe(
      finalize(() => {
        this.spinner.hide();
      })
    );
  }
}
