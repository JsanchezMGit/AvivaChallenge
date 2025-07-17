using System.Text.Json;
using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Enums;
using Payment.Enterprice.Shared;
using Payment.Mappers;

namespace Payment.ExternalServices;

public class CazaPagosService : PaymentServicePartial, ICazaPagosService
{
    public CazaPagosService(HttpClient httpClient) : base(httpClient){ }

    public async Task CancelOrderAsync(string id)
    {
        var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/cancel?id={id}", new StringContent(string.Empty));
        response.EnsureSuccessStatusCode();
    }

    public async Task<OrderResponseDTO> GetOrderAsync(string id)
    {
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Order/{id}");
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
        var oreder = JsonSerializer.Deserialize<OrderResponseDTO>(responseData, _options);
        if (oreder == null)
            throw new InvalidOperationException("Error obtener la orden con la respuesta de CazaPagos.");
        return oreder;
    }

    public async Task<IEnumerable<OrderResponseDTO>> GetOrdersAsync()
    {
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Order");
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
        var oreder = JsonSerializer.Deserialize<IEnumerable<OrderResponseDTO>>(responseData, _options);
        if (oreder == null)
            throw new InvalidOperationException("Error obtener las ordenes con la respuesta de CazaPagos.");
        return oreder;
    }

    public async Task PayOrderAsync(string id)
    {
        var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/pay?id={id}", new StringContent(string.Empty));
        response.EnsureSuccessStatusCode();
    }

    public async Task<OrderResponseDTO> SetOrderAsync(OrderRequestDTO orderRequest)
    {        
        var paymentMethod = orderRequest.Method.TryParse<OrderPaymentMethod>();
        if (!paymentMethod.HasValue)
            throw new ArgumentException("Invalid payment method.", nameof(orderRequest.Method));
        orderRequest.Method = PaymentMethodMapper.ToProviderMethod(OrderProvider.CazaPagos, paymentMethod.Value);
        var requestString = JsonSerializer.Serialize(orderRequest, _options);
        var content = new StringContent(requestString, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Order", content);
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
        var order = JsonSerializer.Deserialize<OrderResponseDTO>(responseData, _options);
        if (order == null)
            throw new InvalidOperationException("Error obtener la orden con la respuesta de CazaPagos.");
        return order;
    }
}