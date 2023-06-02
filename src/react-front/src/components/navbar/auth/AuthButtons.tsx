import {Button, Stack} from "@chakra-ui/react";
import ThemeButton from "../theme/ThemeButton";
import {Link} from "react-router-dom";

const AuthButtonsComponent = () => {
    return (
        <Stack
            flex={{base: 1, md: 0}}
            justify={'flex-end'}
            direction={'row'}
            spacing={6}>
            <ThemeButton/>
            <Button
                as={Link}
                fontSize={'sm'}
                fontWeight={400}
                bg={'pink.400'}
                to={'/sign-in'}
                _hover={{
                    bg: 'pink.300',
                }}
            >
                Sign In
            </Button>
            <Button
                as={Link}
                fontSize={'sm'}
                fontWeight={400}
                bg={'pink.400'}
                to={'/sign-up'}
                _hover={{
                    bg: 'pink.300',
                }}
            >
                Sign Up
            </Button>
        </Stack>
    )
}

export default AuthButtonsComponent
