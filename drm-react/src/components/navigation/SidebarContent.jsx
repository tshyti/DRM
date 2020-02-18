import React from 'react';
import {
    List, ListItem, ListItemIcon, ListItemText
} from '@material-ui/core';
import { Link, useHistory, useLocation } from 'react-router-dom';
import AppRoutes from '../../routes/mainRoutes';
import { useSelector } from 'react-redux';

export default function SidebarContent() {
    const history = useHistory();

    const pushRoute = route => {
        const currentRoute = history.location;
        if (route.path === currentRoute.pathname) return;
        history.push(route.path);
    };

    const userRole = useSelector(state => state.authentication.userRole);

    return (
        <List style={{ paddingTop: 0 }}>
            {AppRoutes.map(route => {
                if (!route.roles.includes(userRole)) {
                    return null;
                }
                return (
                    <ListItem
                        button
                        selected={history.location.pathname === route.path}
                        onClick={() => pushRoute(route)}
                        key={route.path}
                    >
                        <ListItemIcon>
                            <route.drawerIcon />
                        </ListItemIcon>
                        <ListItemText> {route.name} </ListItemText>
                    </ListItem>
                )
            })}
        </List>
    );
}
