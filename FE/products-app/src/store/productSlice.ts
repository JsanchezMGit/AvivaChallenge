import { createAsyncThunk, createSlice, type PayloadAction } from '@reduxjs/toolkit';
import { 
  type AppState,
  IDLE, LOADING, SUCCEEDED, FAILED,
  FETCH_PRODUCTS,
  type ProductDetail,
  type Product
} from '../types/Index';
import { productsApi } from '../api';

const initialState: AppState = {
  orders: [],
  products: [],
  selectedProducts: [],
  status: IDLE,
  error: null
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
  reducers: {
    changeSelectedProduct: (state, action: PayloadAction<ProductDetail>) => {
      const selectedProduct = action.payload;
      const index = state.selectedProducts.findIndex(product => product.id === selectedProduct.id);
      if (index === -1) {
        state.selectedProducts.push({ id: selectedProduct.id, name: selectedProduct.name, unitPrice: selectedProduct.price } as Product);
      } else {
        state.selectedProducts.splice(index, 1);
      }
    },    
  },
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

export const { changeSelectedProduct } = productsSlice.actions;

export default productsSlice.reducer;