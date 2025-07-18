import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../store/store';
import {
  fetchOrders as fectchOrdersAction,
  patchOrder as patchOrderAction,
  deleteOrder as deleteOrderAction
} from '../store/orderSlice';

export function useOrders() {
    const state = useSelector((state: RootState) => state.orders);
    const dispatch = useDispatch<AppDispatch>();
    const fetchOrders = async () => {
        await dispatch(fectchOrdersAction());
    };

    const patchOrder = async (id: string) => {
        await dispatch(patchOrderAction(id));
    };

    const deleteOrder = async (id: string) => {
        await dispatch(deleteOrderAction(id));
    };

  return {
    state,
    fetchOrders,
    patchOrder,
    deleteOrder
  };
}