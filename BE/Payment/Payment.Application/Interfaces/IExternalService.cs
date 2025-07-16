using Payment.Application.DTOs;

namespace Payment.Application.Interfaces;

public interface IExternalService
{
    Task<OrderResponseDTO> SetOrderAsync(OrderRequestDTO orderRequest);
    Task<IEnumerable<OrderResponseDTO>> GetOrdersAsync();
    Task<OrderResponseDTO> GetOrderAsync(string id);
    Task CancelOrderAsync(string id);
    Task PayOrderAsync(string id);
}
