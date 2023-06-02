import {Box, Link, Popover, PopoverTrigger, Stack} from "@chakra-ui/react";
import {Link as RouterLink} from "react-router-dom";
import NAV_ITEMS from "../../../models/NavItems";

const DesktopNav = () => {

    return (
        <Stack direction={'row'} spacing={4}>
            {NAV_ITEMS.map((navItem) => (
                <Box key={navItem.label}>
                    <Popover trigger={'hover'} placement={'bottom-start'}>
                        <PopoverTrigger>
                            <Link
                                p={2}
                                as={RouterLink}
                                to={navItem.href ?? '#'}
                                fontSize={'xl'}
                                fontWeight={500}
                                _hover={{
                                    textDecoration: 'none'
                                }}>
                                {navItem.label}
                            </Link>
                        </PopoverTrigger>
                    </Popover>
                </Box>
            ))}
        </Stack>
    );
};

export default DesktopNav
