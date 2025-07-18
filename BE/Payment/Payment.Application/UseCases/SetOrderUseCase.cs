using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;

namespace Payment.Application.UseCases;

public class SetOrderUseCase
{
    private readonly IExternalServiceAdapter _externalServiceAdapter;
    private readonly IPaymentProviderSelector _paymentProviderSelector;
    private readonly IRepository<OrderEntity> _repository;
    public SetOrderUseCase(
        IExternalServiceAdapter externalServiceAdapter,
        IPaymentProviderSelector paymentProviderSelector,
        IRepository<OrderEntity> repository)
    {
        _externalServiceAdapter = externalServiceAdapter;
        _paymentProviderSelector = paymentProviderSelector;
        _repository = repository;
    }
    public async Task<OrderEntity> ExecuteAsync(OrderRequestDTO orderRequest)
    {
        var bestProvider = _paymentProviderSelector.SelectBetsByLowerCost(orderRequest);
        var order = await _externalServiceAdapter.SetOrderAsync(orderRequest, bestProvider);
        await _repository.AddAsync(order);
        return order;
    }
}
