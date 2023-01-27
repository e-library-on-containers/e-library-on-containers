import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {User} from "../../models/user";
import {catchError, EMPTY, from, map, Observable, shareReplay, tap} from "rxjs";
import {MatSnackBar} from '@angular/material/snack-bar';
import {TokenResponse} from "../../models/tokenResponse"
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,
              private snackBar: MatSnackBar,
              private jwtHelper: JwtHelperService) {
  }

  login(email: string, password: string) {
    return this.http.post<TokenResponse>('https://elib.zajaczkowski.dev/identity' + '/Auth/authenticate', {
        email: email,
        password: password
      })
      .pipe(
        map(response => response.token),
        tap(token => localStorage.setItem('access_token', token)),
        map(token => this.jwtHelper.decodeToken(token)),
        shareReplay()
      );
  }

  createAccount(user: User): Observable<boolean> {
    return this.http.post('https://elib.zajaczkowski.dev/identity' + '/Account/register', user)
      .pipe(
        map(() => true),
        catchError((error: HttpErrorResponse) => {
          this.snackBar.open(error.message, '', {duration: 2000})
          return from([false]);
        }),
        tap(_ => console.log(`Created user with email: ${user.email}`))
      )
  }
}
