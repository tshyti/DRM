import React from 'react';
import { Home, Info } from '@material-ui/icons';
import HomePage from '../pages/home/Home';
import CreateUser from '../pages/createUser/createUser';

const appHomePath = '/app';

export const appHomeRoute = {
    name: 'Home',
    drawerIcon: Home,
    path: appHomePath,
    component: HomePage,
    roles: ['Common', 'Admin'],
    isProtected: true
};

export const createUserRoute = {
    name: 'Create User',
    drawerIcon: Info,
    path: appHomePath + '/createUser',
    component: CreateUser,
    roles: ['Admin'],
    isProtected: true
};

export default [
    appHomeRoute,
    createUserRoute
];
