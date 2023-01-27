import { Injectable } from '@angular/core';
import {Book} from "../../models/book";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor() { }

  getAllBooks(): Observable<Array<Book>> {
    return new Observable<Array<Book>>(
      subscriber => subscriber.next([
        {
          isbn: "2222222222222",
          title: "Przewodowe i bezprzewodowe sieci LAN",
          author: "Krzysztof Nowicki, Janusz Koran Mekka",
          description: "Powieść przygodowa o wysyłaniu ramkji Shreka",
          isAvailable: true,
          imgSource: "ni ma"
        } as Book,
        {
          isbn: "1111111111111",
          title: "Łagodne wprowadzenie do algorytmów",
          author: "Marek Kubale, Inny Ziomek",
          description: "XD",
          isAvailable: false,
          imgSource: "ni ma"
        } as Book
      ])
    )
  }
}
