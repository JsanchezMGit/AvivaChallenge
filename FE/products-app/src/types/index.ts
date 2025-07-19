export type Order = {
  orderId: string;
  productsAmount: number;
  feeAmount: number;
  status: 'Pending' | 'Paid' | 'Cancelled';
  paymentMethod: string;
  fees?: Fee[];
  products: Product[];
  provider: string;
}

export type OrderRquest = {
  method: string;
  products: Product[];
}

export type OrderFormData = {
  trackingNumber: string;
  origin: string;
  destination: string;
  recipient: string;
}

export type OrderStatusChange = {
  orderId: string;
  status: 'Paid' | 'Cancelled';
}

export type Fee = {
  name: string;
  amount: number;
}

export type Product = {
  id: number;
  name: string;
  unitPrice: number;
}

export type ProductDetail = {
  id: number;
  name: string;
  description: string;
  status: string;
  price: number;
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
export type ModalState = {
  show: boolean;
  title: string;
}

export type OrderState = {
  orders: Order[];
  orderRequest: OrderRquest;
  status: string;
  error: string | null;
}

export type ProductState = {
  products: ProductDetail[];
  status: string;
  error: string | null;
}

export type AlertState = {
    show: boolean;
    message: string;
    type: 'success' | 'error';
};

export const IDLE = "idle";
export const LOADING = "loading";
export const SUCCEEDED = "succeeded";
export const FAILED = "failed";

export const FETCH_ORDERS = "orders";
export const FETCH_ORDER = "orders/get";
export const CREATE_ORDER = "orders/post";
export const PATCH_ORDER = "orders/patch";
export const DELETE_ORDER = "orders/delete";
export const FETCH_PRODUCTS = "products";