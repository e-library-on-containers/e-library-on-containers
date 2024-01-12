import {AxiosInstance} from "axios";
import {environment} from "./environment";
import axiosInstance from "./HttpClient";
import {Movie} from "../models/Movie";

export class MoviesService {

    readonly apiUrl: string;
    readonly httpClient: AxiosInstance;

    constructor() {
        this.apiUrl = environment.apiUrl
        this.httpClient = axiosInstance;
    }

    getAllMovies(): Promise<Movie[]> {
        return this.httpClient
            .get(`${this.apiUrl}/movies`)
            .then((response) => response.data)
            .catch((error) => {
                console.error(error);
                return Promise.reject(error);
            })
    }
}