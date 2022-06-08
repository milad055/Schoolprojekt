import React from 'react';
import ReactDOM from 'react-dom/client';

import App from './App'

// Versioner före 18.0
//ReactDOM.render(document.querySelector('#root'), <App/>)
// Från och med version 18.0...
ReactDOM.createRoot(document.querySelector('#root')).render( < App / > );