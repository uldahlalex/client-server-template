import {atom} from "jotai";
import {Theme} from "daisyui";

const initialTheme = localStorage.getItem('theme') || document.documentElement.getAttribute('data-theme') as Theme || 'cupcake';

export const ThemeAtom = atom<string>(initialTheme);