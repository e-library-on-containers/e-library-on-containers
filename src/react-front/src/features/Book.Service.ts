import {AxiosInstance} from 'axios';
import {Book} from '../models/Book';
import {BookCopy} from '../models/BookCopy';
import axiosInstance from "./HttpClient";
import {environment} from "./environment";

export class BookService {

    readonly apiUrl: string;
    readonly httpClient: AxiosInstance;

    constructor() {
        this.apiUrl = environment.apiUrl;
        this.httpClient = axiosInstance;
    }

    getBook(isbn: string): Promise<Book> {
        return this.httpClient
            .get(`${this.apiUrl}/books/${isbn}`)
            .then((response) => response.data as Book)
            .catch((error) => {
                return Promise.reject(error);
            });
    }

    getFreeBook(isbn: string): Promise<BookCopy> {
        return this.httpClient
            .get(`${this.apiUrl}/books-copies?isbn=${isbn}&isAvailable=true`)
            .then((response) => response.data as BookCopy[])
            .then((response) => response.filter((book) => book.isAvailable))
            .then((response) => {
                if (response.length === 0) {
                    return Promise.reject('No free copies available');
                }
                return response[0];
            })
            .catch((error) => {
                return Promise.reject(error);
            });
    }

    getAllBooks(): Promise<Book[]> {
        return this.httpClient
            .get(`${this.apiUrl}/books`)
            .then((response) => response.data as Book[])
            .catch((error) => {
                return Promise.reject(error);
            });
    }

    getAllBookCopies(): Promise<BookCopy[]> {
        return this.httpClient
            .get(`${this.apiUrl}/books-copies`)
            .then((response) => response.data as BookCopy[])
            .catch((error) => {
                return Promise.reject(error);
            });
    }

    getBookCopyInfo(bookCopyId: number): Promise<Book> {
        return this.httpClient
            .get(`${this.apiUrl}/books-copies/${bookCopyId}/book-info`)
            .then((response) => response.data as Book)
            .catch((error) => {
                return Promise.reject(error);
            });
    }
}
