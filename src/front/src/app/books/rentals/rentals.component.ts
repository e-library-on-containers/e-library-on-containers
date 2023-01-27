import {Component, OnInit} from '@angular/core';
import {Book} from "../../models/book";
import {RentedBook} from "../../models/rentedBook";
import {ActivatedRoute} from "@angular/router";
import {BookService} from "../book/book.service";

@Component({
  selector: 'app-rentals',
  templateUrl: './rentals.component.html',
  styleUrls: ['./rentals.component.css']
})
export class RentalsComponent implements OnInit {
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

  returnBook() {
    if (this.username.length === 0) {
      this.errorMsg = "Error: Username is empty";
      return;
    }
    if (this.membership.length === 0) {
      this.errorMsg = "Error: Membership # is empty";
      return;
    }
    // let i = this.rentedBooks.map(x => x.id).indexOf(this.focusedId);
    // if (i !== -1) {
    //   if (this.rentedBooks[i].username === this.username &&
    //     this.rentedBooks[i].membership === this.membership) {
    //     // this.list = this.list.filter(x => x.id !== this.rentedBooks[i].id);
    //     this.rentedBooks.splice(i, 1);
    //   } else {
    //     this.errorMsg = "Error: Invalid username or membership #";
    //     return;
    //   }
    // }

    localStorage.setItem('rented_list', JSON.stringify(this.rentedBooks));
    this.closeModal();
  }

  openModalReturn(isbn: string) {
    let book = this.list.find(x => x.isbn === isbn);
    if (book) {
      this.focusedIsbn = book.isbn;
      this.focusedTitle = book.title;
    }
    let modal = document.getElementById("returnModal");
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
