import type { OrderRquest, Product } from '../../types/Index';
import { formatCurrency } from '../../utils/Index';
import './Index.css';

interface OrderFormProps {
  products: Product[];
  orderRequest: OrderRquest;
  onSetOrder: () => void;
  onSetPaymentMethod: (paymentMethod: string) => void;
}

const OrderForm = ({ products, orderRequest, onSetOrder, onSetPaymentMethod }: OrderFormProps) => {
  const handlePaymentMethodChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    if (name === 'payment-method') {
      onSetPaymentMethod(value);
    }
  };

  return (
    <section className="product__list" id="lista" aria-labelledby="product-title">
      <h2 id="product-title"><i className="fas fa-list-check"></i> Productos</h2>
      <div className="table__container">
        <table>
          <thead>
            <tr>
              <th>Producto</th>
              <th>Precio unitario</th>
            </tr>
          </thead>
          <tbody>
            {products.length === 0 ? (
              <tr>
                <td colSpan={2} style={{ textAlign: 'center' }}>No se encontraron productos</td>
              </tr>
            ) : (
              products.map(product => (
                <tr key={product.id}>
                  <td data-label="Nombre">{product.name}</td>
                  <td data-label="Precio por pieza">{formatCurrency(product.unitPrice)}</td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
      <div className='controls-container'>
        <select onChange={handlePaymentMethodChange} id="payment-method" name="payment-method" aria-label="Método de pago">
          <option value="cash">Pago en Efectivo</option>
          <option value="CreditCard">Pago con TDC</option>
          <option value="Transfer">Transferencia Bancaria</option>
        </select>
        <button disabled={orderRequest.products.length === 0} onClick={onSetOrder} type="button" className="button__prymary" aria-label="Enviar orden" aria-controls="order-form" role="button">Enviar Orden</button>
      </div>
    </section>
  );
};

export default OrderForm;