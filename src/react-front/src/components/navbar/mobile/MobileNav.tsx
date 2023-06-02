import {Stack} from "@chakra-ui/react";
import NAV_ITEMS from "../../../models/NavItems";
import MobileNavItem from "./MobileNavItem";

const MobileNav = () => {
    return (
        <Stack
            p={4}
            display={{md: 'none'}}>
            {NAV_ITEMS.map((navItem) => (
                <MobileNavItem key={navItem.label} {...navItem} />
            ))}
        </Stack>
    );
};

export default MobileNav
