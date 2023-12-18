import {AxiosInstance} from "axios";
import {Rent} from "../models/Rent";
import {environment} from "./environment"
import axiosInstance from "./HttpClient";
import {BorrowRequest} from "../models/BorrowRequest";

export class RentalService {

    readonly apiUrl: string;
    readonly httpClient: AxiosInstance;

    constructor() {
        this.apiUrl = environment.apiUrl
        this.httpClient = axiosInstance;
    }

    getAllRentals(): Promise<Rent[]> {
        return this.httpClient
            .get(`${this.apiUrl}/rentals`)
            .then((response) => response.data)
            .catch((error) => {
                console.error(error);
                return Promise.reject(error);
            })
    }

    rentBook(bookCopyId: number): Promise<Object> {
        return this.httpClient.post(`${this.apiUrl}/rentals/rent`, {bookInstanceId: bookCopyId});
    }

    returnBook(rentId: string) {
        return this.httpClient.post(`${this.apiUrl}/rentals/${rentId}/return`)
    }

    borrowBook(bookCopyId: number): Promise<Object> {
        return this.httpClient.post(`${this.apiUrl}/rentals/borrow`, {bookInstanceId: bookCopyId})
    }

    approveBookBorrow(borrowId: string): Promise<Object> {
        return this.httpClient.post(`${this.apiUrl}/rentals/accept-borrow`, {borrowId: borrowId})
    }

    getAllBorrowRequests(): Promise<BorrowRequest[]> {
        return this.httpClient.get(`${this.apiUrl}/rentals/borrows`)
            .then(response => response.data)
            .catch((error) => {
                console.error(error);
                return Promise.reject(error);
            })
    }
}
