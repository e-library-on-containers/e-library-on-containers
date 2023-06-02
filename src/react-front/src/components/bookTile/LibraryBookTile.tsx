import {Box, Button, VStack} from "@chakra-ui/react";
import {Book} from "../../models/Book";
import BookTile from "./BookTile";

const LibraryBookTile = (props: { book: Book, onRentBook: (bookIsbn: string) => void, isLoggedIn: boolean }) => {
    const rentedOutButton = {
        width: "100%",
        backgroundColor: "grey",
        color: "white",
        padding: "14px 20px",
        margin: "8px 0",
        border: "none",
        borderRadius: "4px",
        cursor: "not-allowed"
    };

    const rentButton = {
        width: "100%",
        backgroundColor: "#010057",
        color: "white",
        padding: "14px 20px",
        margin: "8px 0",
        border: "none",
        borderRadius: "4px",
        cursor: "pointer"
    }

    return (
        <VStack
            spacing={4}
            key={props.book.id}
        >
            <BookTile book={props.book}/>
            {
                props.isLoggedIn && <Box style={{textAlign: "center"}}>
                    {props.book.isAvailable ? (
                        <Button
                            type="button"
                            style={rentButton}
                            onClick={() => props.onRentBook(props.book.isbn)}
                        >
                            Rent
                        </Button>
                    ) : (
                        <Button
                            type="button"
                            style={rentedOutButton}
                            disabled
                        >
                            Rented Out
                        </Button>
                    )
                    }
                </Box>}
        </VStack>
    )
}
export default LibraryBookTile;
