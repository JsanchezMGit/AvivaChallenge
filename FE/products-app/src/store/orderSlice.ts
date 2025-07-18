import { createAsyncThunk, createSlice, type PayloadAction } from '@reduxjs/toolkit';
import { 
  type AppState, type Order, 
  IDLE, LOADING, SUCCEEDED, FAILED,
  FETCH_ORDERS, PATCH_ORDER,
  DELETE_ORDER,
  type OrderStatusChange
} from '../types';
import paymentsApi from '../api';

const initialState: AppState = {
  orders: [],
  status: IDLE,
  error: null
};

export const fetchOrders = createAsyncThunk(FETCH_ORDERS, async () => {
    try {
        const response = await paymentsApi.paymentsApi.get('orders');
        return response.data as Order[];
    } catch (error) {
        console.error('Ocurrio un error al intentar obtener las ordenes', error);
        throw error;
    }
});

export const patchOrder = createAsyncThunk(PATCH_ORDER, async (id: string) => {
  try {
      const response = await paymentsApi.paymentsApi.patch(`orders/${id}`);
      return response.data;
  } catch (error) {
      console.error(`Ocurrio un error al intentar pagar la orden`, error);
      throw error;
  }
});

export const deleteOrder = createAsyncThunk(DELETE_ORDER, async (id: string) => {
  try {
      const response = await paymentsApi.paymentsApi.delete(`orders/${id}`);
      return response.data;
  } catch (error) {
      console.error(`Ocurrio un error al intentar cancelar la orden`, error);
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
      })
      .addCase(patchOrder.pending, (state) => {
        state.status = LOADING;
        state.error = null;
      })
      .addCase(patchOrder.fulfilled, (state, action: PayloadAction<OrderStatusChange>) => {
        state.status = SUCCEEDED;
        const index = state.orders.findIndex(o => o.orderId === action.payload.orderId);
        if (index !== -1) {
          state.orders[index].status = action.payload.status;
        }
      })
      .addCase(patchOrder.rejected, (state, action) => {
        state.status = FAILED;
        state.error = action.error.message || 'Ocurrio un error al pagar la orden';
      })
      .addCase(deleteOrder.pending, (state) => {
        state.status = LOADING;
        state.error = null;
      })
      .addCase(deleteOrder.fulfilled, (state, action: PayloadAction<OrderStatusChange>) => {
        state.status = SUCCEEDED;
        const index = state.orders.findIndex(o => o.orderId === action.payload.orderId);
        if (index !== -1) {
          state.orders[index].status = action.payload.status;
        }
      })
      .addCase(deleteOrder.rejected, (state, action) => {
        state.status = FAILED;
        state.error = action.error.message || 'Ocurrio un error al cancelar la orden';
      });
  }
});

export const { setOrders, addOrder } = ordersSlice.actions;

export default ordersSlice.reducer;