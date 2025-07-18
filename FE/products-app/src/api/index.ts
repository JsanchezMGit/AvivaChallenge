import axios from "axios";

const apiKey = import.meta.env.VITE_API_KEY;

export const productsApi = axios.create({
    baseURL: 'http://localhost:5045/'
});

productsApi.interceptors.request.use((config) => {  
    if (!apiKey) {
        console.error('API Key no configurada');
        throw new Error('Configuración de API no válida');
    }

    config.headers['X-Api-Key'] = apiKey;
    return config;
});

export const paymentsApi = axios.create({
    baseURL: 'http://localhost:5228/v1/'
});

paymentsApi.interceptors.request.use((config) => {  
    if (!apiKey) {
        console.error('API Key no configurada');
        throw new Error('Configuración de API no válida');
    }

    config.headers['X-Api-Key'] = apiKey;
    return config;
});