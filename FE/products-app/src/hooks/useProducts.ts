import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../store/store';
import { fetchProducts as fetchProductsAction } from '../store/productSlice';

export function useProducts() {
    const productState = useSelector((state: RootState) => state.products);
    const dispatch = useDispatch<AppDispatch>();

    const fetchProducts = async () => {
        await dispatch(fetchProductsAction());
    };

  return {
    productState,
    fetchProducts    
  };
}