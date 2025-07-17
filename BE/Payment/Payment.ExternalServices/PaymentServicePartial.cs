using System.Text.Json;

namespace Payment.ExternalServices;

public partial class PaymentServicePartial
{
    protected readonly HttpClient _httpClient;
    protected readonly JsonSerializerOptions _options;
    public PaymentServicePartial(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }
}
