import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import {TokenResponse} from "../models/TokenResponse";

interface AuthState {
    jwt: TokenResponse | null;
}

const initialState: AuthState = {
    jwt: null,
};

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        login: (state, action: PayloadAction<TokenResponse>) => {
            state.jwt = action.payload;
        },
        logout: (state) => {
            state.jwt = null;
        },
    },
});

export const { login, logout } = authSlice.actions;
export default authSlice.reducer;
