import { useEffect, useRef } from 'react';
import OrderList from './components/OrderList/Index';
import ProductList from './components/ProductList/Index';
import { useOrders } from './hooks/useOrders';
import { useProducts } from './hooks/useProducts';

function App() {
  const { state: orderState, fetchOrders, patchOrder, deleteOrder } = useOrders();
  const { state: productState, fetchProducts } = useProducts();

  const initialRender = useRef(true);

  useEffect(() => {
    if (initialRender.current) {
      initialRender.current = false;
      fetchProducts();
      fetchOrders();
    }
  }, []);

  return (
    <div className="App">
      <main className="main-content">
        <ProductList products={productState.products} />
        <OrderList
          orders={orderState.orders}
          onCancel={deleteOrder}
          onPay={patchOrder}
        />
      </main>
    </div>
  )
}

export default App
