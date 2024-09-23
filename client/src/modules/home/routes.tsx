import { RouteObject } from "react-router-dom";
import { Navigation } from "@app/components/navigation";
import { HomePage } from "./components/home";

const ROUTES: RouteObject[] = [
	{
		path: '/',
		element: <Navigation />,
		children: [
			{
				index:true,
				element:  <HomePage />
			}
		]
	}
]

export default ROUTES;