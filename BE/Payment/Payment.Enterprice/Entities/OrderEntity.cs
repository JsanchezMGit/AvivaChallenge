using Payment.Enterprice.Enums;
using Payment.Enterprice.Shared;

namespace Payment.Enterprice.Entities;

public class OrderEntity
{
    public OrderEntity() { }
    public OrderEntity(string orderId, string provider)
    {
        OrderId = orderId;
        var parsedProvider = provider.TryParse<OrderProvider>();
        Provider = parsedProvider ?? throw new ArgumentException($"Nombre de provedor invalido", nameof(provider));
    }
    public string OrderId { get; set; }
    public decimal ProductsAmount => Products?.GroupBy(c => c.Name)
                                              .Sum(c => c.Count() * c.FirstOrDefault()?.UnitPrice ?? 0) ?? 0m;
    public decimal FeeAmount => Fees?.Sum(c => c.Amount) ?? 0m;
    public OrderStatus Status { get; set; }
    public OrderPaymentMethod PaymentMethod { get; set; }
    public List<FeeEntity>? Fees { get; set; }
    public List<ProductEntity>? Products { get; set; }
    public OrderProvider Provider { get; set; }
}
