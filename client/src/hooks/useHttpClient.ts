import {Api} from '../Api.ts';

export default function useHttpClient() {
     const baseUrl = 'http://localhost:5000';

     const useHttpClient = new Api({
        baseURL: baseUrl
    });

    useHttpClient.instance.interceptors.request.use((config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    });
    return useHttpClient;
}

