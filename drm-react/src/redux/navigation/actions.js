import { OPEN_DRAWER, CLOSE_DRAWER } from './actionNames';

export function openDrawer() {
    return { type: OPEN_DRAWER };
}

export function closeDrawer() {
    return { type: CLOSE_DRAWER };
}
