using Payment.Enterprice.Entities;

namespace Payment.Application.DTOs;

public class OrderRequestDTO
{
    public required string Method { get; set; }
    public List<ProductEntity>? Products { get; set; }
}
