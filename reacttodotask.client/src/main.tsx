﻿import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'

const root = ReactDOM.createRoot(document.getElementById('root')!);

// Tek bir kere çağrılmış olması gerekiyor
root.render(
    <React.StrictMode>
        <App />
    </React.StrictMode>
);