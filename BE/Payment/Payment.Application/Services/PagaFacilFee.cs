using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Enums;
using Payment.Enterprice.Shared;

namespace Payment.Application.Services;

public class PagaFacilFee : IFeeCalculator
{
    public PagaFacilFee()
    {
        _paymentMethodFunctions = new Dictionary<OrderPaymentMethod, Func<OrderRequestDTO, decimal>>
        {
            { OrderPaymentMethod.Cash, _ => CalculateCashFee() },
            { OrderPaymentMethod.CreditCard, CalculateCreditCardFee }
        };
    }
    private readonly Dictionary<OrderPaymentMethod, Func<OrderRequestDTO, decimal>> _paymentMethodFunctions;
    public OrderProvider ProviderName => OrderProvider.PagaFacil;

    public decimal Calculate(OrderRequestDTO orderRequest)
    {
        if (orderRequest == null)
            throw new ArgumentNullException(nameof(orderRequest));

        var paymentMethod = orderRequest.Method.TryParse<OrderPaymentMethod>();
        if (paymentMethod == null || !_paymentMethodFunctions.TryGetValue(paymentMethod.Value, out var calculateFunction))
            throw new ArgumentException("Método de pago no soportado. Métodos válidos", nameof(orderRequest.Method));

        return calculateFunction(orderRequest);
    }

    private decimal CalculateCashFee() => 15m;

    private decimal CalculateCreditCardFee(OrderRequestDTO orderRequest)
    {
        if (orderRequest.Products == null || !orderRequest.Products.Any())
            return 0m;

        return orderRequest.Products
            .GroupBy(p => p.Name)
            .Sum(g => g.Count() * g.First().UnitPrice) * 0.01m;
    }
    
    public bool SupportsPaymentMethod(OrderPaymentMethod paymentMethod) => _paymentMethodFunctions.ContainsKey(paymentMethod);
}
