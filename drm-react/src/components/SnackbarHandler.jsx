/* eslint-disable react/jsx-fragments */
import React, { Fragment, useEffect } from 'react';
import { useSnackbar } from 'notistack';
import { Button } from '@material-ui/core';
import { useSelector, useDispatch } from 'react-redux';
import { removeSnackbar } from '../redux/snackbar/actions';

export default function SnackbarHandler() {
    const snackbar = useSelector(state => state.snackbar);
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();
    const dispatch = useDispatch();

    useEffect(() => {
        if (Object.entries(snackbar).length !== 0 && snackbar.constructor === Object) {
            enqueueSnackbar(snackbar.message, {
                variant: snackbar.variant,
                autoHideDuration: snackbar.duration,
                action,
                anchorOrigin: {
                    vertical: 'top',
                    horizontal: 'right'
                }
            });
            dispatch(removeSnackbar());
        }
    });

    const action = key => (
        <Fragment>
            <Button onClick={() => closeSnackbar(key)}>
                <span style={{ color: '#fff' }}>Dismiss</span>
            </Button>
        </Fragment>
    );
    return null;
}
