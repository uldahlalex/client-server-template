import {Api} from './Api.ts';

export const baseUrl = 'http://localhost:8080';

export const http = new Api({
    baseURL: baseUrl
});