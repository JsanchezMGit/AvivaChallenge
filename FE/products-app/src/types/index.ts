export type Order = {
  orderId: string;
  productsAmount: number;
  feeAmount: number;
  status: 'Pending' | 'Paid' | 'Cancelled';
  paymentMethod: string;
  fees?: Fee[];
  products?: Product[];
  provider: string;
}

export type Fee = {
  name: string;
  amount: number;
}

export type Product = {
  name: string;
  amount: number;
}

export type StatusType = 'Pending' | 'Paid' | 'Cancelled';

export const statusLabels: Record<StatusType, string> = {
  'Pending': 'Pendiente',
  'Paid': 'Pagado',
  'Cancelled': 'Cancelado'
};

export const statusClasses: Record<StatusType, string> = {
  'Pending': 'badge--pending',
  'Paid': 'badge--paid',
  'Cancelled': 'badge--cancelled'
};

export type AppState = {
  orders: Order[];
  status: string;
  error: string | null;
}

export const IDLE = "idle";
export const LOADING = "loading";
export const SUCCEEDED = "succeeded";
export const FAILED = "failed";

export const FETCH_ORDERS = "orders";
export const FETCH_ORDER = "orders/get";
export const CREATE_ORDER = "orders/post";
export const UPDATE_ORDER = "orders/put";
export const DELETE_ORDER = "orders/delete";