/* eslint-disable indent */
import { OPEN_DRAWER, CLOSE_DRAWER } from './actionNames';

const initialState = {
    isDrawerOpened: false
};

export default function reducer(state = initialState, action) {
    switch (action.type) {
        case OPEN_DRAWER:
            return { isDrawerOpened: true };
        case CLOSE_DRAWER:
            return { isDrawerOpened: false };
        default:
            return state;
    }
}
