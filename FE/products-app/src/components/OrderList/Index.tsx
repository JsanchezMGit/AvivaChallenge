import { statusClasses, statusLabels, type Order } from '../../types/Index';
import './Index.css';

interface OrderListProps {
  orders: Order[];
  onCancel: (order: string) => void;
  onPay: (order: string) => void;
}

const OrderList = ({ orders, onCancel, onPay }: OrderListProps) => {
  const handleCancelButton = (guideId: string) => {
    onCancel(guideId);
  }

  const handlePayButton = (guideId: string) => {
    onPay(guideId);
  }

  return (
    <section className="order__list" id="lista" aria-labelledby="order-title">
      <h2 id="order-title"><i className="fas fa-clipboard-list"></i> Ordenes Registradas</h2>

      <div className="table__container">
        <table>
          <thead>
            <tr>
              <th>Orden</th>
              <th>Estado</th>
              <th>Monto</th>
              <th>Comision</th>
              <th>Metodo de Pago</th>
              <th>Proveedor</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {orders.length === 0 ? (
              <tr>
                <td colSpan={7} style={{ textAlign: 'center' }}>No se encontraron guías</td>
              </tr>
            ) : (
              orders.map(order => (
                <tr key={order.orderId}>
                  <td data-label="Número de orden">{order.orderId}</td>
                  <td data-label="Estado">
                    <span className={`status-badge ${statusClasses[order.status]}`}>
                      {statusLabels[order.status]}
                    </span>
                  </td>
                  <td data-label="Monto de productos">{order.productsAmount}</td>
                  <td data-label="Monto de comisión">{order.feeAmount}</td>
                  <td data-label="Método de Pago">{order.paymentMethod}</td>
                  <td data-label="Proveedor de pago">{order.provider}</td>
                  <td>
                    <div className="action-buttons">
                      <button 
                        disabled={order.status === 'Paid' || order.status === 'Cancelled'}
                        className="action-btn action-btn--cancel" 
                        onClick={() => handleCancelButton(order.orderId)}
                        aria-label="Actualizar estatus"
                      >
                        <i className="fas fa-cancel"></i>
                      </button>
                      <button
                        disabled={order.status === 'Paid' || order.status === 'Cancelled'}
                        className="action-btn action-btn--pay"
                        onClick={() => handlePayButton(order.orderId)}
                        aria-label="Ver historial"
                      >
                        <i className="fas fa-dollar-sign"></i>
                      </button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </section>
  );
};

export default OrderList;