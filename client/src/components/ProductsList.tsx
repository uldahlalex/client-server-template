import {ProductsAtom} from "../atoms/ProductsAtom.tsx";
import {useAtom} from "jotai";
import {useNavigate} from "react-router-dom";

export default function ProductsList() {

    const [products] = useAtom(ProductsAtom);
    const navigate = useNavigate();

    return(<>
        <h1 className="menu-title mt-10 flex">Products from an API</h1>
        {
            products.length > 0 && (
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                    {products.map(product => (
                        <div onClick={e => navigate('/products/'+product.id)} key={product.id} className="card bordered">
                            <div className="card-body">
                                <h2 className="card-title">{product.title}</h2>
                                <p className="text-lg">${product.price}</p>
                            </div>
                        </div>
                    ))}
                </div>

            )
        }
    </>)
}