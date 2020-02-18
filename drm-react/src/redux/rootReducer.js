import { combineReducers } from 'redux';
import navigationReducer from './navigation/reducer';
import authenticationReducer from './authentication/reducer';
import snackbarReducer from './snackbar/reducer';
import mainReducer from './main/reducer';
import createUserReducer from '../redux/createUser/reducer';

const rootReducer = combineReducers({
    navigation: navigationReducer,
    authentication: authenticationReducer,
    snackbar: snackbarReducer,
    main: mainReducer,
    createUser: createUserReducer
});

export default rootReducer;
