import { Injectable } from '@angular/core';
import {AppConfigService} from "../../config/app-config.service";
import {catchError, from, map, Observable} from "rxjs";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Rent} from "../../models/rent";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class RentalService {
  private readonly apiUrl: string;

  constructor(
    private snackBar: MatSnackBar,
    private http: HttpClient) {
    this.apiUrl = AppConfigService.getApiUrl()
  }

  getAllRentals(): Observable<Array<Rent>> {
    return this.http.get(`${this.apiUrl}/rentals`).pipe(
      catchError((error: HttpErrorResponse) => {
        this.snackBar.open(error.error.message, '', {duration: 2000})
        return from([]);
      }),
      map(response => Object.assign([], response)),
      map(response => response.map(rent => rent as Rent))
    )
  }

  rentBook(bookCopyId: number) {
    return this.http.post(`${this.apiUrl}/rentals/rent`, { bookInstanceId: bookCopyId }).pipe(
      catchError((error: HttpErrorResponse) => {
        this.snackBar.open(error.error.message, '', {duration: 2000})
        return from([]);
      })
    )
  }

  returnBook(rentId: string) {
    return this.http.post(`${this.apiUrl}/rentals/${rentId}/return`).pipe(
      catchError((error: HttpErrorResponse) => {
        this.snackBar.open(error.error.message, '', {duration: 2000})
        return from([]);
      })
    )
  }
}
