import React, { useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Grid, Dialog, DialogTitle, DialogActions, Button, } from '@material-ui/core';
import DoneAllIcon from '@material-ui/icons/DoneAll';
import GetAppIcon from '@material-ui/icons/GetApp';
import PublishIcon from '@material-ui/icons/Publish';
import { useDispatch, useSelector } from 'react-redux';
import FirstRowCards from './components/FirstRowCard';
import MainTable from './components/MainTable';
import { requestGetUserFiles, closeDialog, requestDeleteFile } from '../../redux/main/actions';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        paddingTop: 30
    },
    paper: {
        padding: theme.spacing(2),
        textAlign: 'center',
        color: theme.palette.text.secondary,
    },
}));

export default function Home() {
    const classes = useStyles();
    const dispatch = useDispatch();

    const userFiles = useSelector(state => state.main.filteredFiles);
    const totalFiles = useSelector(state => state.main.totalFiles);
    const downloadNo = useSelector(state => state.main.downloadNo);
    const uploadNo = useSelector(state => state.main.uploadNo);

    const isDialogOpened = useSelector(state => state.main.isDialogOpened);

    useEffect(() => {
        dispatch(requestGetUserFiles());
    }, []);

    return (
        <div className={classes.root}>
            <Grid container spacing={2}>
                <Grid item sm={12} md={4} lg={4}>
                    <FirstRowCards number={totalFiles} text="Total Files" Icon={DoneAllIcon} iconColor="#7da5e8" />
                </Grid>
                <Grid item sm={12} md={4} lg={4}>
                    <FirstRowCards number={downloadNo} text="Downloads" Icon={GetAppIcon} iconColor="#e5779c" />
                </Grid>
                <Grid item sm={12} md={4} lg={4}>
                    <FirstRowCards number={uploadNo} text="Uploads" Icon={PublishIcon} iconColor="#7abaab" />
                </Grid>
                <Grid item xs={12}>
                    <MainTable userFiles={userFiles} />
                </Grid>
            </Grid>
            <Dialog open={isDialogOpened}>
                <DialogTitle>
                    Are you sure you want to delete this file ?
                </DialogTitle>
                <DialogActions>
                    <Button onClick={() => dispatch(requestDeleteFile())}> Yes </Button>
                    <Button onClick={() => dispatch(closeDialog())}>No</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}
