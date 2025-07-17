using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Enums;
using Payment.Enterprice.Shared;

namespace Payment.Application.Services;

public class PaymentProviderSelector : IPaymentProviderSelector
{
    private readonly IEnumerable<IFeeCalculator> _calculators;

    public PaymentProviderSelector(IEnumerable<IFeeCalculator> calculators)
    {
        _calculators = calculators;
    }

    public OrderProvider SelectBetsByLowerCost(OrderRequestDTO orderRequest)
    {
        if (orderRequest == null)
            throw new ArgumentNullException(nameof(orderRequest));

        var supportedCalculators = _calculators
            .Where(c =>
            {
                var paymentMethod = orderRequest.Method.TryParse<OrderPaymentMethod>();
                return paymentMethod.HasValue && c.SupportsPaymentMethod(paymentMethod.Value);
            })
            .ToList();

        if (!supportedCalculators.Any())
            throw new InvalidOperationException($"Ningún proveedor soporta el método de pago: {orderRequest.Method}");

        return supportedCalculators
            .Select(c => new 
            {
                Provider = c.ProviderName,
                Fee = c.Calculate(orderRequest)
            })
            .OrderBy(x => x.Fee)
            .First()
            .Provider;
    } 
}
