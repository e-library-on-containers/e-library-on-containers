import {RentalState} from "./RentalState";

export interface RentedBook {
    rentId: string,
    bookCopyId: number,
    userId: string,
    coverImg: string,
    title: string,
    authors: string,
    isbn: string,
    rentalState: RentalState
}
