import {useAtom} from "jotai";
import {PatientsAtom} from "./atoms/PatientsAtom.tsx";
import {useEffect} from "react";
import {http} from "./http.ts";

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