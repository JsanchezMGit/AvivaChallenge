using Payment.Enterprice.Entities;

namespace Payment.Application.DTOs;

public class OrderResponseDTO
{
    public required string OrderId { get; set; }
    public decimal Amount { get; set; }
    public required string Status { get; set; }
    public required string Method { get; set; }
    public List<FeeEntity>? Fees { get; set; }
    public List<ProductEntity>? Products { get; set; }
}