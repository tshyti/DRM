import React from 'react';
import useStyles from '../components/sharedStyles/useStyles';
import Navigation from '../components/navigation/Navigation';
import AppMainRouting from '../pages/main/AppMainRouting';

export default function MainTemplate() {
    const classes = useStyles();
    return (
        <div className={classes.root}>
            <Navigation />
            <AppMainRouting />
        </div>
    );
}
