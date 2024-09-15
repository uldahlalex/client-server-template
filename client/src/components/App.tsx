import {Route, Routes} from "react-router-dom";
import React, {useEffect} from "react";
import {Toaster} from "react-hot-toast";
import {DevTools} from "jotai-devtools";
import Navigation from "./Navigation.tsx";
import {useAtom} from "jotai";
import {ThemeAtom} from "../atoms/ThemeAtom.tsx";
import Home from "./Home.tsx";
import ProductDetail from "./ProductDetail.tsx";
import FormDemo from "./UIExamples/FormDemo.tsx";
import FlexboxDemo from "./UIExamples/FlexboxDemo.tsx";
import ModalDemo from "./UIExamples/ModalDemo.tsx";


const App = () => {

    const [theme, setTheme] = useAtom(ThemeAtom);

    useEffect(() => {
        localStorage.setItem('theme', theme);
        document.documentElement.setAttribute('data-theme', theme);
    }, [theme])

    return (<>

        <Navigation/>
        <Toaster position={"bottom-center"}/>
        {/*<FlexboxDemo />*/}
        {/*<FormDemo />*/}
        {/* <ModalDemo />*/}
        <Routes>
            <Route path="/" element={<Home/>}/>
            <Route path="/products/:id" element={<ProductDetail/>}/>
            <Route path="/ui" element={<>
                <FormDemo/>
                <FlexboxDemo />
                <ModalDemo />
            </>}/>
        </Routes>
        <DevTools/>

    </>)
}
export default App;