import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {User} from "../../models/user";
import {catchError, from, map, Observable, shareReplay, tap} from "rxjs";
import {MatSnackBar} from '@angular/material/snack-bar';
import {TokenResponse} from "../../models/tokenResponse"
import {JwtHelperService} from "@auth0/angular-jwt";
import {AppConfigService} from "../../config/app-config.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl: string;

  constructor(private http: HttpClient,
              private snackBar: MatSnackBar,
              private jwtHelper: JwtHelperService) {
      this.apiUrl = AppConfigService.getApiUrl()
  }

  login(email: string, password: string) {
    return this.http.post<TokenResponse>(`${this.apiUrl}/identity/auth`, {
        email: email,
        password: password
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.snackBar.open(error.error.message, '', {duration: 2000})
          return from([]);
        }),
        map(response => response.token),
        tap(token => localStorage.setItem('access_token', token)),
        map(token => this.jwtHelper.decodeToken(token)),
        shareReplay()
      );
  }

  createAccount(user: User): Observable<boolean> {
    return this.http.post(`${this.apiUrl}/identity/register`, user)
      .pipe(
        map(() => true),
        catchError((error: HttpErrorResponse) => {
          this.snackBar.open(error.error.message, '', {duration: 2000})
          return from([false]);
        })
      )
  }
}
