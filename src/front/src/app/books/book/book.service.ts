import { Injectable } from '@angular/core';
import {Book} from "../../models/book";
import {catchError, filter, from, map, Observable} from "rxjs";
import {AppConfigService} from "../../config/app-config.service";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {BookCopy} from "../../models/bookCopy";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl: string;

  constructor(
    private snackBar: MatSnackBar,
    private http: HttpClient) {
    this.apiUrl = AppConfigService.getApiUrl()
  }

  getBook(isbn: string): Observable<Book> {
    return this.http.get(`${this.apiUrl}/books/${isbn}`).pipe(
      map(response => response as Book)
    )
  }

  getFreeBook(isbn: string): Observable<BookCopy> {
    return this.http.get(`${this.apiUrl}/books-copies?isbn=${isbn}&isAvailable=true`).pipe(
      catchError((error: HttpErrorResponse) => {
        this.snackBar.open(error.error.message, '', {duration: 2000})
        return from([]);
      }),
      map(response => Object.assign([], response)),
      map(response => response.map(book => book as BookCopy)),
      filter(response => response.length > 0),
      map(response => response.filter(book => book.isAvailable)),
      map(response => response[0]),
      map(response => response as BookCopy)
    )
  }

  getAllBooks(): Observable<Array<Book>> {
    return this.http.get(`${this.apiUrl}/books`).pipe(
      catchError((error: HttpErrorResponse) => {
        this.snackBar.open(error.error.message, '', {duration: 2000})
        return from([]);
      }),
      map(response => Object.assign([], response)),
      map(response => response.map(book => book as Book))
    )
  }

  getAllBookCopies(): Observable<Array<BookCopy>> {
    return this.http.get(`${this.apiUrl}/books-copies`).pipe(
      catchError((error: HttpErrorResponse) => {
        this.snackBar.open(error.error.message, '', {duration: 2000})
        return from([]);
      }),
      map(response => Object.assign([], response)),
      map(response => response.map(book => book as BookCopy))
    )
  }
}
