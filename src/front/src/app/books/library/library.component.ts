import {Component, OnInit} from '@angular/core';
import {Book} from "../../models/book";
import {RentedBook} from "../../models/rentedBook";
import {ActivatedRoute} from "@angular/router";
import {BookService} from "../book/book.service";

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent  implements OnInit {
  list: Array<Book> = [];
  rentedBooks: Array<RentedBook> = [];

  //Modal variables
  username = "";
  membership = "";
  focusedIsbn = "";
  focusedTitle = ""; //Title of book selected by user
  rentalDuration: any;
  errorMsg = "";

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService) {
  }

  ngOnInit() {
    this.bookService.getAllBooks().subscribe(list => this.list = list)
  }

  /* BOOK OPERATIONS */
  rentBook() {
    //Input Validation
    let dur = parseFloat(this.rentalDuration);
    if (!dur || !(!isNaN(this.rentalDuration) && (dur | 0) === dur)) {
      this.errorMsg = "Error: Rent Duration has to be a number";
      return;
    }
    if (this.username.length === 0) {
      this.errorMsg = "Error: Username is empty";
      return;
    }
    if (this.membership.length === 0) {
      this.errorMsg = "Error: Membership # is empty";
      return;
    }

    let retDate = new Date();
    retDate.setDate(retDate.getDate() + +this.rentalDuration);
    console.log(retDate);

    // if (this.rentedBooks &&
    //   !this.rentedBooks.map(x => x.id).includes(this.focusedIsbn)) {
    //   this.rentedBooks.push({
    //     id: this.focusedIsbn,
    //     rent_duration: this.rentalDuration,
    //     return_date: retDate,
    //     username: this.username,
    //     membership: this.membership
    //   });
    // }
    localStorage.setItem('rented_list', JSON.stringify(this.rentedBooks));

    console.log(localStorage.getItem('rented_list'));

    this.closeModal();
  }

  /* RENT/RETURN MODAL */
  openModal(isbn: string) {
    let book = this.list.find(x => x.isbn === isbn);
    if (book) {
      this.focusedIsbn = book.isbn;
      this.focusedTitle = book.title;
    }
    let modal = document.getElementById("loginModal");
    if (modal != null) {
      modal.style.display = "block";
    }
  }

  closeModal() {
    let modal = document.getElementById("loginModal");
    if (modal)
      modal.style.display = "none";
    modal = document.getElementById("returnModal");
    if (modal)
      modal.style.display = "none";
  }

  loadDefault(event: Event) {
    (event.target as HTMLImageElement).src = "assets/no-image.jpg"
  }
}
