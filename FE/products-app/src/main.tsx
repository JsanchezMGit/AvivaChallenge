import React from 'react';
import { Provider } from 'react-redux';
import { createRoot } from 'react-dom/client'
import { store } from './store/store';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import '@fortawesome/fontawesome-free/css/all.min.css';
import './main.css'
import App from './App.tsx'

library.add(fas);

createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <App />
    </Provider>
  </React.StrictMode>
)
