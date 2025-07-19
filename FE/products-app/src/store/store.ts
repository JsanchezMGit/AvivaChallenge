import { configureStore } from '@reduxjs/toolkit';
import orderReducer from './orderSlice';
import productReducer from './productSlice';
import modalReducer from './modalSlice';
import alertReducer from './alertSlice';

export const store = configureStore({
  reducer: {
    orders: orderReducer,
    products: productReducer,
    modal: modalReducer,
    alert: alertReducer
  }
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;