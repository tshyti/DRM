import React from 'react';
import { Switch, Route } from 'react-router-dom';
import useStyles from '../../components/sharedStyles/useStyles';
import mainRouteConfig from '../../routes/mainRoutes';
import ProtectedRoute from '../../components/route/ProtectedRoute';

export default function AppMainRouting() {
    const classes = useStyles();
    return (
        <main className={classes.content}>
            <div className={classes.content}>
                <Switch>
                    {mainRouteConfig.map(route => {
                        if (route.isProtected) {
                            return (
                                <ProtectedRoute exact path={route.path} accessRoles={route.roles} key={route.path}>
                                    <route.component />
                                </ProtectedRoute>
                            );
                        }
                        return (
                            <Route exact path={route.path} key={route.path}>
                                {route.component}
                            </Route>
                        );
                    })}
                </Switch>
            </div>
        </main>
    );
}
