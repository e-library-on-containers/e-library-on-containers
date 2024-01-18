import SpinnerWrapper from "../../components/wrapper/SpinnerWrapper";
import ErrorWrapper from "../../components/wrapper/ErrorWrapper";
import {Box, Link, ListItem, SimpleGrid, Text, UnorderedList} from "@chakra-ui/react";
import React, {useEffect, useState} from "react";
import {BookService} from "../../features/Book.Service";
import {RentalService} from "../../features/Rental.Service";
import RentalBookTile from "../../components/bookTile/RentalBookTile";
import {RentedBook} from "../../models/RentedBook";
import {useSelector} from "react-redux";
import {RootState} from "../../redux/rootReducer";
import {useNavigate} from "react-router-dom";
import {BorrowRequest} from "../../models/BorrowRequest";

const Rental = () => {
    const [error, setError] = useState<string>();
    const [loading, setLoading] = useState(true);
    const [rentedBooks, setRentedBooks] = useState<RentedBook[]>([])
    const [borrowRequests, setBorrowRequests] = useState<BorrowRequest[]>([])

    const jwt = useSelector((state: RootState) => state.auth.jwt);

    const bookService = new BookService();
    const rentalService = new RentalService();
    const navigate = useNavigate();

    useEffect(() => {
        updateRentals();
        getBorrowRequests();
    }, []);

    const updateRentals = () => {
        setLoading(true)
        setRentedBooks([])
        rentalService.getAllRentals()
            .then(rentals =>
                rentals.map(async rental => {
                    const bookCopyInfo = await bookService.getBookCopyInfo(rental.bookCopyId)
                    const rentedBook = {
                        rentId: rental.id,
                        bookCopyId: rental.bookCopyId,
                        userId: rental.userId,
                        coverImg: bookCopyInfo.coverImg,
                        title: bookCopyInfo.title,
                        authors: bookCopyInfo.authors,
                        isbn: bookCopyInfo.isbn,
                        rentalState: rental.rentalState
                    } as RentedBook;
                    setRentedBooks([...rentedBooks, rentedBook])
                }))
            .finally(() => setLoading(false))
            .catch(error => setError(error))
    }

    const getBorrowRequests = () => {
        rentalService.getAllBorrowRequests()
            .then(requests => setBorrowRequests(requests))
            .catch(error => setError(error))
    }

    const returnBook = (rentId: string) => {
        rentalService.returnBook(rentId)
            .then(_ => navigate('/'));
    }

    const approveBorrow = (borrowId: string) => {
        rentalService.approveBookBorrow(borrowId)
            .catch(error => setError(error))
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
                    {borrowRequests.length > 0 &&
                        <UnorderedList style={{textAlign: "left"}}>
                            {borrowRequests.map(request =>
                                <ListItem key={request.id}>User {request.newOwner} wants to borrow
                                    book {request.bookInstanceId}. <Link onClick={() => approveBorrow(request.id)}>Approve?</Link></ListItem>)}
                        </UnorderedList>}
                </ErrorWrapper>
            </SpinnerWrapper> :
            <Text fontSize={'3xl'}>First log in!</Text>
    )
}

export default Rental
