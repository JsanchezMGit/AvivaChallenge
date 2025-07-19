import { createSlice, type PayloadAction} from '@reduxjs/toolkit';
import {  type ModalState } from '../types/Index';

const initialState: ModalState = {
  show: false,
  title: ''
};

const modalSlice = createSlice({
  name: 'modal',
  initialState,
  reducers: {
    showModal: (state, action: PayloadAction<string>) => {
      state.show = true;
      state.title = action.payload;
    },
    hideModal: (state) => {
      state.show = false;
    }
  }
});

export const { showModal, hideModal } = modalSlice.actions;

export default modalSlice.reducer;