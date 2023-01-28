import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {BookService} from "../book/book.service";
import {RentalService} from "../rental/rental.service";
import {RentedBook} from "../../models/rentedBook";
import {filter} from "rxjs";

@Component({
  selector: 'app-rentals',
  templateUrl: './rentals.component.html',
  styleUrls: ['./rentals.component.css']
})
export class RentalsComponent implements OnInit {
  list: Array<RentedBook> = [];

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    private rentalService: RentalService) {
  }

  ngOnInit() {
    this.rentalService.getAllRentals().subscribe(
      rental => {
        this.list = []
        rental
          .map(rent => this.bookService.getAllBookCopies()
            .pipe(filter(copies => copies.length > 0))
            .subscribe(copies => {
                const copy = copies.find(copy => copy.id == rent.bookCopyId);
                if (copy !== undefined) {
                  this.bookService.getBook(copy.isbn).subscribe(book => {
                    const rentedBook = {
                      rentId: rent.id,
                      bookCopyId: rent.bookCopyId,
                      userId: rent.userId,
                      coverImg: book.coverImg,
                      title: book.title,
                      authors: book.authors,
                      isbn: book.isbn
                    } as RentedBook;
                    this.list = [...this.list, rentedBook]
                  })
                }
              }
            )
          )
      }
    )
  }

  returnBook(rentalId: string) {
    this.rentalService.returnBook(rentalId).subscribe()
  }

  loadDefault(event: Event) {
    (event.target as HTMLImageElement).src = "assets/no-image.jpg"
  }
}
