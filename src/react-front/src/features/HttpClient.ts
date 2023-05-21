import axios, {AxiosInstance} from 'axios';
import {environment} from "./environment";


const axiosInstance: AxiosInstance = axios.create({
    baseURL: environment.apiUrl,
    headers: {
        'Content-Type': 'application/json'
    }
});

// Add JWT token to every request
axiosInstance.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('access-token');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        const userId = localStorage.getItem('user-id');
        if (userId) {
            config.headers['X-User-Id'] = `${userId}`;
        }
        return config;
    },
    (error) => Promise.reject(error),
);

export default axiosInstance;
