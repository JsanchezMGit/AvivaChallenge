using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;
using Payment.Enterprice.Enums;

namespace Payment.Application.UseCases;

public class GetOrdersUseCase
{
    private readonly IExternalServiceAdapter _externalServiceAdapter;
    public GetOrdersUseCase(IExternalServiceAdapter externalServiceAdapter)
        => _externalServiceAdapter = externalServiceAdapter;
    public async Task<IEnumerable<OrderEntity>> ExecuteAsync()
    {
        var orders = new List<OrderEntity>();
        orders.AddRange(await _externalServiceAdapter.GetOrdersAsync(OrderProvider.CazaPagos));
        orders.AddRange(await _externalServiceAdapter.GetOrdersAsync(OrderProvider.PagaFacil));
        return orders;
    }
}