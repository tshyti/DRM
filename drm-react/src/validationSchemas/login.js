/* eslint-disable quotes */
import * as yup from 'yup';

const schema = yup.object().shape({
    username: yup.string()
    // password: yup.string()
    //     .required('Password is required!')
    //     .min(6)
    //     .matches(new RegExp('^(?=.*[a-z])'), 
    // 'Password must contain at least a lower case letter!')
    //     .matches(new RegExp('^(?=.*[A-Z])'), 
    // 'Password must contain at least an upper case letter!')
    //     .matches(new RegExp('^(?=.*[0-9])'), 'Password must contain least a number!')
    //     .matches(new RegExp('^(?=.*[^a-zA-Z0-9])'), 'Password must contain special character!')
});

export default schema;
