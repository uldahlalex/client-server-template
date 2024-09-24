import { RouteObject } from "react-router-dom";

import ProductsRoutes from "@modules/products/routes";

const ROUTES: RouteObject[] = [
	...ProductsRoutes,
]

export default ROUTES;