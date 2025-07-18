
using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;
using Payment.Enterprice.Enums;

namespace Payment.Adapters;

public class OrderExternalServiceAdapter : IExternalServiceAdapter
{
    private readonly Dictionary<OrderProvider, Func<IExternalService>> _providerResolvers;
    private readonly IMapper<OrderResponseDTO, OrderEntity> _mapper;
    public OrderExternalServiceAdapter(
        ICazaPagosService cazaPagosService,
        IPagaFacilService pagaFacilService,
        IMapper<OrderResponseDTO, OrderEntity> mapper)
    {
        _providerResolvers = new Dictionary<OrderProvider, Func<IExternalService>>
        {
            [OrderProvider.CazaPagos] = () => cazaPagosService,
            [OrderProvider.PagaFacil] = () => pagaFacilService
        };
        _mapper = mapper;
    }

    public async Task CancelOrderAsync(string id, OrderProvider provider) =>
        await ExecuteProviderOperationAsync(provider, p => p.CancelOrderAsync(id));

    public async Task<OrderEntity> GetOrderAsync(string id, OrderProvider provider)
    {
        var order = await ExecuteProviderOperationAsync(
            provider,
            p => p.GetOrderAsync(id),
            _mapper.ToEntity);
        order.Provider = provider;
        return order;
    }

    public async Task<IEnumerable<OrderEntity>> GetOrdersAsync(OrderProvider provider)
    {
        var orders = await ExecuteProviderOperationAsync(
            provider,
            p => p.GetOrdersAsync(),
            orders => orders.Select(_mapper.ToEntity).ToList());

        foreach (var order in orders)
            order.Provider = provider;
        return orders;
    }

    public async Task PayOrderAsync(string id, OrderProvider provider) =>
        await ExecuteProviderOperationAsync(provider, p => p.PayOrderAsync(id));

    public async Task<OrderEntity> SetOrderAsync(OrderRequestDTO orderRequest, OrderProvider provider)
    {
        var order = await ExecuteProviderOperationAsync(
            provider,
            p => p.SetOrderAsync(orderRequest),
            _mapper.ToEntity);
        order.Provider = provider;
        return order;
    }

    private async Task<TResult> ExecuteProviderOperationAsync<TServiceResult, TResult>(
        OrderProvider providerName,
        Func<IExternalService, Task<TServiceResult>> providerOperation,
        Func<TServiceResult, TResult> resultMapper)
    {
        if (!_providerResolvers.TryGetValue(providerName, out var resolver))
        {
            throw new ArgumentException($"Proveedor no soportado: {providerName}");
        }

        var provider = resolver();
        var result = await providerOperation(provider);
        return resultMapper(result);
    }
    
    private async Task ExecuteProviderOperationAsync(
        OrderProvider providerName,
        Func<IExternalService, Task> providerOperation)
    {
        if (!_providerResolvers.TryGetValue(providerName, out var resolver))
        {
            throw new ArgumentException($"Proveedor no soportado: {providerName}");
        }

        var provider = resolver();
        await providerOperation(provider);
    }     
}