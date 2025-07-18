import { createAsyncThunk, createSlice, type PayloadAction } from '@reduxjs/toolkit';
import { 
  type AppState, type Order, IDLE, LOADING, SUCCEEDED, FAILED, FETCH_ORDERS
} from '../types';
import paymentsApi from '../api';

const initialState: AppState = {
  orders: [],
  status: IDLE,
  error: null
};

export const fetchOrders = createAsyncThunk(FETCH_ORDERS, async () => {
    try {
        const response = await paymentsApi.paymentsApi.get('/orders');
        return response.data as Order[];
    } catch (error) {
        console.error('Ocurrio un error al intentar obtener las ordenes', error);
        throw error;
    }
});

const ordersSlice = createSlice({
  name: 'orders',
  initialState,
  reducers: {
    setOrders: (state, action: PayloadAction<Order[]>) => {
      state.orders = action.payload;
    },
    addOrder: (state, action: PayloadAction<Order>) => {
      state.orders.push(action.payload);
    },
    updateOrder: (state, action: PayloadAction<Order>) => {
      const index = state.orders.findIndex(order => order.orderId === action.payload.orderId);
      if (index !== -1) {
        state.orders[index] = action.payload;
      }
    },
    removeOrder: (state, action: PayloadAction<string>) => {
      state.orders = state.orders.filter(order => order.orderId !== action.payload);
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchOrders.pending, (state) => {
        state.status = LOADING;
        state.error = null;
      })
      .addCase(fetchOrders.fulfilled, (state, action: PayloadAction<Order[]>) => {
        state.status = SUCCEEDED;
        state.orders = action.payload;
      })
      .addCase(fetchOrders.rejected, (state, action) => {
        state.status = FAILED;
        state.error = action.error.message || 'Ocurrio un error al obtener las ordenes';
      });
  }
});

export const { setOrders, addOrder, updateOrder, removeOrder } = ordersSlice.actions;

export default ordersSlice.reducer;