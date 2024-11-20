import React, {useEffect} from "react";
import {useAtom} from "jotai";
import {PatientsAtom} from "../atoms/PatientsAtom.tsx";
import {useInitializeData} from "../hooks/useInitializeData.ts";

export default function Home() {

    const [patients, setPatients] = useAtom(PatientsAtom);

    useEffect(() => {
        localStorage.setItem('token', 'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAiLCJleHAiOjE3MzI2OTUyMzUsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJhbGV4QHVsZGFobC5kayIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMmJiMjFlOWEtZmMwYi00Y2RlLTgyZjctZGM2YWJmYTYxNzAyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiUmVhZGVyIiwiaWF0IjoxNzMyMDkwNDM1LCJuYmYiOjE3MzIwOTA0MzV9.G5wYJZHXwF4-vSOoKG0Pa3MB6fxnaaL5RjtQ-cYhH2K4Mzn_SVrB0bN8Aa1LHbiV1DYxyezbOozeC82C7n5a5w')
    },[])
    
    useInitializeData();

    return (
        <div>
            <h1 className="menu-title text-5xl m-5">The react template</h1>
            <p className="font-bold">This is a template for a react project with Jotai, Typescript, DaisyUI, Vite (& more)</p>

            {
                patients.map((patient) => (
                    <div key={patient.id} className="card bordered m-4">
                        <div className="card-body">
                            {JSON.stringify(patient)}
                        </div>
                    </div>
                ))
            }

        </div>
    );
}