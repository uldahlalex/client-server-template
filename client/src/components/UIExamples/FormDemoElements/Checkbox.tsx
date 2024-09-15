import {useAtom} from "jotai/index";
import {FormAtom} from "../FormDemo.tsx";

export function Checkbox() {

    const [form, setForm] = useAtom(FormAtom);

    return <label className="label cursor-pointer">
        <span className="label-text">Remember me</span>
        <input type="checkbox"
               onChange={() => setForm({...form, rememberMe: !form.rememberMe})}
               checked={form.rememberMe}
               className="checkbox"/>
    </label>
}