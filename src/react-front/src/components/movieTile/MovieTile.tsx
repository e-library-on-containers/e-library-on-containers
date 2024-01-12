import {Box, Text, useColorModeValue} from "@chakra-ui/react";
import {Movie} from "../../models/Movie";

const MovieTile = (props: { movie: Movie }) => {

    const lessImportantText = {
        color: "#888888"
    }

    const borderColor = useColorModeValue('black', 'white')

    const boxStyle = {
        marginTop: "8px",
        padding: "8px",
        border: "4px",
        borderStyle: "inset",
        borderColor: borderColor
    }

    return (
        <Box style={boxStyle}>
            <Text fontSize={'lg'}>{props.movie.title}</Text>
            <Text fontSize={'sm'} style={lessImportantText}>{props.movie.category}</Text>
            <Text fontSize={'md'}>Directors: {props.movie.directors.join(", ")}</Text>
            <Text fontSize={'md'}>Actors: {props.movie.actors.join(", ")}</Text>
            <Text fontSize={'md'}>Screenwriters: {props.movie.screenwriters.join(", ")}</Text>
        </Box>
    )
}

export default MovieTile
