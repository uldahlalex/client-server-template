import { RouteObject } from "react-router-dom";
import { Navigation } from "@app/components/navigation";
import { ProductsPage } from "./components/products";

const ROUTES: RouteObject[] = [
	{
		path: '/',
		element: <Navigation />,
		children: [
			{
				index:true,
				element:  <ProductsPage />
			}
		]
	}
]

export default ROUTES;