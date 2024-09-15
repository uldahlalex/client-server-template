import {defineConfig} from 'vite'
import react from '@vitejs/plugin-react'
import {viteSingleFile} from "vite-plugin-singlefile";

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        react(
            {
                babel: {
                    presets: ['jotai/babel/preset'],
                },
            }
        ),
        viteSingleFile({useRecommendedBuildConfig: false})
    ],
    server: {
        port: 4200
    },

    css: {
        preprocessorOptions: {
            scss: {
                additionalData: `@import "tailwindcss/base"; @import "tailwindcss/components"; @import "tailwindcss/utilities";`
            }
        }
    }

})