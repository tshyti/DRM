/* eslint-disable operator-linebreak */
/* eslint-disable react/jsx-wrap-multilines */
/* eslint-disable react/forbid-prop-types */
import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import Input from '@material-ui/core/Input';
import InputAdornment from '@material-ui/core/InputAdornment';

import TextField from '@material-ui/core/TextField';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';

import Toolbar from '@material-ui/core/Toolbar';
import Tooltip from '@material-ui/core/Tooltip';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';

import IconButton from '@material-ui/core/IconButton';
import FilterListIcon from '@material-ui/icons/FilterList';
import DownloadIcon from '@material-ui/icons/GetApp';
import UploadIcon from '@material-ui/icons/Publish';
import DeleteIcon from '@material-ui/icons/Delete';
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import ClearIcon from '@material-ui/icons/Clear';
import PropTypes from 'prop-types';

import { useDispatch, useSelector } from 'react-redux';
import { requestUploadFile, requestDownloadUserFile, filterUserFiles, openDialog } from '../../../redux/main/actions';
import { FullDateFormatter } from '../../../services/dateParser';

const useStyles = makeStyles({
    table: {
        minWidth: 650,
    },
});

const useToolbarStyles = makeStyles(theme => ({
    root: {
        paddingLeft: theme.spacing(2),
        paddingRight: theme.spacing(1),
    },
    title: {
        flex: '1',
        display: 'flex',
        alignItems: 'center'
    },
    uploadButton: {
        marginLeft: '20px'
    },
    input: {
        display: 'none'
    }
}));

function EnhancedTableToolbar() {
    const classes = useToolbarStyles();
    const dispatcher = useDispatch();

    const filterWord = useSelector(state => state.main.filterWord);

    const handleFileUpload = event => {
        if (event.target.files[0] !== undefined) {
            dispatcher(requestUploadFile(event.target.files[0]));
        }
    };

    const handleSearchChange = event => {
        const word = event.target.value;
        dispatcher(filterUserFiles(word));
    };

    return (
        <Toolbar className={classes.root}>
            <div className={classes.title}>
                <Typography variant="h6" id="tableTitle">
                    My Files
                </Typography>

                <div className={classes.uploadButton}>
                    <input
                        accept="*"
                        className={classes.input}
                        id="text-button-file"
                        multiple={false}
                        type="file"
                        onChange={handleFileUpload}
                    />
                    <label htmlFor="text-button-file">
                        <Button
                            variant="contained"
                            color="default"
                            className={classes.uploadButton}
                            startIcon={<CloudUploadIcon />}
                            component="span"
                        >
                            Upload File
                        </Button>
                    </label>
                </div>
            </div>
            <FormControl>
                <InputLabel htmlFor="standard-adornment-password">Search Files</InputLabel>
                <Input
                    id="standard-adornment-password"
                    type="text"
                    value={filterWord}
                    onChange={handleSearchChange}
                    style={{ paddingRight: filterWord.length !== 0 ? 0 : '48px' }}
                    endAdornment={(
                        <InputAdornment position="end">
                            {filterWord.length !== 0 &&
                                <IconButton
                                    aria-label="toggle password visibility"
                                    onClick={() => dispatcher(filterUserFiles(''))}
                                >
                                    <ClearIcon />
                                </IconButton>
                            }
                        </InputAdornment>
                    )}
                />
            </FormControl>
        </Toolbar>
    );
};


export default function MainTable({ userFiles }) {
    const classes = useStyles();

    return (
        <TableContainer component={Paper}>
            <EnhancedTableToolbar />
            <Table className={classes.table} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>File Name</TableCell>
                        <TableCell align="right">Date Added</TableCell>
                        <TableCell align="right">File Size</TableCell>
                        <TableCell align="right">Actions</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {userFiles.map(file => (
                        <TableRow key={file.id}>
                            <TableCell component="th" scope="row">
                                {file.name}
                            </TableCell>
                            <TableCell align="right">{FullDateFormatter(file.createdDate)}</TableCell>
                            <TableCell align="right">test</TableCell>
                            <FileActions fileId={file.id} />
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

function FileActions({ fileId }) {
    const dispatch = useDispatch();

    return (
        <TableCell align="right">
            <Tooltip title="Download">
                <IconButton onClick={() => dispatch(requestDownloadUserFile(fileId))}>
                    <DownloadIcon />
                </IconButton>
            </Tooltip>
            <Tooltip title="Delete">
                <IconButton onClick={() => dispatch(openDialog(fileId))}>
                    <DeleteIcon />
                </IconButton>
            </Tooltip>
        </TableCell>
    );
}

MainTable.propTypes = {
    userFiles: PropTypes.array.isRequired
};

FileActions.propTypes = {
    fileId: PropTypes.string.isRequired
};
