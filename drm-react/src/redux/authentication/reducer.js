/* eslint-disable indent */
import {
    AUTHENTICATE_USER,
    USER_AUTHENTICATED,
    AUTHENTICATION_FAILED,
    APPLICATION_IS_STARTING,
    APPLICATION_STARTED,
    REQUEST_LOGOUT,
    USER_LOGGED_OUT
} from './actionNames';

const initialState = {
    id: '',
    username: '',
    name: '',
    surname: '',
    email: '',
    token: '',
    refreshToken: '',
    expirationTime: '',
    userRole: '',
    rememberMe: false,
    isLoading: false,
    isAuthenticated: false,
    isAppLoading: true,
    error: {}
};

export default function reducer(state = initialState, action) {
    const { type, payload } = action;
    switch (type) {
        case AUTHENTICATE_USER:
            return { ...state, rememberMe: payload.rememberMe, isLoading: true };
        case USER_AUTHENTICATED:
            return { ...state, ...payload, isLoading: false, isAuthenticated: true };
        case AUTHENTICATION_FAILED:
            return { ...state, error: payload, isLoading: false };
        case APPLICATION_IS_STARTING:
            return { ...state };
        case APPLICATION_STARTED:
            return { ...state, isAppLoading: false, ...payload };
        case REQUEST_LOGOUT:
            return { ...state, isAuthenticated: false };
        case USER_LOGGED_OUT:
            return { ...initialState, isAppLoading: false };
        default:
            return state;
    }
}
