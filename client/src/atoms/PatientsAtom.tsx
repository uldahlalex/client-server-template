import {atom} from "jotai";
import {Patient} from "../Api.ts";

export const PatientsAtom = atom<Patient[]>([]);