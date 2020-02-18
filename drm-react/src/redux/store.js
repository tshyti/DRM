import { createStore, compose, applyMiddleware } from 'redux';
import createSagaMiddleware from 'redux-saga';
import rootReducer from './rootReducer';
import rootSaga from '../sagas/rootSaga';

const initializeSaga = createSagaMiddleware();
const storeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const store = createStore(rootReducer,
    storeEnhancers(
        applyMiddleware(initializeSaga)
    )
);
initializeSaga.run(rootSaga);

export default store;
