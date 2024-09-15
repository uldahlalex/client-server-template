import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {Product} from "../models/DummyJsonAPI.ts";

export default function ProductDetail() {
    const params = useParams();
    const [currentProduct, setCurrentProduct] = useState<Product | null>(null);

    useEffect(() => {
        fetch('https://dummyjson.com/products/'+params.id)
            .then(response => response.json())
            .then(data => {

                setCurrentProduct(data as Product)
            });
    }, [])

    return (
        <div>
            {
                currentProduct && (
                    <div className="card bordered">
                        <div className="card-body">
                            <h2 className="card-title">{currentProduct.title}</h2>
                            <p className="text-lg">${currentProduct.price}</p>
                            <p>{currentProduct.description}</p>
                        </div>
                    </div>
                )
            }
        </div>
    )
}