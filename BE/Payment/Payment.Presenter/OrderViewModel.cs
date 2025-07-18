using Payment.Enterprice.Entities;

namespace Payment.Presenters;

public class OrderViewModel
{
    public required string OrderId { get; set; }
    public decimal ProductsAmount { get; set; }
    public decimal FeeAmount { get; set; }
    public required string Status { get; set; }
    public required string PaymentMethod { get; set; }
    public List<FeeEntity>? Fees { get; set; }
    public List<ProductEntity>? Products { get; set; }
    public required string Provider { get; set; }
}
