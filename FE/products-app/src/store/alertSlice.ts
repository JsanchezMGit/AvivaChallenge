import { createSlice, type PayloadAction} from '@reduxjs/toolkit';
import {  type AlertState } from '../types/Index';

const initialState: AlertState = {
    show: false,
    message: '',
    type: 'success'
};

const alertSlice = createSlice({
  name: 'alert',
  initialState,
  reducers: {
    showAlert: (state, action: PayloadAction<{ message: string; type: 'success' | 'error' }>) => {
      state.show = true;
      state.message = action.payload.message;
      state.type = action.payload.type;
    },
    hideAlert: (state) => {
      state.show = false;
    }
  }
});

export const { showAlert, hideAlert } = alertSlice.actions;
export default alertSlice.reducer;