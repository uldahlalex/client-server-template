import {useAtom} from "jotai";
import {PatientsAtom} from "../atoms/PatientsAtom.tsx";
import {useEffect} from "react";
import useHttpClient from "./useHttpClient.ts";

export function useInitializeData() {
    
    const [, setPatients] = useAtom(PatientsAtom);
    const http = useHttpClient();
    
    
    useEffect(() => {
        if(localStorage.getItem('token') === null)
            return;
        http.api.patientGetAllPatients().then((response) => {
            setPatients(response.data);
        }).catch(e => {
            console.log(e)
        })
    }, [])
}