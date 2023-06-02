import {Spinner} from "@chakra-ui/react";
import React, {ReactNode} from "react";

const SpinnerWrapper = (props: { loading: boolean, children: ReactNode }) => {
    return (props.loading ? <Spinner/> : <>{props.children}</>);
}

export default SpinnerWrapper
