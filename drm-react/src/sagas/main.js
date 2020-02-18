import { takeLatest, call, put, select, takeEvery } from 'redux-saga/effects';
import { REQUEST_UPLOAD_FILE, REQUEST_GET_USER_FILES, REQUEST_DOWNLOAD_USER_FILE, REQUEST_DELETE_FILE } from '../redux/main/actionNames';
import { fileToBase64, base64toBlob } from '../services/base64Conversions';
import {
    uploadFileSuccess,
    getUserFilesFailed,
    getUserFilesSuccess,
    uploadFileFailed,
    downloadUserFileFailed,
    downloadUserFileSuccess,
    deleteFileFailed,
    deleteFileSuccess
} from '../redux/main/actions';
import axios from '../services/axiosConfig';
import { showSnackbar } from '../redux/snackbar/actions';

export default function* watcherSaga() {
    yield takeEvery(REQUEST_UPLOAD_FILE, uploadFileWorker);
    yield takeLatest(REQUEST_GET_USER_FILES, userFilesWorker);
    yield takeEvery(REQUEST_DOWNLOAD_USER_FILE, downloadUserFilesWorker);
    yield takeEvery(REQUEST_DELETE_FILE, deleteFileWatcher);
}

function* uploadFileWorker(action) {
    try {
        const token = yield select(state => state.authentication.token);
        const file = yield call(uploadFileToServer, action.payload, token);
        yield put(uploadFileSuccess(file));
        yield put(showSnackbar({
            variant: 'success',
            duration: 5000,
            message: 'File has been uploaded successfully'
        }));
    }
    catch (error) {
        yield put(uploadFileFailed(error.response));
    }
}

async function uploadFileToServer(file, token) {
    const fileBase64 = await fileToBase64(file);
    const config = {
        headers: {
            Authorization: `Bearer ${token}`
        }
    };
    const requestBody = {
        name: file.name,
        mimeType: file.type,
        data: fileBase64
    };

    const userFiles = await axios.post('/userfiles', requestBody, config);
    return userFiles.data;
}

function* userFilesWorker() {
    try {
        const token = yield select(state => state.authentication.token);
        const userFiles = yield call(getUserFiles, token);
        yield put(getUserFilesSuccess(userFiles));
    }
    catch (error) {
        yield put(getUserFilesFailed(error.response));
    }
}

async function getUserFiles(token) {
    const config = {
        headers: {
            Authorization: `Bearer ${token}`
        }
    };

    const [userFiles, userStats] = await Promise.all([
        axios.get('/userfiles', config),
        axios.get('/users/getUserStats', config)
    ]);
    return {
        userFiles: userFiles.data,
        userStats: userStats.data
    };
}

function* downloadUserFilesWorker(action) {
    try {
        const token = yield select(state => state.authentication.token);
        yield call(downloadUserFile, action.payload, token);
        yield put(downloadUserFileSuccess());
        yield put(showSnackbar({
            variant: 'success',
            duration: 3000,
            message: 'File has been downloaded successfully'
        }));
    }
    catch (error) {
        yield put(downloadUserFileFailed(error));
    }
}

async function downloadUserFile(fileId, token) {
    const config = {
        headers: {
            Authorization: `Bearer ${token}`
        }
    };
    const userFiles = await axios.get(`/userfiles/${fileId}`, config);
    const blob = base64toBlob(userFiles.data.data, userFiles.data.mimeType);

    const url = window.URL.createObjectURL(blob);
    let link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', userFiles.data.name);
    document.body.appendChild(link);
    link.click();
}

function* deleteFileWatcher(action) {
    try {
        const token = yield select(state => state.authentication.token);
        const fileId = yield select(state => state.main.deletionFileId);
        yield call(deleteFile, fileId, token);
        yield put(deleteFileSuccess(fileId));
        yield put(showSnackbar({
            variant: 'success',
            duration: 5000,
            message: 'File has been deleted successfully'
        }));
    }
    catch (error) {
        yield put(deleteFileFailed(error.data));
    }
}

async function deleteFile(fileId, token) {
    const config = {
        headers: {
            Authorization: `Bearer ${token}`
        }
    };
    const userFiles = await axios.delete(`/userfiles/${fileId}`, config);
}
