
using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;

namespace Payment.Adapters;

public class OrderExternalServiceAdapter : IExternalServiceAdapter<OrderEntity>
{
    private readonly Dictionary<string, Func<IExternalService>> _providerResolvers;
    private readonly IMapper<OrderResponseDTO, OrderEntity> _mapper;
    public OrderExternalServiceAdapter(
        ICazaPagosService cazaPagosService,
        IPagaFacilService pagaFacilService,
        IMapper<OrderResponseDTO, OrderEntity> mapper)
    {
        _providerResolvers = new Dictionary<string, Func<IExternalService>>
        {
            ["CazaPagos"] = () => cazaPagosService,
            ["PagaFacil"] = () => pagaFacilService
        };        
        _mapper = mapper;
    }

    public async Task<OrderEntity> GetOrderAsync(string id, string provider)
    {
        var order = await ExecuteProviderOperationAsync(
            provider,
            p => p.GetOrderAsync(id),
            _mapper.ToEntity);
        return order;
    }

    public async Task<IEnumerable<OrderEntity>> GetOrdersAsync(string provider)
    {
        var orders = await ExecuteProviderOperationAsync(
            provider,
            p => p.GetOrdersAsync(),
            orders => orders.Select(_mapper.ToEntity));
        return orders;
    }
    
    private async Task<TResult> ExecuteProviderOperationAsync<TServiceResult, TResult>(
        string providerName,
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
}