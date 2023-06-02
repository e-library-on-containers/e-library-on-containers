import {IconButton, useColorMode} from "@chakra-ui/react";
import {FaMoon, FaSun} from "react-icons/fa";

const ThemeButtonComponent = () => {
    const {colorMode, toggleColorMode} = useColorMode();
    return (
        <IconButton
            icon={colorMode === 'light' ? <FaMoon/> : <FaSun/>}
            onClick={toggleColorMode}
            variant="ghost"
            fontSize={'2xl'}
            aria-label="theme"/>
    )
}

export default ThemeButtonComponent
