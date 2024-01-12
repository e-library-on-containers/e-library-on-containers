import {AxiosInstance} from "axios";
import {Rent} from "../models/Rent";
import {environment} from "./environment"
import axiosInstance from "./HttpClient";

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
                return Promise.reject();
            })
    }

    rentBook(bookCopyId: number): Promise<Object> {
        return this.httpClient.post(`${this.apiUrl}/rentals/rent`, {bookInstanceId: bookCopyId});
    }

    returnBook(rentId: string) {
        return this.httpClient.post(`${this.apiUrl}/rentals/${rentId}/return`)
    }
}
