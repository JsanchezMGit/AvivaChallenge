import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../store/store';
import {
  createOrder as createOrderAction,
  fetchOrders as fectchOrdersAction,
  patchOrder as patchOrderAction,
  deleteOrder as deleteOrderAction,
  setPaymentMethod as setPaymentMethodAction,
  changeSelectedProduct as changeSelectedProductAction,
  resetOrderRequest as resetOrderRequestAction
} from '../store/orderSlice';
import type { ProductDetail } from '../types/Index';

export function useOrders() {
    const orderState = useSelector((state: RootState) => state.orders);
    const dispatch = useDispatch<AppDispatch>();

    const createOrder = async () => {
        await dispatch(createOrderAction(orderState.orderRequest));
    };

    const fetchOrders = async () => {
        await dispatch(fectchOrdersAction());
    };

    const patchOrder = async (id: string) => {
        await dispatch(patchOrderAction(id));
    };

    const deleteOrder = async (id: string) => {
        await dispatch(deleteOrderAction(id));
    };

    const setPaymentMethod = (paymentMethod: string) => {
        dispatch(setPaymentMethodAction(paymentMethod));
    };
    
    const changeSelectedProduct = (product: ProductDetail) => {
        dispatch(changeSelectedProductAction(product));
    };
    
    const resetOrderRequest = () => {
        dispatch(resetOrderRequestAction());
    };

  return {
    orderState,
    createOrder,
    fetchOrders,
    patchOrder,
    deleteOrder,
    setPaymentMethod,
    changeSelectedProduct,
    resetOrderRequest
  };
}