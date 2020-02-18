import {
    AUTHENTICATE_USER,
    USER_AUTHENTICATED,
    AUTHENTICATION_FAILED,
    APPLICATION_IS_STARTING,
    APPLICATION_STARTED,
    REQUEST_LOGOUT,
    USER_LOGGED_OUT
} from './actionNames';

export function authenticateUser(payload) {
    return { type: AUTHENTICATE_USER, payload };
}

export function userAuthenticated(payload) {
    return { type: USER_AUTHENTICATED, payload };
}

export function authenticationFailed(payload) {
    return { type: AUTHENTICATION_FAILED, payload };
}

export function appIsStarting(payload) {
    return { type: APPLICATION_IS_STARTING, payload };
}

export function appStarted(payload) {
    return { type: APPLICATION_STARTED, payload };
}

export function requestLogout() {
    return { type: REQUEST_LOGOUT };
}

export function userLoggedOut() {
    return { type: USER_LOGGED_OUT };
}
