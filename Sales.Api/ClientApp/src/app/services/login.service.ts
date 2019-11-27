import { TokenService } from '@/services/token.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { tap } from 'rxjs/operators';
import { User } from '@/models/user';
import { RegisterRequest } from '@/models/register-request';
import { LoginRequest } from '@/models/login-request';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private readonly url = environment.apiUrl + 'user';
  private readonly subject: BehaviorSubject<User>;
  private user: User;

  constructor(
    private readonly http: HttpClient,
    private readonly tokenService: TokenService
  ) {
    this.subject = new BehaviorSubject<User>(this.user);
  }

  isUserLogged(): boolean {
    return !!this.user && !!this.tokenService.checkTokenExistence();
  }

  asObservable(): Observable<User> {
    return this.subject.asObservable();
  }

  login(email: string, password: string): Observable<any> {
    const data: LoginRequest = {
      email,
      password
    };

    const obs = this.http
      .post<User>(this.url + '/login', data)
      .pipe(tap(res => this.saveUser(res)));

    return obs;
  }

  logout(): void {
    this.tokenService.removeToken();
    this.user = null;

    this.emitChanges();
  }

  register(
    name: string,
    lastName: string,
    email: string,
    password: string
  ): Observable<any> {
    const data: RegisterRequest = {
      name,
      lastName,
      email,
      password,
      confirmPassword: password
    };

    const obs = this.http
      .post<User>(this.url + '/register', data)
      .pipe(tap(res => this.saveUser(res)));

    return obs;
  }

  private saveUser(user: User): void {
    this.user = user;
    this.tokenService.saveToken(user.token);
    this.emitChanges();
  }

  private emitChanges(): void {
    this.subject.next(this.user);
  }
}
