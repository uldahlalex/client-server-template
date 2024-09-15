import React, {useEffect, useState} from "react";
import {useAtom} from "jotai";
import {Product, Root} from "../models/DummyJsonAPI.ts";
import {useNavigate} from "react-router-dom";
import {ProductsAtom} from "../atoms/ProductsAtom.tsx";
import ProductsList from "./ProductsList.tsx";

export default function Home() {

    const [, setProducts] = useAtom(ProductsAtom);

    useEffect(() => {
        fetch('https://dummyjson.com/products')
            .then(response => response.json())
            .then((data: Root) => {
                if (data.products && data.products.length > 1)
                    setProducts(data.products as any[])
            })
    },[])

    return (
        <div>
            <h1 className="menu-title text-5xl m-5">The react template</h1>
            <p className="font-bold">This is a template for a react project with Jotai, Typescript, DaisyUI, Vite (& more)</p>

            <ProductsList />

        </div>
    );
}