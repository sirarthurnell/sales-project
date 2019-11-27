import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private readonly authTokenKey = 'auth_token';

  saveToken(token: string): void {
    localStorage.setItem(this.authTokenKey, token);
  }

  getAuthToken(): string | null {
    const token = localStorage.getItem(this.authTokenKey);
    return token;
  }

  checkTokenExistence(): boolean {
    return !!this.getAuthToken();
  }

  removeToken(): void {
    localStorage.removeItem(this.authTokenKey);
  }
}
