import {Box, Image, Text, useColorModeValue} from "@chakra-ui/react";
import {Book} from "../../models/Book";
import {RentedBook} from "../../models/RentedBook";

const BookTileComponent = (props: { book: Book | RentedBook }) => {

    const lessImportantText = {
        color: "#888888"
    }

    const bookDescription = {
        marginTop: "8px"
    }

    const borderColor = useColorModeValue('black', 'white')

    const imageStyle = {
        borderRadius: "1em",
        border: "0.25vmin",
        borderStyle: "solid",
        borderColor: borderColor
    }

    const loadDefault = () => {
        return "/images/no-image.jpg";
    };

    return (
        <>
            <Image
                style={imageStyle}
                src={props.book.coverImg}
                fallbackSrc={loadDefault()}
            />
            <Box style={bookDescription}>
                <Text fontSize={'lg'} as="b" >{props.book.title}</Text>
                <Text fontSize={'md'}>{props.book.authors}</Text>
                <Text fontSize={'sm'} as="em" style={lessImportantText}>ISBN: {props.book.isbn}</Text>
            </Box>
        </>
    )
}

export default BookTileComponent
