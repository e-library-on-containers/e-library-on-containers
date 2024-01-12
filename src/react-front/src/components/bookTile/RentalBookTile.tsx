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

    const awaitingApprovalButton = {
        backgroundColor: "pink.400",
        color: "white"
    }

    return (
        <VStack
            spacing={4}
            key={props.book.rentId}
        >
            <BookTile book={props.book}/>
            <Box style={{textAlign: "center"}}>
                {props.book.rentalState.toString() === "AWAITING_RETURN_APPROVAL" ?
                    <Button
                        variant={"ghost"}
                        isLoading
                        spinnerPlacement={"start"}
                        loadingText={"Awaiting admin approval"}
                        style={awaitingApprovalButton}
                    />
                    :
                    <Button
                        style={rentButton}
                        onClick={() => props.onReturnBook(props.book.rentId)}
                    >
                        Return
                    </Button>
                }
            </Box>
        </VStack>
    )
}
export default RentalBookTile;
