import {
    takeEvery,
    call,
    put,
    takeLatest,
    select
} from 'redux-saga/effects';
import {
    AUTHENTICATE_USER,
    APPLICATION_IS_STARTING,
    REQUEST_LOGOUT
} from '../redux/authentication/actionNames';
import {
    authenticationFailed,
    userAuthenticated,
    appStarted,
    userLoggedOut
} from '../redux/authentication/actions';
import {
    REQUEST_CREATE_USER
} from '../redux/createUser/actionNames';
import {
    createUserSuccess,
    createUserFailed
} from '../redux/createUser/actions'
import { showSnackbar } from '../redux/snackbar/actions';
import axios from '../services/axiosConfig';

export default function* watcherSaga() {
    yield takeEvery(APPLICATION_IS_STARTING, appStartWatcher);
    yield takeEvery(AUTHENTICATE_USER, workerSaga);
    yield takeLatest(REQUEST_LOGOUT, logoutWatcher);
    yield takeLatest(REQUEST_CREATE_USER, createUserWatcher);
}

function* workerSaga(action) {
    try {
        const payload = yield call(login, action.payload);
        yield put(userAuthenticated(payload.data));
    }
    catch (e) {
        yield put(authenticationFailed(e.response));
    }
}

async function login(user) {
    const authDetails = await axios.post('Authentication/Login', user);

    // if no error is thrown
    localStorage.setItem('auth', JSON.stringify({
        token: authDetails.data.token,
        refreshToken: authDetails.data.refreshToken,
        userRole: authDetails.data.userRole
    }));
    return authDetails;
}

function* appStartWatcher(action) {
    try {
        const auth = yield call(appStart);
        yield put(appStarted(auth));
    }
    catch (e) {
        console.log(e);
    }
}

function appStart() {
    let auth = localStorage.getItem('auth');
    auth = JSON.parse(auth);
    if (auth && auth.token) {
        auth['isAuthenticated'] = true;
    }
    return auth;
}

function* logoutWatcher(action) {
    try {
        const token = yield select(state => state.authentication.token);
        yield put(showSnackbar({
            variant: 'success',
            duration: 3000,
            message: 'You are logged out.'
        }));
        yield call(logoutUser, token);
        yield put(userLoggedOut());
        yield put({ type: 'RESET_STORE' });
    }
    catch (e) {
        console.log(JSON.stringify(e));
    }
}

async function logoutUser(token) {
    localStorage.removeItem('auth');
    const config = {
        headers: {
            Authorization: `Bearer ${token}`
        }
    };
    await axios.post('authentication/logout', null, config);
}

function* createUserWatcher(action) {
    try {
        const token = yield select(state => state.authentication.token);
        const user = yield call(createUser, action.payload, token);
        yield put(createUserSuccess(user));
        yield put(showSnackbar({
            variant: 'success',
            duration: 3000,
            message: 'User has been created successfully'
        }));
    }
    catch (error) {
        yield put(createUserFailed(error.response));
        yield put(showSnackbar({
            variant: 'error',
            duration: 3000,
            message: error.response.data
        }));
    }
}

async function createUser(user, token) {
    const config = {
        headers: {
            Authorization: `Bearer ${token}`
        }
    };
    const userRequest = {
        username: user.username,
        password: user.password,
        name: user.firstName,
        surname: user.lastName,
        email: user.email,
        roleID: user.roleID
    };
    const createdUser = await axios.post('users', userRequest, config);
    return createdUser.data;
}
