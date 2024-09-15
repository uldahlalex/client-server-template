import ThemeSwitcher from "./ThemeSwitcher.tsx";

export default function Navigation() {
    return (
        <div className="navbar bg-base-100 h-16 min-h-[4rem]">
            <div className="flex-1">

            </div>
            <div className="flex-none">
                <ThemeSwitcher />
            </div>
        </div>
    );
}