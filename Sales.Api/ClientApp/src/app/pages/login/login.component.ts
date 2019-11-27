import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginService } from '@/services/login.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  private loginSubscription: Subscription;

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
  constructor(
    private loginService: LoginService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  /**
   * Subscribes to user login changes.
   */
  ngOnInit(): void {
    this.loginSubscription = this.loginService
      .asObservable()
      .subscribe(user => {
        // ? Maybe always null the first time.
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
   * Logins the user.
   */
  login(): void {
    this.loginService.login(this.email, this.password).subscribe(
      _ => {
        this.toastr.success('Login successful');
      },
      _ => {
        this.toastr.error('User not found.');
      }
    );
  }

  /**
   * Redirects to sales page.
   */
  private goToSalesPage(): void {
    this.router.navigate(['/sales']);
  }
}
