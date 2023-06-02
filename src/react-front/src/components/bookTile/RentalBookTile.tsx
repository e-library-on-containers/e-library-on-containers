import {Box, Button, VStack} from "@chakra-ui/react";
import BookTile from "./BookTile";
import {RentedBook} from "../../models/RentedBook";

const RentalBookTile = (props: { book: RentedBook, onReturnBook: (bookIsbn: string) => void }) => {

    const rentButton = {
        width: "100%",
        backgroundColor: "pink.400",
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
            key={props.book.rentId}
        >
            <BookTile book={props.book}/>
            <Box style={{textAlign: "center"}}>
                <Button
                    type="button"
                    style={rentButton}
                    onClick={() => props.onReturnBook(props.book.rentId)}
                >
                    Return
                </Button>
            </Box>
        </VStack>
    )
}
export default RentalBookTile;
