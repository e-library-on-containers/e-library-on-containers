import {Collapse, Flex, Stack, Text, useDisclosure} from "@chakra-ui/react";
import {Link} from "react-router-dom";
import {NavItem} from "../../../models/NavItem";
import React from "react";

const MobileNavItem = ({label, href}: NavItem) => {
    const {isOpen, onToggle} = useDisclosure();

    return (
        <Stack spacing={4}>
            <Flex
                py={2}
                as={Link}
                to={href ?? '#'}
                justify={'space-between'}
                align={'center'}
                _hover={{
                    textDecoration: 'none',
                }}>
                <Text fontWeight={600}>
                    {label}
                </Text>
            </Flex>

            <Collapse in={isOpen} animateOpacity style={{marginTop: '0!important'}}>
                <Stack
                    mt={2}
                    pl={4}
                    borderLeft={1}
                    borderStyle={'solid'}
                    align={'start'}>
                </Stack>
            </Collapse>
        </Stack>
    );
};

export default MobileNavItem
