/* eslint-disable indent */
import {
    REQUEST_UPLOAD_FILE,
    UPLOAD_FILE_SUCCESS,
    UPLOAD_FILE_FAILED,
    REQUEST_GET_USER_FILES,
    GET_USER_FILES_FAILED,
    GET_USER_FILES_SUCCESS,
    REQUEST_DOWNLOAD_USER_FILE,
    DOWNLOAD_USER_FILE_SUCCESS,
    DOWNLOAD_USER_FILE_FAILED,
    FILTER_USER_FILES,
    REQUEST_DELETE_FILE,
    DELETE_FILE_SUCCESS,
    DELETE_FILE_FAIL,
    OPEN_DIALOG,
    CLOSE_DIALOG
} from './actionNames';

const initialState = {
    totalFiles: 0,
    downloadNo: 0,
    uploadNo: 0,
    loading: false,
    error: {},
    files: [],
    filterWord: '',
    filteredFiles: [],
    deletionFileId: '',
    isDialogOpened: false
};

export default function reducer(state = initialState, action) {
    const { type, payload } = action;

    switch (type) {
        case REQUEST_UPLOAD_FILE:
            return { ...state, loading: true };
        case UPLOAD_FILE_SUCCESS:
            return {
                ...state,
                loading: false,
                files: [...state.files, payload],
                filteredFiles: [...state.files, payload].filter(file => file.id !== payload
                    && file.name.toLowerCase().includes(state.filterWord.toLowerCase())
                ),
                uploadNo: state.uploadNo + 1,
                totalFiles: state.totalFiles + 1
            };
        case UPLOAD_FILE_FAILED:
            return { ...state, loading: false, error: payload };

        case REQUEST_GET_USER_FILES:
            return { ...state, loading: true };
        case GET_USER_FILES_SUCCESS:
            return {
                ...state,
                loading: false,
                files: payload.userFiles,
                filteredFiles: payload.userFiles,
                ...payload.userStats
            };
        case GET_USER_FILES_FAILED:
            return { ...state, loading: false, error: payload };
        case FILTER_USER_FILES:
            return {
                ...state,
                filterWord: payload,
                filteredFiles: state.files.filter(
                    file => file.name.toLowerCase().includes(payload.toLowerCase())
                )
            };

        case REQUEST_DOWNLOAD_USER_FILE:
            return { ...state, loading: true };
        case DOWNLOAD_USER_FILE_SUCCESS:
            return { ...state, loading: false, downloadNo: state.downloadNo + 1 };
        case DOWNLOAD_USER_FILE_FAILED:
            return { ...state, loading: false, error: payload };

        case OPEN_DIALOG:
            return {
                ...state,
                isDialogOpened: true,
                deletionFileId: payload
            };
        case CLOSE_DIALOG:
            return { ...state, isDialogOpened: false, deletionFileId: '' };
        case REQUEST_DELETE_FILE:
            return { ...state, loading: true, isDialogOpened: false };
        case DELETE_FILE_SUCCESS:
            return {
                ...state,
                loading: false,
                files: state.files.filter(file => file.id !== payload),
                filteredFiles: state.files.filter(file => file.id !== payload
                    && file.name.toLowerCase().includes(state.filterWord.toLowerCase())
                ),
                totalFiles: state.totalFiles - 1,
                deletionFileId: ''
            };
        case DELETE_FILE_FAIL:
            return { ...state, loading: false, error: payload };
        default:
            return state;
    }
}
