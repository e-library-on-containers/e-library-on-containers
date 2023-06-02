// authActions.ts

import { Action } from 'redux';

export interface LoginAction extends Action {
    type: 'LOGIN';
    payload: string;
}

export interface LogoutAction extends Action {
    type: 'LOGOUT';
}

export type AuthAction = LoginAction | LogoutAction;

export const login = (jwt: string): LoginAction => {
    return {
        type: 'LOGIN',
        payload: jwt,
    };
};

export const logout = (): LogoutAction => {
    return {
        type: 'LOGOUT',
    };
};
