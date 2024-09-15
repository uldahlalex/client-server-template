import {atom, useAtom} from "jotai";
import {ImageUpload} from "./FormDemoElements/ImageUpload.tsx";
import {RadioButtons} from "./FormDemoElements/RadioButtons.tsx";
import {Checkbox} from "./FormDemoElements/Checkbox.tsx";
import {Slider} from "./FormDemoElements/Slider.tsx";
import {DropdownSelect} from "./FormDemoElements/DropdownSelect.tsx";
import {TextInput} from "./FormDemoElements/TextInput.tsx";

export interface Form {
    rememberMe: boolean;
    imageUpload?: File;
    imageUrl?: string;
    radioButtonValue: string;
    sliderValue: number;
    dropdownValue: string;
    textInputValue?: string;
    dateInputValue?: string;
}

export const FormAtom = atom<Form>({
    rememberMe: false,
    imageUpload: undefined,
    imageUrl: '',
    radioButtonValue: '',
    sliderValue: 40,
    dropdownValue: '',
    textInputValue: '',
    dateInputValue: new Date().toLocaleDateString()
})

export function DatePicker() {
    const [form, setForm] = useAtom(FormAtom);
    return <input type="datetime-local" className="btn" value={form.dateInputValue}
           onChange={e => setForm({...form, dateInputValue: e.target.value})}/>
}

export default function FormDemo() {

    const [form, setForm] = useAtom(FormAtom);

    return <>

        <div className="w-64 flex flex-wrap">

        <Checkbox/>

            <ImageUpload/>

            <RadioButtons/>

            <Slider/>

            <DropdownSelect />

            <TextInput/>

            <DatePicker />



            {form.imageUrl && (
                <img
                    src={form.imageUrl}
                    alt="Uploaded image"
                    className="mt-4 max-w-xs"
                />
            )}


        </div>
        <pre className="whitespace-pre-wrap break-words">
            The form state is: {JSON.stringify(form)}

        </pre>

    </>
}