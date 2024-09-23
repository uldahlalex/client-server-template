import {useAtom} from "jotai";
import {PatientsAtom} from "../src/atoms/PatientsAtom.tsx";
import {useEffect} from "react";
import {http} from "./MOVE/http.ts";

export function useInitializeData() {
    
    const [, setPatients] = useAtom(PatientsAtom);
    
    
    useEffect(() => {
        http.api.patientGetAllPatients().then((response) => {
            setPatients(response.data);
        }).catch(e => {
            console.log(e)
        })
    }, [])
}