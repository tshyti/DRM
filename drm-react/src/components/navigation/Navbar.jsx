import React from 'react';
import clsx from 'clsx';
import AppBar from '@material-ui/core/AppBar';
import { Button, Grow } from '@material-ui/core';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import { useDispatch } from 'react-redux';
import PropTypes from 'prop-types';
import useStyles from '../sharedStyles/useStyles';
import { openDrawer, closeDrawer } from '../../redux/navigation/actions';
import useWindowDimensions from '../../hooks/useWindowDimensions';
import { requestLogout } from '../../redux/authentication/actions';

export default function Navbar({ isDrawerOpened }) {
    const classes = useStyles();
    const dispatch = useDispatch();

    const handleDrawerOpen = () => dispatch(openDrawer());
    const handleDrawerClose = () => dispatch(closeDrawer());
    const handleLogout = () => dispatch(requestLogout());

    return (
        <AppBar
            position="fixed"
            className={clsx(classes.appBar)}
        >
            <Toolbar>
                <LeftAppIcon />
                <AppTitle />
                <Button
                    color="inherit"
                    style={{ marginLeft: 'auto' }}
                    onClick={handleLogout}
                >
                    <ExitToAppIcon />  Logout
                </Button>
            </Toolbar>
        </AppBar>
    );

    function LeftAppIcon() {
        if (isDrawerOpened) {
            return (
                <IconButton
                    color="inherit"
                    arial-label="close drawer"
                    edge="start"
                    onClick={handleDrawerClose}
                    className={clsx(classes.menuButton)}
                >
                    <ChevronLeftIcon />
                </IconButton>
            );
        }
        return (
            <IconButton
                color="inherit"
                aria-label="open drawer"
                onClick={handleDrawerOpen}
                edge="start"
                className={clsx(classes.menuButton)}
            >
                <MenuIcon />
            </IconButton>
        );
    }
}

Navbar.propTypes = {
    isDrawerOpened: PropTypes.bool.isRequired
};

function AppTitle() {
    const { width } = useWindowDimensions();
    if (width < 490) {
        return (
            <Typography variant="h6" noWrap>
                DRM
            </Typography>
        );
    }
    return (
        <Typography variant="h6" noWrap>
            Document Right's Management
        </Typography>
    );
}
