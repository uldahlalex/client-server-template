import {useAtom} from "jotai/index";
import {FormAtom} from "../FormDemo.tsx";

export function DropdownSelect() {

    const [form, setForm] = useAtom(FormAtom);

    return <select onChange={e => setForm({...form, dropdownValue: e.target.value})} value={form.dropdownValue}
                   className="select select-bordered w-full max-w-xs">
        <option disabled selected>Who shot first?</option>
        <option value="Han Solo">Han Solo</option>
        <option value="Greedo">Greedo</option>
    </select>
}