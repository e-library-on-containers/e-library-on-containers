import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {LoginComponent} from './auth/login/login.component';
import {PageNotFoundComponent} from "./page-not-found/page-not-found.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RentalsComponent} from './books/rentals/rentals.component';
import {LibraryComponent} from './books/library/library.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {JwtInterceptor, JwtModule} from "@auth0/angular-jwt";
import {SignupComponent} from './auth/signup/signup.component';
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { APP_BASE_HREF } from '@angular/common';
import {environment} from "../ environments/environment";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PageNotFoundComponent,
    RentalsComponent,
    LibraryComponent,
    SignupComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatSnackBarModule,
    BrowserAnimationsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: jwtTokenGetter,
        headerName: 'Authorization',
        authScheme: 'Bearer '
      }
    })
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: APP_BASE_HREF, useValue: environment.baseHref}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

export function jwtTokenGetter() {
  return localStorage.getItem('access_token');
}
