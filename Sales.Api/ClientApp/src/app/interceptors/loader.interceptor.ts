import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs/operators';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {
  private shouldShowSpinner = true;

  /**
   * Creates a new instance.
   */
  constructor(private spinner: NgxSpinnerService) {}

  /**
   * Intercepts the request.
   * @param req Request
   * @param next Next interceptor
   */
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    setTimeout(() => {
      if (this.shouldShowSpinner) {
        this.spinner.show();
      }
    }, 2000);

    this.shouldShowSpinner = true;

    return next.handle(req).pipe(
      finalize(() => {
        this.shouldShowSpinner = false;
        this.spinner.hide();
      })
    );
  }
}
