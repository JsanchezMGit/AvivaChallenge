using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;

namespace Payment.Application.UseCases;

public class CancelOrderUseCase
{
    private readonly IExternalServiceAdapter _externalService;
    private readonly IRepository<OrderEntity> _repository;
    public CancelOrderUseCase(
        IExternalServiceAdapter externalServiceAdapter,
        IRepository<OrderEntity> repository)
    {
        _externalService = externalServiceAdapter;
        _repository = repository;
    }
    public async Task<OrderStatusChangeDTO> ExecuteAsync(string id)
    {
        var orderModel = await _repository.GetByIdAsync(id);
        return await _externalService.CancelOrderAsync(id, orderModel.Provider);
    }
}