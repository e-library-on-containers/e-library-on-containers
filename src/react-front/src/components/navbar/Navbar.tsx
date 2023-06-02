import {Box, Collapse, Flex, IconButton, useDisclosure} from "@chakra-ui/react";
import DesktopNav from "./desktop/DesktopNav";
import MobileNav from "./mobile/MobileNav";
import {BiMenu, BiX} from "react-icons/bi";
import AuthButtons from "./auth/AuthButtons";
import {useSelector} from "react-redux";
import {RootState} from "../../redux/rootReducer";
import LogoutButton from "./auth/LogoutButton";

const Navbar = () => {
    const {isOpen, onToggle} = useDisclosure();
    const jwt = useSelector((state: RootState) => state.auth.jwt);

    return (
        <Box width={"100%"} marginBottom={"1em"}>
            <Flex
                minH={'60px'}
                py={{base: 2}}
                px={{base: 4}}
                borderBottom={1}
                borderStyle={'solid'}
                align={'center'}>
                <Flex
                    flex={{base: 1, md: 'auto'}}
                    ml={{base: -2}}
                    display={{base: 'flex', md: 'none'}}>
                    <IconButton
                        onClick={onToggle}
                        icon={
                            isOpen ? <BiX size={30}/> : <BiMenu size={30}/>
                        }
                        variant={'ghost'}
                        aria-label={'Toggle Navigation'}
                    />
                </Flex>
                <Flex flex={{base: 1}} justify={{base: 'center', md: 'start'}}>
                    <Flex display={{base: 'none', md: 'flex'}} ml={10}>
                        <DesktopNav/>
                    </Flex>
                </Flex>
                {
                    jwt != null ?
                        <LogoutButton/> :
                        <AuthButtons/>
                }
            </Flex>

            <Collapse in={isOpen} animateOpacity>
                <MobileNav/>
            </Collapse>
        </Box>
    );
}

export default Navbar
