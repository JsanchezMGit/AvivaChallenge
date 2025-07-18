import { useEffect, useRef } from 'react';
import OrderList from './components/ProductList/Index'
import { useOrders } from './hooks/useOrders'

function App() {
  const { state, fetchOrders } = useOrders();

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
          onCancel={() => fetchOrders()} 
          onUpdate={() => fetchOrders()} 
        />
      </main>
    </div>
  )
}

export default App
