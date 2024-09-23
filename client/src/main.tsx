import { StrictMode } from "react";
import ReactDOM from 'react-dom/client'
import App from '@app/App'
import '@assets/styles/styles.css';
import 'jotai-devtools/styles.css';


ReactDOM.createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <App/>
    </StrictMode>
)