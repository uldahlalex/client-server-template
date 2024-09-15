import {atom, useAtom} from "jotai";

interface ModalControllerForm {
    modalOpen: boolean;
}
const ModalControllerAtom = atom<ModalControllerForm>({modalOpen: false});

export default function ModalDemo() {

    const [modalController, setModalController] = useAtom(ModalControllerAtom);

    return (<>
        <button className="btn" onClick={() => {
            setModalController({modalOpen: true});
            console.log(modalController.modalOpen);
        }}>Open modal!</button>
        <Modal/>
    </>)
}

function Modal() {

    const [modalController, setModalController] = useAtom(ModalControllerAtom);

    if(!modalController.modalOpen) return null;

    return <div className="modal modal-open">
        <div className="modal-box">
            <h3 className="font-bold text-lg">Confirm?</h3>
            <div className="modal-action">
                <button className="btn btn-outline" onClick={() => {
                    const closed = {...modalController, modalOpen: false};
                    setModalController(closed);
                    console.log(modalController.modalOpen);
                }}>Cancel
                </button>
                <button className="btn btn-primary" onClick={() => {
                    const closed = {...modalController, modalOpen: false};
                    setModalController(closed);
                    console.log(modalController.modalOpen);
                }}>Confirm and close
                </button>
            </div>
        </div>
    </div>
}