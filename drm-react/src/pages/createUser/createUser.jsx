import React, { useState, useEffect } from 'react';
import {
    Grid,
    Divider,
    TextField,
    Select,
    MenuItem,
    InputLabel,
    FormControl,
    Button
} from '@material-ui/core';
import { useForm } from 'react-hook-form';
import schema from '../../validationSchemas/createUser';
import axios from '../../services/axiosConfig';
import { useSelector, useDispatch } from 'react-redux';
import { requestCreateUser } from '../../redux/createUser/actions';

const styles = {
    root: {
        flexGrow: 1,
        paddingTop: 30
    },
    paper: {
        textAlign: 'center'
    }
};

export default function CreateUser() {

    const { register, handleSubmit, watch, errors } = useForm({ validationSchema: schema });
    const token = useSelector(state => state.authentication.token);
    const dispatch = useDispatch();

    const [roles, setRoles] = useState([]);
    const [selectedRole, setSelectedRole] = useState('');

    useEffect(() => {
        const getRoles = async () => {
            const config = {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            };
            const rolesArray = await axios.get('roles', config);
            setRoles(rolesArray.data);
        };
        getRoles();
    }, []);

    const onSubmit = userForm => {
        userForm['roleID'] = selectedRole;
        dispatch(requestCreateUser(userForm));
    };

    return (
        <div style={styles.root}>
            <h2>Create User </h2>
            <Divider variant="fullWidth" />
            <form onSubmit={handleSubmit(onSubmit)}>
                <Grid container alignItems="center" spacing={3}>
                    <Grid item xs={6}>
                        <TextField
                            margin="normal"
                            fullWidth
                            id="firstName"
                            label="First Name*"
                            name="firstName"
                            inputRef={register}
                            error={errors.firstName !== undefined}
                            helperText={errors.firstName !== undefined ? errors.firstName.message : false}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            margin="normal"
                            fullWidth
                            id="lastName"
                            label="Last Name*"
                            name="lastName"
                            inputRef={register}
                            error={errors.lastName !== undefined}
                            helperText={errors.lastName !== undefined ? errors.lastName.message : false}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            margin="normal"
                            fullWidth
                            id="username"
                            label="Username*"
                            name="username"
                            inputRef={register}
                            error={errors.username !== undefined}
                            helperText={errors.username !== undefined ? errors.username.message : false}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            margin="normal"
                            fullWidth
                            id="email"
                            label="Email Address*"
                            name="email"
                            inputRef={register}
                            error={errors.email !== undefined}
                            helperText={errors.email !== undefined ? errors.email.message : false}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            margin="normal"
                            fullWidth
                            id="password"
                            type="password"
                            label="Password*"
                            name="password"
                            inputRef={register}
                            error={errors.password !== undefined}
                            helperText={errors.password !== undefined ? errors.password.message : false}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <FormControl fullWidth>
                            <InputLabel id="roleLabel">User Role*</InputLabel>
                            <Select
                                labelId="roleLabel"
                                name="roleId"
                                fullWidth
                                value={selectedRole}
                                onChange={event => setSelectedRole(event.target.value)}
                            >
                                {roles.map(role => (
                                    <MenuItem key={role.id} selected={role.id === selectedRole} value={role.id}>{role.name}</MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={2}>
                        <Button
                            fullWidth
                            type="submit"
                            variant="contained"
                            color="primary"
                        >
                            Create
                        </Button>
                    </Grid>
                </Grid>
            </form>
        </div>
    );
}
