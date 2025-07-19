import { createAsyncThunk, createSlice, type PayloadAction } from '@reduxjs/toolkit';
import { 
  type Order, 
  IDLE, LOADING, SUCCEEDED, FAILED,
  FETCH_ORDERS, PATCH_ORDER,
  DELETE_ORDER,
  type OrderStatusChange,
  CREATE_ORDER,
  type OrderRquest,
  type OrderState,
  type ProductDetail,
  type Product
} from '../types/Index';
import { paymentsApi } from '../api';

const initialState: OrderState = {
  orders: [],
  orderRequest: {
    products: [],
    method: 'Cash'
  },
  status: IDLE,
  error: null
};

export const createOrder = createAsyncThunk(CREATE_ORDER, async (order: OrderRquest) => {
  try {
    const response = await paymentsApi.post('orders', order);
    return response.data as Order;
  } catch (error) {
    console.error('Ocurrio un error al intentar crear la orden', error);
    throw error;
  }
});

export const fetchOrders = createAsyncThunk(FETCH_ORDERS, async () => {
    try {
        const response = await paymentsApi.get('orders');
        return response.data as Order[];
    } catch (error) {
        console.error('Ocurrio un error al intentar obtener las ordenes', error);
        throw error;
    }
});

export const patchOrder = createAsyncThunk(PATCH_ORDER, async (id: string) => {
  try {
      const response = await paymentsApi.patch(`orders/${id}`);
      return response.data;
  } catch (error) {
      console.error(`Ocurrio un error al intentar pagar la orden`, error);
      throw error;
  }
});

export const deleteOrder = createAsyncThunk(DELETE_ORDER, async (id: string) => {
  try {
      const response = await paymentsApi.delete(`orders/${id}`);
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
    setPaymentMethod: (state, action: PayloadAction<string>) => {
      state.orderRequest.method = action.payload;
    },
    changeSelectedProduct: (state, action: PayloadAction<ProductDetail>) => {
      const selectedProduct = action.payload;
      const index = state.orderRequest.products.findIndex(product => product.id === selectedProduct.id);
      if (index === -1) {
        state.orderRequest.products.push({ id: selectedProduct.id, name: selectedProduct.name, unitPrice: selectedProduct.price } as Product);
      } else {
        state.orderRequest.products.splice(index, 1);
      }   
    },
    resetOrderRequest: (state) => {
        state.orderRequest.method = 'Cash';
        state.orderRequest.products = [];
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(createOrder.pending, (state) => {
        state.status = LOADING;
        state.error = null;
      })
      .addCase(createOrder.fulfilled, (state, action: PayloadAction<Order>) => {
        state.status = SUCCEEDED;
        state.orders.push(action.payload);
      })
      .addCase(createOrder.rejected, (state, action) => {
        state.status = FAILED;
        state.error = action.error.message || 'Ocurrio un error al crear la orden';
      })    
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
export const { setPaymentMethod, changeSelectedProduct, resetOrderRequest } = ordersSlice.actions;
export default ordersSlice.reducer;