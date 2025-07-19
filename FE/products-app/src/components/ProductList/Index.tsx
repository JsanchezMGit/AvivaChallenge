import { type Product, type ProductDetail } from '../../types/Index';
import { formatCurrency } from '../../utils/Index';
import './Index.css';

interface ProductListProps {
  products: ProductDetail[];
  selectedProducts: Product[];
  onShowOrderRequest: (tittle: string) => void;
  onProductCheckChange: (product: ProductDetail) => void;
}

const OrderList = ({ products, selectedProducts, onShowOrderRequest, onProductCheckChange }: ProductListProps) => {
  return (
    <section className="product__list" id="lista" aria-labelledby="product-title">
      <h2 id="product-title"><i className="fas fa-list-check"></i> Productos</h2>
      <div className="table__container">
        <table>
          <thead>
            <tr>
              <th></th>
              <th>Producto</th>
              <th>Detalle</th>
              <th>Estatus</th>
              <th>Precio unitario</th>
            </tr>
          </thead>
          <tbody>
            {products.length === 0 ? (
              <tr>
                <td colSpan={5} style={{ textAlign: 'center' }}>No se encontraron productos</td>
              </tr>
            ) : (
              products.map(product => (
                <tr key={product.id}>
                  <td>
                    <input type="checkbox"
                      disabled={product.status !== 'Available'}
                      checked={selectedProducts.some(p => p.id === product.id)} 
                      onChange={() => onProductCheckChange(product)}  />
                  </td>
                  <td data-label="Nombre">{product.name}</td>
                  <td data-label="etalle">{product.description}</td>
                  <td data-label="Estatus">{product.status}</td>
                  <td data-label="Precio por pieza">{formatCurrency(product.price)}</td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
      <div className='controls-container'>
        <button disabled={selectedProducts.length === 0} onClick={() => onShowOrderRequest('Nueva Orden')} type="button" className="button__prymary" aria-label="Crear orden" aria-controls="order-form" role="button">Crear Orden</button>
      </div>
    </section>
  );
};

export default OrderList;