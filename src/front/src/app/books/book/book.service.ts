import { Injectable } from '@angular/core';
import {Book} from "../../models/book";
import {map, Observable} from "rxjs";
import {AppConfigService} from "../../config/app-config.service";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = AppConfigService.getApiUrl()
  }

  getAllBooks(): Observable<Array<Book>> {
    return this.http.get(`${this.apiUrl}/books`).pipe(
      map(response => Object.assign([], response)),
      map(response => response.map(book => book as Book))
    )
  }
}
