import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://drmapi20200126113358.azurewebsites.net/api/',
    timeout: 50000,
    headers: {
        'Content-Type': 'application/json'
    }
});

export default instance;
