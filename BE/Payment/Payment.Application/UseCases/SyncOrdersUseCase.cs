using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;
using Payment.Enterprice.Enums;

namespace Payment.Application.UseCases;

public class SyncOrdersUseCase
{
    private readonly IExternalServiceAdapter _externalServiceAdapter;
    private readonly ISyncRepository<OrderEntity> _repository;

    public SyncOrdersUseCase(IExternalServiceAdapter externalServiceAdapter, ISyncRepository<OrderEntity> repository)
    {
        _externalServiceAdapter = externalServiceAdapter;
        _repository = repository;
    }
    public async Task ExecuteAsync()
    {
        var orders = new List<OrderEntity>();
        orders.AddRange(await _externalServiceAdapter.GetOrdersAsync(OrderProvider.CazaPagos));
        orders.AddRange(await _externalServiceAdapter.GetOrdersAsync(OrderProvider.PagaFacil));
        await _repository.SyncOrdersAsync(orders);
    }
}
