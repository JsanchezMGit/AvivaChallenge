import { createAsyncThunk, createSlice, type PayloadAction } from '@reduxjs/toolkit';
import { 
  IDLE, LOADING, SUCCEEDED, FAILED,
  FETCH_PRODUCTS,
  type ProductDetail,
  type ProductState
} from '../types/Index';
import { productsApi } from '../api';

const initialState: ProductState = {
  products: [],
  status: IDLE,
  error: null,
};

export const fetchProducts = createAsyncThunk(FETCH_PRODUCTS, async () => {
    try {
        const response = await productsApi.get('products');
        return response.data as ProductDetail[];
    } catch (error) {
        console.error('Ocurrio un error al intentar obtener los productos', error);
        throw error;
    }
});

const productsSlice = createSlice({
  name: 'products',
  initialState,
  reducers: { },
  extraReducers: (builder) => {
    builder
      .addCase(fetchProducts.pending, (state) => {
        state.status = LOADING;
        state.error = null;
      })
      .addCase(fetchProducts.fulfilled, (state, action: PayloadAction<ProductDetail[]>) => {
        state.status = SUCCEEDED;
        state.products = action.payload;
      })
      .addCase(fetchProducts.rejected, (state, action) => {
        state.status = FAILED;
        state.error = action.error.message || 'Ocurrio un error al obtener los productos';
      });
  }
});

export const { actions, reducer } = productsSlice;

export default productsSlice.reducer;