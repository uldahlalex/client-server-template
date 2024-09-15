import {useAtom} from "jotai/index";
import {FormAtom} from "../FormDemo.tsx";

export function TextInput() {

    const [form, setForm] = useAtom(FormAtom);

    return <input value={form.textInputValue} onChange={e => setForm({...form, textInputValue: e.target.value})}
                  type="text" placeholder="Type here" className="input input-bordered w-full max-w-xs"/>

}