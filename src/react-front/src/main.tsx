import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import {ChakraProvider} from '@chakra-ui/react'
import './index.css'
import {BrowserRouter} from 'react-router-dom'
import rootReducer from "./redux/rootReducer";
import {configureStore} from "@reduxjs/toolkit";
import {Provider} from "react-redux";

const store = configureStore({
    reducer: rootReducer,
});

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <React.StrictMode>
        <ChakraProvider>
            <Provider store={store}>
                <BrowserRouter>
                    <App/>
                </BrowserRouter>
            </Provider>
        </ChakraProvider>
    </React.StrictMode>
)
