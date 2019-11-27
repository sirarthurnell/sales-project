import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginService } from '@/services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, OnDestroy {
  private loginSubscription: Subscription;

  /**
   * Contains the username.
   */
  name = 'sirarthurnell';

  /**
   * Contains the username.
   */
  lastName = 'sirarthurnell';

  /**
   * Contains the email of the user.
   */
  email = 'sirarthurnell@gmail.com';

  /**
   * Contains the password.
   */
  password = 'Passw0rd*';

  /**
   * Validation params.
   */
  validation = environment.validationParams;

  /**
   * Creates a new instance.
   * @param loginService Login service.
   */
  constructor(private loginService: LoginService, private router: Router) {}

  /**
   * Subscribes to user login changes.
   */
  ngOnInit(): void {
    this.loginSubscription = this.loginService
      .asObservable()
      .subscribe(user => {
        if (user) {
          this.goToSalesPage();
        }
      });
  }

  /**
   * Unsubscribes from login changes.
   */
  ngOnDestroy(): void {
    this.loginSubscription.unsubscribe();
  }

  /**
   * Registers the user.
   */
  register(): void {
    this.loginService.register(
      this.name,
      this.lastName,
      this.email,
      this.password
    ).subscribe(_ => this.goToSalesPage());
  }

  /**
   * Redirects to sales page.
   */
  private goToSalesPage(): void {
    this.router.navigate(['/sales']);
  }
}
