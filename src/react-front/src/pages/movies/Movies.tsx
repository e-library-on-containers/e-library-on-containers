import React, {useEffect, useState} from "react";
import {MoviesService} from "../../features/Movies.Service";
import {Movie} from "../../models/Movie";
import ErrorWrapper from "../../components/wrapper/ErrorWrapper";
import {Box, SimpleGrid} from "@chakra-ui/react";
import SpinnerWrapper from "../../components/wrapper/SpinnerWrapper";
import MovieTile from "../../components/movieTile/MovieTile";

const Movies = () => {
    const moviesService = new MoviesService();
    const [movies, setMovies] = useState<Movie[]>([])
    const [error, setError] = useState<string>();
    const [loading, setLoading] = useState<boolean>(true);

    const updateMovies = () => {
        setMovies([])
        setError(undefined)
        setLoading(true)
        moviesService.getAllMovies()
            .then(movies => setMovies(movies))
            .finally(() => setLoading(false))
            .catch(error => setError(error))
    }

    useEffect(() => {
        updateMovies();
    }, []);

    return (
        <SpinnerWrapper loading={loading}>
            <ErrorWrapper error={error}>
                <SimpleGrid minChildWidth='200px' spacing='40px'>
                    {
                        movies?.length > 0 ?
                            movies.filter((movie: Movie) => movie.inPreview)
                                .map((movie: Movie) => <MovieTile key={movie.id} movie={movie}/>)
                            : <Box>No movies in database</Box>
                    }
                </SimpleGrid>
            </ErrorWrapper>
        </SpinnerWrapper>
    )
}

export default Movies