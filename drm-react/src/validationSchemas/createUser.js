import * as yup from 'yup';

const schema = yup.object().shape({
    firstName: yup.string()
        .required('First Name is required')
        .min(2)
        .max(20)
        .matches(new RegExp('[A-Za-z]')),
    lastName: yup.string()
        .required('Last Name is required')
        .min(2)
        .max(20)
        .matches(new RegExp('[A-Za-z]')),
    username: yup.string()
        .required('Username is required')
        .min(3)
        .max(20),
    email: yup.string()
        .required('Email is required')
        .email('Email is not valid'),
    password: yup.string()
        .required('Password is required!')
        .min(6)
        .matches(new RegExp('^(?=.*[a-z])'),
            'Password must contain at least a lower case letter!')
        .matches(new RegExp('^(?=.*[A-Z])'),
            'Password must contain at least an upper case letter!')
        .matches(new RegExp('^(?=.*[0-9])'), 'Password must contain least a number!')
        .matches(new RegExp('^(?=.*[^a-zA-Z0-9])'), 'Password must contain special character!')
});

export default schema;
