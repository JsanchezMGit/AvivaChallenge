using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;

namespace Payment.Application.UseCases;

public class SetOrderUseCase
{
    private readonly IExternalServiceAdapter _externalServiceAdapter;
    private readonly IPaymentProviderSelector _paymentProviderSelector;
    public SetOrderUseCase(
        IExternalServiceAdapter externalServiceAdapter,
        IPaymentProviderSelector paymentProviderSelector)
    {
        _externalServiceAdapter = externalServiceAdapter;
        _paymentProviderSelector = paymentProviderSelector;
    }
    public async Task<OrderEntity> ExecuteAsync(OrderRequestDTO orderRequest)
    {
        var bestProvider = _paymentProviderSelector.SelectBetsByLowerCost(orderRequest);
        var order = await _externalServiceAdapter.SetOrderAsync(orderRequest, bestProvider);
        return order;
    }
}
