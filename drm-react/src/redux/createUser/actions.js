import {
    REQUEST_CREATE_USER,
    CREATE_USER_SUCCESS,
    CREATE_USER_FAILED
} from './actionNames';

export function requestCreateUser(payload) {
    return { type: REQUEST_CREATE_USER, payload };
}

export function createUserSuccess(payload) {
    return { type: CREATE_USER_SUCCESS, payload };
}

export function createUserFailed(payload) {
    return { type: CREATE_USER_FAILED, payload };
}
