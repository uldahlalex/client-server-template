import {useAtom} from "jotai/index";
import {FormAtom} from "../FormDemo.tsx";

export function ImageUpload() {

    const [form, setForm] = useAtom(FormAtom);
    return <>
        <input
            type="file"
            accept="image/*"
            onChange={e => {
                const file = e.target.files?.[0];
                if (file) {
                    const imageUrl = URL.createObjectURL(file);
                    setForm({...form, imageUpload: file, imageUrl: imageUrl});
                }
            }}
            className="file-input file-input-bordered w-full max-w-xs"
        />
    </>
}