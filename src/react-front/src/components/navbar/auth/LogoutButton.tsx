import {Button, Stack} from "@chakra-ui/react";
import ThemeButton from "../theme/ThemeButton";
import {useNavigate} from "react-router-dom";
import {useDispatch} from "react-redux";
import {logout} from "../../../redux/authSlice";
import {AuthService} from "../../../features/Auth.Service";

const LogoutButtonComponent = () => {
    const dispatch = useDispatch();
    const authService = new AuthService();
    let navigate = useNavigate();

    const onLogOut = () => {
        dispatch(logout())
        authService.logout()
        navigate('/')
    }

    return (
        <Stack
            flex={{base: 1, md: 0}}
            justify={'flex-end'}
            direction={'row'}
            spacing={6}>
            <ThemeButton/>
            <Button
                as={Button}
                fontSize={'sm'}
                fontWeight={400}
                bg={'pink.400'}
                _hover={{
                    bg: 'pink.300',
                }}
                onClick={() => dispatch(logout())}
            >
                Log out
            </Button>
        </Stack>
    )
}

export default LogoutButtonComponent
