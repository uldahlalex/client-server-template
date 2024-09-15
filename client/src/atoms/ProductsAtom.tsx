import {Product} from "../models/DummyJsonAPI.ts";
import {atom} from "jotai";

export const ProductsAtom = atom<Product[]>([]);