using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;

namespace Payment.Presenters;

public class OrderPresenter : IPresenter<OrderEntity, OrderViewModel>
{
    public OrderViewModel Present(OrderEntity order) => MapToViewModel(order);
    public IEnumerable<OrderViewModel> Present(IEnumerable<OrderEntity> orders) => orders.Select(MapToViewModel).ToList();

    private OrderViewModel MapToViewModel(OrderEntity order)
    {
        return new OrderViewModel
        {
            OrderId = order.OrderId,
            ProductsAmount = order.ProductsAmount,
            FeeAmount = order.FeeAmount,
            Status = order.Status.ToString(),
            PaymentMethod = order.PaymentMethod.ToString(),
            Fees = order.Fees,
            Products = order.Products,
            Provider = order.Provider.ToString()
        };
    }
}