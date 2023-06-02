import {useFormik} from "formik";
import * as Yup from 'yup';
import {Button, FormControl, FormErrorMessage, FormLabel, Input} from "@chakra-ui/react";

import {AuthService} from "../../features/Auth.Service";
import {useNavigate} from "react-router-dom";
import {useDispatch} from "react-redux";
import {login} from "../../redux/authSlice";

const SignInComponent = () => {
    const autService = new AuthService()
    let navigate = useNavigate();
    const dispatch = useDispatch();

    const formik = useFormik({
        initialValues: {
            email: '',
            password: '',
        },
        validationSchema: Yup.object().shape({
            email: Yup.string().email('Invalid email address').required('Email is required'),
            password: Yup.string().required('Password is required'),
        }),
        onSubmit: (values) => {
            autService.login(values)
                .then(token => {
                    dispatch(login(token))
                    navigate('/')
                })
                .catch(e => console.error(e))
        },
    });

    const allowSignIn = () => {
        return !formik.errors.email &&
            !formik.errors.password &&
            Boolean(formik.touched.email) &&
            Boolean(formik.touched.password)
    }

    return (
        <form onSubmit={formik.handleSubmit}>
            <FormControl isInvalid={!!(formik.touched.email && formik.errors.email)}>
                <FormLabel htmlFor="email">Email:</FormLabel>
                <Input
                    type="email"
                    id="email"
                    name="email"
                    placeholder="your@email.com"
                    value={formik.values.email}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                />
                <FormErrorMessage>{formik.errors.email}</FormErrorMessage>
            </FormControl>

            <FormControl isInvalid={!!(formik.touched.password && formik.errors.password)}>
                <FormLabel htmlFor="password">Password:</FormLabel>
                <Input
                    type="password"
                    id="password"
                    name="password"
                    placeholder="Password"
                    value={formik.values.password}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                />
                <FormErrorMessage>{formik.errors.password}</FormErrorMessage>
            </FormControl>

            <Button mt={4} colorScheme="teal" type="submit" isDisabled={!allowSignIn()}>
                Login
            </Button>
        </form>
    )
}

export default SignInComponent
