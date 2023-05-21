import {AuthService} from "../../features/Auth.Service";
import {useNavigate} from "react-router-dom";
import {useFormik} from "formik";
import * as Yup from "yup";
import {Button, FormControl, FormErrorMessage, FormLabel, Input} from "@chakra-ui/react";
import {User} from "../../models/User";

const SignUpComponent = () => {
    const autService = new AuthService()
    let navigate = useNavigate();

    const formik = useFormik({
        initialValues: {
            email: "",
            firstname: "",
            lastname: "",
            password: ""
        },
        validationSchema: Yup.object().shape({
            email: Yup.string().email('Invalid email address').required('Email is required'),
            firstname: Yup.string().required('First name is required'),
            lastname: Yup.string().required('Last name is required'),
            password: Yup.string().required('Password is required'),
        }),
        onSubmit: (values) => {
            autService.createAccount(values as unknown as User)
                .then(() => navigate('/'))
                .catch(e => console.error(e))
        },
    });

    const allowSignUp = () => {
        return !formik.errors.email &&
            !formik.errors.firstname &&
            !formik.errors.lastname &&
            !formik.errors.password &&
            Boolean(formik.touched.email) &&
            Boolean(formik.touched.firstname) &&
            Boolean(formik.touched.lastname) &&
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
                    value={formik.values.email}
                    placeholder='your@email.com'
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                />
                <FormErrorMessage>{formik.errors.email}</FormErrorMessage>
            </FormControl>

            <FormControl isInvalid={!!(formik.touched.firstname && formik.errors.firstname)}>
                <FormLabel htmlFor="text">First name:</FormLabel>
                <Input
                    type="text"
                    id="firstname"
                    name="firstname"
                    value={formik.values.firstname}
                    placeholder="John"
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                />
                <FormErrorMessage>{formik.errors.firstname}</FormErrorMessage>
            </FormControl>

            <FormControl isInvalid={!!(formik.touched.lastname && formik.errors.lastname)}>
                <FormLabel htmlFor="text">Last name:</FormLabel>
                <Input
                    type="text"
                    id="lastname"
                    name="lastname"
                    placeholder="Doe"
                    value={formik.values.lastname}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                />
                <FormErrorMessage>{formik.errors.lastname}</FormErrorMessage>
            </FormControl>

            <FormControl isInvalid={!!(formik.touched.password && formik.errors.password)}>
                <FormLabel htmlFor="password">Password:</FormLabel>
                <Input
                    type="password"
                    id="password"
                    name="password"
                    value={formik.values.password}
                    placeholder="Password"
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                />
                <FormErrorMessage>{formik.errors.password}</FormErrorMessage>
            </FormControl>

            <Button mt={4} colorScheme="teal" type="submit" isDisabled={!allowSignUp()}>
                Login
            </Button>
        </form>
    )
}

export default SignUpComponent
