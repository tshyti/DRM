import React from 'react';
import { Link } from 'react-router-dom';
import clsx from 'clsx';
import { useTheme } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import List from '@material-ui/core/List';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import InboxIcon from '@material-ui/icons/MoveToInbox';
import MailIcon from '@material-ui/icons/Mail';
import { useDispatch } from 'react-redux';
import PropTypes from 'prop-types';
import useStyles from '../sharedStyles/useStyles';
import { closeDrawer } from '../../redux/navigation/actions';
import SidebarContent from './SidebarContent';

export default function SideBar({ isDrawerOpened }) {
    const classes = useStyles();

    return (
        <Drawer
            variant="permanent"
            className={clsx(classes.drawer, {
                [classes.drawerOpen]: isDrawerOpened,
                [classes.drawerClose]: !isDrawerOpened,
            })}
            classes={{
                paper: clsx({
                    [classes.drawerOpen]: isDrawerOpened,
                    [classes.drawerClose]: !isDrawerOpened,
                }),
            }}
            open={isDrawerOpened}
        >
            <div className={classes.toolbar} />
            <SidebarContent />
        </Drawer>
    );
}

SideBar.propTypes = {
    isDrawerOpened: PropTypes.bool.isRequired
};
