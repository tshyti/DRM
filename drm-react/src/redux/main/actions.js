import {
    REQUEST_UPLOAD_FILE,
    UPLOAD_FILE_SUCCESS,
    UPLOAD_FILE_FAILED,
    REQUEST_GET_USER_FILES,
    GET_USER_FILES_SUCCESS,
    GET_USER_FILES_FAILED,
    REQUEST_DOWNLOAD_USER_FILE,
    DOWNLOAD_USER_FILE_FAILED,
    DOWNLOAD_USER_FILE_SUCCESS,
    FILTER_USER_FILES,
    REQUEST_DELETE_FILE,
    DELETE_FILE_SUCCESS,
    DELETE_FILE_FAIL,
    OPEN_DIALOG,
    CLOSE_DIALOG
} from './actionNames';

export function requestUploadFile(payload) {
    return { type: REQUEST_UPLOAD_FILE, payload };
}

export function uploadFileSuccess(payload) {
    return { type: UPLOAD_FILE_SUCCESS, payload };
}

export function uploadFileFailed(payload) {
    return { type: UPLOAD_FILE_FAILED, payload };
}

export function requestGetUserFiles() {
    return { type: REQUEST_GET_USER_FILES };
}

export function getUserFilesSuccess(payload) {
    return { type: GET_USER_FILES_SUCCESS, payload };
}

export function getUserFilesFailed(payload) {
    return { type: GET_USER_FILES_FAILED, payload };
}

export function filterUserFiles(payload) {
    return { type: FILTER_USER_FILES, payload };
}

export function requestDownloadUserFile(fileId) {
    return { type: REQUEST_DOWNLOAD_USER_FILE, payload: fileId };
}

export function downloadUserFileSuccess(payload) {
    return { type: DOWNLOAD_USER_FILE_SUCCESS, payload };
}

export function downloadUserFileFailed(payload) {
    return { type: DOWNLOAD_USER_FILE_FAILED, payload };
}

export function requestDeleteFile(fileId) {
    return { type: REQUEST_DELETE_FILE, payload: fileId };
}

export function deleteFileSuccess(fileId) {
    return { type: DELETE_FILE_SUCCESS, payload: fileId };
}

export function deleteFileFailed(payload) {
    return { type: DELETE_FILE_FAIL, payload };
}

export function openDialog(payload) {
    return { type: OPEN_DIALOG, payload };
}

export function closeDialog(payload) {
    return { type: CLOSE_DIALOG, payload };
}
