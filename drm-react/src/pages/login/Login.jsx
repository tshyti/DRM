import React, { useState } from 'react';
import { useLocation, Redirect } from 'react-router-dom';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import { CircularProgress, LinearProgress } from '@material-ui/core';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Link from '@material-ui/core/Link';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import { useDispatch, useSelector } from 'react-redux';
import { useForm } from 'react-hook-form';
import useStyles from './styles';
import { authenticateUser } from '../../redux/authentication/actions';
import schema from '../../validationSchemas/login';

export default function Login(props) {
    const classes = useStyles();
    const dispatch = useDispatch();

    const { register, handleSubmit, watch, errors } = useForm({ validationSchema: schema });
    const navLocation = useLocation();

    const error = useSelector(store => store.authentication.error);
    const isLoading = useSelector(store => store.authentication.isLoading);
    const isAuthenticated = useSelector(store => store.authentication.isAuthenticated);

    const [isSubmitDisabled, setSubmitDisabled] = useState(true);

    const handleSignIn = data => {
        if (!isLoading) {
            dispatch(authenticateUser(data));
        }
    };

    // disables the submit button if one of the fields is not completed
    const checkSubmitDisabled = () => {
        const { username, password } = watch();
        if (username.length > 0 && password.length > 0) {
            return setSubmitDisabled(false);
        }
        return setSubmitDisabled(true);
    };

    // if user is authenticated 
    // he must be redirected to the page he tried to access
    if (isAuthenticated) {
        const locationState = navLocation.state;

        // if user is redirected from another page to the login
        // he will be redirected to that page
        if (locationState) {
            return <Redirect to={locationState.from.pathname} />;
        }
        return <Redirect to="/app" />;
    }

    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <div className={classes.paper}>
                <Avatar className={classes.avatar}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Sign in
                </Typography>
                <Typography className={classes.underTitleText} variant="subtitle1">
                    Hello there! Sign in and start managing your
                    Document Right's Management account.
                </Typography>
                <form className={classes.form} noValidate onSubmit={handleSubmit(handleSignIn)}>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        onChange={checkSubmitDisabled}
                        id="username"
                        label="Username"
                        name="username"
                        autoComplete="username"
                        autoFocus
                        inputRef={register}
                        error={error.data && error.data === 'There is no user registered with this username.' ? true : false}
                        helperText={error.data && error.data === 'There is no user registered with this username.' ? error.data : false}
                    />
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        name="password"
                        label="Password"
                        type="password"
                        id="password"
                        autoComplete="current-password"
                        onChange={checkSubmitDisabled}
                        inputRef={register}
                        error={error.data && error.data === 'Incorrect password.' ? true : false}
                        helperText={error.data && error.data === 'Incorrect password.' ? error.data : false}
                    />
                    <Button
                        fullWidth
                        type="submit"
                        variant="contained"
                        color="primary"
                        className={classes.submit}
                        disabled={isSubmitDisabled}
                    >
                        <ButtonContent loading={isLoading} />
                    </Button>
                </form>
            </div>
        </Container>
    );
}

const ButtonContent = ({ loading }) => {
    if (loading) {
        return <CircularProgress size={24} style={{ color: ' #fff' }} />;
    }
    return 'Sign In';
};
