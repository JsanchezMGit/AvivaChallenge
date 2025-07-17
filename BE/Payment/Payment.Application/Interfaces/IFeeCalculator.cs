using Payment.Application.DTOs;
using Payment.Enterprice.Enums;

namespace Payment.Application.Interfaces;

public interface IFeeCalculator
{
    OrderProvider ProviderName { get; }
    bool SupportsPaymentMethod(OrderPaymentMethod paymentMethod);
    decimal Calculate(OrderRequestDTO orderRequest);
}
