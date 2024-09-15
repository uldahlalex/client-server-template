export default function FlexboxDemo() {

    function CenteredInADiv() {
        return <div className="border flex justify-center items-center h-20">
            <button className="btn btn-primary">This button is centered (both X and Y)</button>
        </div>
    }

    function SpaceAround() {
        return <div className="border flex justify-around">
            <button className="btn btn-primary">Space around</button>
            <button className="btn btn-primary">Space around</button>
        </div>
    }

    function MultipleLinesOfElements() {
        return <div className="border flex flex-wrap">
            <button className="btn btn-primary">Multiple lines if not enough space</button>
            <button className="btn btn-primary">Multiple lines if not enough space</button>
            <button className="btn btn-primary">Multiple lines if not enough space</button>
            <button className="btn btn-primary">Multiple lines if not enough space</button>
            <button className="btn btn-primary">Multiple lines if not enough space</button>
        </div>



    }

    return <>
        <CenteredInADiv/>
        <SpaceAround/>
        <MultipleLinesOfElements/>
    </>
}