/* eslint-disable indent */
import uuid4 from 'uuid/v4';
import {
    SHOW_SNACKBAR,
    REMOVE_SNACKBAR
} from './actionNames';

const initialState = {
    key: 1,
    isRunning: true,
    variant: 'success',
    duration: 3000,
    message: 'test'
};

export default function reducer(state = {}, action) {
    const { type, payload } = action;
    switch (type) {
        case SHOW_SNACKBAR:
            return {
                key: uuid4(),
                variant: payload.variant,
                duration: payload.duration,
                message: payload.message
            };
        case REMOVE_SNACKBAR:
            return {};
        default:
            return state;
    }
}
