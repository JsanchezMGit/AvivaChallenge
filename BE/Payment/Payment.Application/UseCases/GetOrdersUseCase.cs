using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;
using Payment.Enterprice.Enums;

namespace Payment.Application.UseCases;

public class GetOrdersUseCase<TViewModel>
{
    IPresenter<OrderEntity, TViewModel> _presenter;
    private readonly IExternalServiceAdapter _externalServiceAdapter;
    public GetOrdersUseCase(IExternalServiceAdapter externalServiceAdapter, IPresenter<OrderEntity, TViewModel> presenter)
    {
        _presenter = presenter;
        _externalServiceAdapter = externalServiceAdapter;
    }
    public async Task<IEnumerable<TViewModel>> ExecuteAsync()
    {
        var orders = new List<OrderEntity>();
        orders.AddRange(await _externalServiceAdapter.GetOrdersAsync(OrderProvider.CazaPagos));
        orders.AddRange(await _externalServiceAdapter.GetOrdersAsync(OrderProvider.PagaFacil));
        return _presenter.Present(orders);
    }
}