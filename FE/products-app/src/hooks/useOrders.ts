import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../store/store';
import { fetchOrders as fectchOrdersAction } from '../store/orderSlice';

export function useOrders() {
    const state = useSelector((state: RootState) => state.orders);
    const dispatch = useDispatch<AppDispatch>();
    const fetchOrders = async () => {
        await dispatch(fectchOrdersAction());
    };

  return {
    state,
    fetchOrders
  };
}