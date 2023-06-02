import {environment} from "./environment";
import {AxiosInstance, AxiosResponse} from "axios";
import axiosInstance from "./HttpClient";
import {TokenResponse} from "../models/TokenResponse";
import {User} from "../models/User";
import jwt_decode from 'jwt-decode';

interface DecodedToken {
    sub: string;
}

export class AuthService {
    private readonly apiUrl: string;
    private readonly httpClient: AxiosInstance;

    constructor() {
        this.apiUrl = environment.apiUrl
        this.httpClient = axiosInstance;
    }

    login(credentials: { email: string, password: string }): Promise<TokenResponse> {
        return this.httpClient.post(`${this.apiUrl}/identity/auth`, credentials)
            .then((response: AxiosResponse<TokenResponse>) => {
                localStorage.setItem('access-token', response.data.token)
                localStorage.setItem('user-id', jwt_decode<DecodedToken>(response.data.token).sub)
                return response.data;
            })
    }

    createAccount(user: User): Promise<boolean> {
        return this.httpClient.post(`${this.apiUrl}/identity/register`, user)
            .then(() => true);
    }

    logout() {
        localStorage.removeItem('access-token')
        localStorage.removeItem('user-id')
    }
}
