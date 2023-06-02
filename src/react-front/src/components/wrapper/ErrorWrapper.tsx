import React, {ReactNode} from "react";

const ErrorWrapper = (props: { error: any, children: ReactNode }) => {
    return (props.error ? <h1>Error occurred: {props.error}</h1> : <>{props.children}</>);
}

export default ErrorWrapper
