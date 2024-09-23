import {Api} from './Api.ts';

export const baseUrl = 'http://localhost:5000';

export const http = new Api({
    baseURL: baseUrl
});