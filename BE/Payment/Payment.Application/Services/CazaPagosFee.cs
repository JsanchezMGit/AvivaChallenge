using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;
using Payment.Enterprice.Enums;
using Payment.Enterprice.Shared;

namespace Payment.Application.Services;

public class CazaPagosFee : IFeeCalculator
{
    public CazaPagosFee()
    {
        _paymentMethodFunctions = new Dictionary<OrderPaymentMethod, Func<decimal, decimal>>
        {
            { OrderPaymentMethod.CreditCard, CalculateCreditCardFee },
            { OrderPaymentMethod.Transfer, CalculateTransferFee }
        };
    }
    private readonly Dictionary<OrderPaymentMethod, Func<decimal, decimal>> _paymentMethodFunctions;
    public OrderProvider ProviderName => OrderProvider.CazaPagos;

    public decimal Calculate(OrderRequestDTO orderRequest)
    {
        if (orderRequest == null)
            throw new ArgumentNullException(nameof(orderRequest));

        if (orderRequest.Products == null || !orderRequest.Products.Any())
            return 0m;

        var paymentMethod = orderRequest.Method.TryParse<OrderPaymentMethod>();
        if (paymentMethod == null || !_paymentMethodFunctions.TryGetValue(paymentMethod.Value, out var calculateFunction))
            throw new ArgumentException("Método de pago no soportado. Métodos válidos", nameof(orderRequest.Method));

        var amount = CalculateTotalAmount(orderRequest.Products);

        return calculateFunction(amount);
    }

    private decimal CalculateTotalAmount(IEnumerable<ProductEntity> products)
    {
        return products
            .GroupBy(p => p.Name)
            .Sum(g => g.Count() * g.First().UnitPrice);
    }

    private decimal CalculateTransferFee(decimal amount) => amount switch
    {
        <= 500 => 5m,
        <= 1000 => amount * 0.025m,
        _ => amount * 0.02m
    };
    private decimal CalculateCreditCardFee(decimal amount) => amount switch
    {
        <= 1500 => amount * 0.02m,
        <= 5000 => amount * 0.015m,
        _ => amount * 0.005m
    };

    public bool SupportsPaymentMethod(OrderPaymentMethod paymentMethod) => _paymentMethodFunctions.Any(p => p.Key == paymentMethod);
}
