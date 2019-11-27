import { TokenService } from '@/services/token.service';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { empty, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  // In the middle of refreshing the token.
  private refreshingTokens = false;

  /**
   * Creates a new instance.
   * @param router Router.
   * @param tokenService Token service.
   */
  constructor(private router: Router, private tokenService: TokenService) {}

  /**
   * Intercepts the current request.
   * @param request Request.
   * @param next Next interceptor.
   */
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Let the refreshing, login or register continue.
    if (this.refreshingTokens || !this.tokenService.checkTokenExistence()) {
      return next.handle(request);
    }

    return next.handle(this.addAuthorizationHeader(request)).pipe(
      catchError(err => {
        if (err instanceof HttpErrorResponse && err.status === 401) {
          // Refresh token expired, go to login.
          this.tokenService.removeToken();
          this.router.navigate(['/login']);
          return empty();
        }

        return throwError(err);
      })
    );
  }

  /**
   * Adds the authorization headers.
   * @param request Request.
   */
  private addAuthorizationHeader(request: HttpRequest<any>): HttpRequest<any> {
    const clonedRequest = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.tokenService.getAuthToken()}`
      }
    });

    return clonedRequest;
  }
}
