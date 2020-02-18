import { all } from 'redux-saga/effects';
import watchAuth from './auth';
import watchMain from './main';

export default function* rootSaga() {
    yield all([
        watchMain(),
        watchAuth()
    ]);
}
