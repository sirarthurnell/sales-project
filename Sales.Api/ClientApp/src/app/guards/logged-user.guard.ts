import { LoginService } from '@/services/login.service';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree
} from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoggedUserGuard implements CanActivate {
  /**
   * Creates a new instance.
   */
  constructor(private logingService: LoginService, private router: Router) {}

  /**
   * Checks if the specified route can be activated.
   * @param route Route
   * @param state State
   */
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (!this.logingService.isUserLogged()) {
      this.logingService.logout();
      this.router.navigate(['login']);
    }
    return this.logingService.isUserLogged();
  }
}
