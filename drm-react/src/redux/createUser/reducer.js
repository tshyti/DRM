/* eslint-disable indent */
import {
    REQUEST_CREATE_USER,
    CREATE_USER_SUCCESS,
    CREATE_USER_FAILED
} from './actionNames';

const initialState = {
    loading: false,
    user: null,
    error: null
};

export default function reducer(state = initialState, action) {
    const { type, payload } = action;
    switch (type) {
        case REQUEST_CREATE_USER:
            return { loading: true };
        case CREATE_USER_SUCCESS:
            return { loading: false, user: payload };
        case CREATE_USER_FAILED:
            return { loading: false, error: payload };
        default:
            return state;
    }
}
