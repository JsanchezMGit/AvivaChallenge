import { useEffect, useRef } from 'react';
import OrderList from './components/ProductList/Index'
import { useOrders } from './hooks/useOrders'

function App() {
  const { state, fetchOrders, patchOrder, deleteOrder } = useOrders();

  const initialRender = useRef(true);

  useEffect(() => {
    if (initialRender.current) {
      initialRender.current = false;
      fetchOrders()
    }
  }, []);

  return (
    <div className="App">
      <main className="main-content">
        <OrderList
          orders={state.orders}
          onCancel={deleteOrder}
          onPay={patchOrder}
        />
      </main>
    </div>
  )
}

export default App
