import { configureStore } from '@reduxjs/toolkit';
import orderReducer from './orderSlice';
import productReducer from './productSlice';
import modalReducer from './modalSlice';

export const store = configureStore({
  reducer: {
    orders: orderReducer,
    products: productReducer,
    modal: modalReducer
  }
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;