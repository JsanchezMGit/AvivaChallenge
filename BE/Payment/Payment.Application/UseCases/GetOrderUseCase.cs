using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;

namespace Payment.Application.UseCases;

public class GetOrderUseCase
{
    private readonly IExternalServiceAdapter _externalServiceAdapter;
    private readonly IRepository<OrderEntity> _repository;

    public GetOrderUseCase(IExternalServiceAdapter externalServiceAdapter, IRepository<OrderEntity> repository)
    {
        _externalServiceAdapter = externalServiceAdapter;
        _repository = repository;
    }
    public async Task<OrderEntity> ExecuteAsync(string id)
    {
        var orderModel = await _repository.GetByIdAsync(id);
        var orderEntity = await _externalServiceAdapter.GetOrderAsync(id, orderModel.Provider);
        return orderEntity;
    }
}
