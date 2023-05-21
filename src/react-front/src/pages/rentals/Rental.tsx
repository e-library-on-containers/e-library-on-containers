import SpinnerWrapper from "../../components/wrapper/SpinnerWrapper";
import ErrorWrapper from "../../components/wrapper/ErrorWrapper";
import {Box, SimpleGrid, Text} from "@chakra-ui/react";
import React, {useEffect, useState} from "react";
import {BookService} from "../../features/Book.Service";
import {RentalService} from "../../features/Rental.Service";
import RentalBookTile from "../../components/bookTile/RentalBookTile";
import {RentedBook} from "../../models/RentedBook";
import {useSelector} from "react-redux";
import {RootState} from "../../redux/rootReducer";

const Rental = () => {
    const [error, setError] = useState<string>();
    const [loading, setLoading] = useState(true);
    const [rentedBooks, setRentedBooks] = useState<RentedBook[]>([])

    const jwt = useSelector((state: RootState) => state.auth.jwt);

    const bookService = new BookService();
    const rentalService = new RentalService();

    useEffect(() => {
        updateRentals();
    }, []);

    const updateRentals = () => {
        setLoading(true)
        setRentedBooks([])
        rentalService.getAllRentals()
            .then(rental => {
                    rental.map(async rent => await bookService.getAllBookCopies()
                        .then(rentals => {
                            setLoading(true)
                            if (rentals.length <= 0) {
                                return Promise.reject("No book in library!")
                            }
                            return rentals
                        })
                        .then(copies => {
                                const copy = copies.find(copy => copy.id == rent.bookCopyId);
                                if (copy !== undefined) {
                                    bookService.getBook(copy.isbn).then(book => {
                                        const rentedBook = {
                                            rentId: rent.id,
                                            bookCopyId: rent.bookCopyId,
                                            userId: rent.userId,
                                            coverImg: book.coverImg,
                                            title: book.title,
                                            authors: book.authors,
                                            isbn: book.isbn
                                        } as RentedBook;
                                        setRentedBooks([...rentedBooks, rentedBook])
                                    })
                                }
                            }
                        )
                        .finally(() => setLoading(false))
                    )
                }
            )
            .finally(() => setLoading(false))
            .catch(error => console.error(error))
    }

    const returnBook = (rentId: string) => {
        rentalService.returnBook(rentId).then(r => updateRentals());
    }

    return (
        jwt != null ?
            <SpinnerWrapper loading={loading}>
                <ErrorWrapper error={error}>
                    <SimpleGrid minChildWidth='200px' spacing='40px'>
                        {
                            rentedBooks?.length !== undefined && rentedBooks.length > 0 ?
                                rentedBooks.map((book: RentedBook) =>
                                    <RentalBookTile key={book.rentId} book={book} onReturnBook={returnBook}/>
                                ) :
                                <Box>Rent book first!</Box>
                        }
                    </SimpleGrid>
                </ErrorWrapper>
            </SpinnerWrapper> :
            <Text fontSize={'4xl'}>First log in!</Text>
    )
}

export default Rental
