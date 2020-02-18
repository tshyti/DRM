/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { useSelector } from 'react-redux';
import PropTypes from 'prop-types';

export default function ProtectedRoute({ children, accessRoles, ...rest }) {
    const isAuthenticated = useSelector(state => state.authentication.isAuthenticated);
    const userRole = useSelector(state => state.authentication.userRole);
    const isAuthorized = accessRoles.includes(userRole);
    return (
        <Route
            {...rest}
            render={({ location }) => {
                if (isAuthenticated && isAuthorized) {
                    return children;
                }
                if (isAuthenticated) {
                    return <Redirect to={{ pathname: '/app' }} />
                }
                return <Redirect to={{ pathname: '/login', state: { from: location } }} />;
            }}
        />
    );
}

ProtectedRoute.propTypes = {
    accessRoles: PropTypes.array.isRequired
};
