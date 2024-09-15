import {useAtom} from "jotai/index";
import {FormAtom} from "../FormDemo.tsx";

export function Slider() {

    const [form, setForm] = useAtom(FormAtom);
    return <input type="range" min="0" max="100" value={form.sliderValue}
                  onChange={e => setForm({...form, sliderValue: Number(e.target.value)})} className="range"/>


}