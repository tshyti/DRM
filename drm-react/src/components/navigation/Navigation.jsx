import React from 'react';
import { CssBaseline } from '@material-ui/core';
import { useSelector } from 'react-redux';
import Navbar from './Navbar';
import SideBar from './Sidebar';

export default function Navigation() {
    const isDrawerOpened = useSelector(state => state.navigation.isDrawerOpened);

    return (
        <div>
            <CssBaseline />
            <Navbar isDrawerOpened={isDrawerOpened} />
            <SideBar isDrawerOpened={isDrawerOpened} />
        </div>
    );
}
