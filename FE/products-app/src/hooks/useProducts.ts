import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../store/store';
import {
  fetchProducts as fetchProductsAction, 
  changeSelectedProduct as changeSelectedProductAction } from '../store/productSlice';
import type { ProductDetail } from '../types/Index';

export function useProducts() {
    const state = useSelector((state: RootState) => state.products);
    const dispatch = useDispatch<AppDispatch>();

    const fetchProducts = async () => {
        await dispatch(fetchProductsAction());
    };

    const changeSelectedProduct = (product: ProductDetail) => {
        dispatch(changeSelectedProductAction(product));
    };

  return {
    state,
    fetchProducts,
    changeSelectedProduct
  };
}