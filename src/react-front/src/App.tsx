import {ChakraProvider} from '@chakra-ui/react'
import './App.css'
import Navbar from "./components/navbar/Navbar";
import {Route, Routes} from "react-router-dom";
import Library from "./pages/library/Library";
import SingUp from "./pages/auth/SignUp";
import SignIn from "./pages/auth/SignIn";
import Rental from "./pages/rentals/Rental";

function App() {

    return (
        <ChakraProvider>
            <Navbar/>
            <Routes>
                <Route path="/" element={<Library/>}/>
                <Route path="/library" element={<Library/>}/>
                <Route path="/rentals" element={<Rental/>}/>
                <Route path="/sign-in" element={<SignIn/>}/>
                <Route path="/sign-up" element={<SingUp/>}/>
            </Routes>
        </ChakraProvider>
    )
}

export default App
