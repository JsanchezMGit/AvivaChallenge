using Payment.Application.DTOs;
using Payment.Enterprice.Entities;
using Payment.Enterprice.Enums;

namespace Payment.Application.Interfaces;

public interface IExternalServiceAdapter
{
    Task<OrderEntity> SetOrderAsync(OrderRequestDTO orderRequest, OrderProvider provider);
    Task<IEnumerable<OrderEntity>> GetOrdersAsync(OrderProvider provider);
    Task<OrderEntity> GetOrderAsync(string id, OrderProvider provider);
    Task CancelOrderAsync(string id, OrderProvider provider);
    Task PayOrderAsync(string id, OrderProvider provider);
}
