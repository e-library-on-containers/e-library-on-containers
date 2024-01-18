import React, {useEffect, useState} from "react";
import {Book} from "../../models/Book";
import {BookService} from "../../features/Book.Service";
import {RentalService} from "../../features/Rental.Service";
import {BookCopy} from "../../models/BookCopy";
import LibraryBookTile from "../../components/bookTile/LibraryBookTile";
import SpinnerWrapper from "../../components/wrapper/SpinnerWrapper";
import ErrorWrapper from "../../components/wrapper/ErrorWrapper";
import {SimpleGrid} from "@chakra-ui/react";
import {AxiosError} from "axios";
import {useSelector} from "react-redux";
import {RootState} from "../../redux/rootReducer";

const LibraryComponent = () => {
    const [books, setBooks] = useState<Array<Book>>([]);
    const [error, setError] = useState<string>();
    const [loading, setLoading] = useState(true);

    const jwt = useSelector((state: RootState) => state.auth.jwt);

    const bookService = new BookService();
    const rentalService = new RentalService();

    useEffect(() => {
        updateBooks();
    }, []);

    const rentBook = (isbn: string) => {
        bookService.getFreeBook(isbn)
            .then((book: BookCopy) => rentalService.rentBook(book.id))
            .then(() => updateBooks())
            .catch((err: AxiosError) => {
                setError(err.name.toString());
            })
            .finally(() => setLoading(false));
    };

    const borrowBook = (isbn: string) => {
        bookService.getBookCopy(isbn)
            .then((bookCopy: BookCopy) => rentalService.borrowBook(bookCopy.id))
            .catch((err: AxiosError) => {
                console.log(err)
                setError(err.name.toString());
            })
    }

    const updateBooks = () => {
        bookService.getAllBooks()
            .then((books: Book[]) => setBooks(books))
            .then(() => setLoading(false))
            .catch(err => {
                setError(err.toString())
                console.log(err.toString())
            })
            .finally(() => setLoading(false));
    };

    return (
        <SpinnerWrapper loading={loading}>
            <ErrorWrapper error={error}>
                <SimpleGrid minChildWidth='200px' spacing='40px'>
                    {books.length === 0 ?
                        <h1>There are no books yet</h1> :
                        books.map((book: Book) => <LibraryBookTile
                            key={book.id}
                            book={book}
                            isLoggedIn={jwt != null}
                            onRentBook={rentBook}
                            onBorrowBook={borrowBook}/>)}
                </SimpleGrid>
            </ErrorWrapper>
        </SpinnerWrapper>
    );
};

export default LibraryComponent;
