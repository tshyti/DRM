import { SHOW_SNACKBAR, REMOVE_SNACKBAR } from './actionNames';

export function showSnackbar(payload) {
    return { type: SHOW_SNACKBAR, payload };
}

export function removeSnackbar(payload) {
    return { type: REMOVE_SNACKBAR, payload };
}
