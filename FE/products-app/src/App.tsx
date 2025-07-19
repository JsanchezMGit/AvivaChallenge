import { useEffect, useRef } from 'react';
import OrderList from './components/OrderList/Index';
import ProductList from './components/ProductList/Index';
import Modal from './components/Modal/Index';
import { useOrders } from './hooks/useOrders';
import { useProducts } from './hooks/useProducts';
import { useModal } from './hooks/useModal';
import OrderForm from './components/OrderForm/Index';

function App() {
  const { orderState, createOrder, fetchOrders, patchOrder, deleteOrder, setPaymentMethod, changeSelectedProduct } = useOrders();
  const { productState, fetchProducts} = useProducts();
  const { modalState, showModal, hideModal } = useModal();

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
        <ProductList
          products={productState.products}
          selectedProducts={orderState.orderRequest.products}
          onShowOrderRequest={showModal}
          onProductCheckChange={changeSelectedProduct}
        />
        <OrderList
          orders={orderState.orders}
          onCancel={deleteOrder}
          onPay={patchOrder}
        />
      </main>
      
      <Modal 
        isOpen={modalState.show}
        onClose={hideModal}
        title={modalState.title}
      >
        <OrderForm 
          products={orderState.orderRequest.products}
          orderRequest={orderState.orderRequest}
          onSetOrder={createOrder}
          onSetPaymentMethod={setPaymentMethod}
        />

      </Modal>      
    </div>
  )
}

export default App
