import React, { useEffect } from 'react';
import {
    BrowserRouter, 
    Switch, 
    Route, 
    Redirect 
} from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import './App.css';
import MainTemplate from '../../templates/MainTemplate';
import Login from '../login/Login';
import { appIsStarting } from '../../redux/authentication/actions';
import SnackbarHandler from '../../components/SnackbarHandler';

function App() {
    const isAppLoading = useSelector(state => state.authentication.isAppLoading);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(appIsStarting());
    }, []);
    if (isAppLoading) {
        return <p> App is Loading </p>;
    }

    return (
        <div>
            <SnackbarHandler />
            <BrowserRouter>
                <Switch>
                    <Route exact path="/">
                        <Redirect to="/app" />
                    </Route>
                    <Route path="/app">
                        <MainTemplate />
                    </Route>
                    <Route exact path="/login">
                        <Login />
                    </Route>
                </Switch>
            </BrowserRouter>
        </div>

    );
}

export default App;
