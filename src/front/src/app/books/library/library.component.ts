import {Component, OnInit} from '@angular/core';
import {Book} from "../../models/book";
import {ActivatedRoute} from "@angular/router";
import {BookService} from "../book/book.service";
import {RentalService} from "../rental/rental.service";
import {tap} from "rxjs";

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent  implements OnInit {
  list: Array<Book> = [];

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    private rentalService: RentalService) {
  }

  ngOnInit() {
    this.bookService.getAllBooks().subscribe(list => this.list = list)
  }

  rentBook(isbn: string) {
    this.bookService.getFreeBook(isbn)
      .pipe(tap(x => console.log(x)))
      .subscribe(book => this.rentalService.rentBook(book.id).pipe(tap(x => console.log(x))).subscribe())
  }

  loadDefault(event: Event) {
    (event.target as HTMLImageElement).src = "assets/no-image.jpg"
  }
}
