using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;

namespace Payment.Application.UseCases;

public class GetOrdersUseCase
{
    private readonly IExternalServiceAdapter<OrderEntity> _externalServiceAdapter;
    public GetOrdersUseCase(IExternalServiceAdapter<OrderEntity> externalServiceAdapter)
        => _externalServiceAdapter = externalServiceAdapter;
    public async Task<IEnumerable<OrderEntity>> ExecuteAsync()
    {
        var orders = new List<OrderEntity>();
        orders.AddRange(await _externalServiceAdapter.GetOrdersAsync("CazaPagos"));
        orders.AddRange(await _externalServiceAdapter.GetOrdersAsync("PagaFacil"));
        return orders;
    }
}