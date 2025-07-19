import { useEffect, useRef } from 'react';
import OrderList from './components/OrderList/Index';
import ProductList from './components/ProductList/Index';
import Modal from './components/Modal/Index';
import { useOrders } from './hooks/useOrders';
import { useProducts } from './hooks/useProducts';
import { useModal } from './hooks/useModal';
import OrderForm from './components/OrderForm/Index';
import Alert from './components/Alert/Index';
import { useAlert } from './hooks/useAlert';

function App() {
  const { orderState, createOrder, fetchOrders, patchOrder, 
          deleteOrder, setPaymentMethod, changeSelectedProduct,
          resetOrderRequest
        } = useOrders();
  const { productState, fetchProducts} = useProducts();
  const { modalState, showModal, hideModal } = useModal();
  const { alertState, hideAlert, showAlert } = useAlert();

  const initialRender = useRef(true);

  const handleSetOrderRequest = async () => {
    await createOrder().then(() => {
      resetOrderRequest();
      hideModal();
      showAlert('Orden creada con éxito', 'success');
    }).catch((error) => {
      showAlert(error.message, 'error'); 
    });
  };

  const handlePayOrder = async (id: string) => {
    await patchOrder(id).then(() => {
      showAlert('Orden pagada con éxito', 'success');
    }).catch((error) => {
      showAlert(error.message, 'error'); 
    });
  };
  
  const handleCancelOrder = async (id: string) => {
    await deleteOrder(id).then(() => {
      showAlert('Orden cancelada con éxito', 'success');
    }).catch((error) => {
      showAlert(error.message, 'error'); 
    });
  };    

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
          onCancel={handleCancelOrder}
          onPay={handlePayOrder}
        />
      </main>
      
      {alertState.show && (
        <Alert 
          message={alertState.message}
          type={alertState.type}
          onClose={hideAlert}
        />
      )}

      <Modal 
        isOpen={modalState.show}
        onClose={hideModal}
        title={modalState.title}
      >
        <OrderForm 
          products={orderState.orderRequest.products}
          orderRequest={orderState.orderRequest}
          onSetOrder={handleSetOrderRequest}
          onSetPaymentMethod={setPaymentMethod}
        />
      </Modal>      
    </div>
  )
}

export default App;