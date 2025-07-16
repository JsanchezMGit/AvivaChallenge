using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;
using Payment.Enterprice.Enums;

namespace Payment.Mappers;

public class OrderMapper : IMapper<OrderResponseDTO, OrderEntity>
{
    public OrderEntity ToEntity(OrderResponseDTO dto)
    {
        var order = new OrderEntity
        {
            OrderId = dto.OrderId,
            Status = Enum.Parse<OrderStatus>(dto.Status),
            PaymentMethod = Enum.Parse<OrderPaymentMethod>(dto.Method),
            Fees = dto?.Fees?.Select(f => new FeeEntity { Amount = f.Amount, Name = f.Name })?.ToList(),
            Products = dto?.Products?.Select(p => new ProductEntity { Name = p.Name, UnitPrice = p.UnitPrice })?.ToList()
        };
        return order;
    }
}