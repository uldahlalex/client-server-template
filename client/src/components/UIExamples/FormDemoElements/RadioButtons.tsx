import {useAtom} from "jotai/index";
import {FormAtom} from "../FormDemo.tsx";

export function RadioButtons() {

    const [form, setForm] = useAtom(FormAtom);

    return <>
        <label className="label cursor-pointer">
            <span className="label-text">first</span>
            <input type="radio" onChange={e => setForm({
                ...form,
                radioButtonValue: e.target.value
            })} value="first" className="radio" checked={form.radioButtonValue === "first"}/>
        </label>

        <label className="label cursor-pointer">
            <span className="label-text">second</span>
            <input type="radio" onChange={e => setForm({...form, radioButtonValue: e.target.value})} value="second"
                   className="radio" checked={form.radioButtonValue === "second"}/>
        </label>
    </>
}