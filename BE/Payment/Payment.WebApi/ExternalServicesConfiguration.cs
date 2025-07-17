using Payment.Adapters;
using Payment.Application.Interfaces;
using Payment.ExternalServices;

namespace Payment.WebApi;

public static class ExternalServicesConfiguration
{
    public static void SetConfig(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICazaPagosService, CazaPagosService>();
        builder.Services.AddScoped<IPagaFacilService, PagaFacilService>();
        builder.Services.AddScoped<IExternalServiceAdapter, OrderExternalServiceAdapter>();
        builder.Services.AddHttpClient<ICazaPagosService, CazaPagosService>(client =>
        {
            var baseUrl = builder.Configuration["BaseUrlCazaPagos"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("Falta la configuracion del valor de BaseUrlCazaPagos.");
            }
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("X-Api-Key", builder.Configuration["CazaPagosKey"]);    
        });

        builder.Services.AddHttpClient<IPagaFacilService, PagaFacilService>(client =>
        {
            var baseUrl = builder.Configuration["BaseUrlPagaFacil"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("Falta la configuracion del valor de BaseUrlPagaFacil.");
            }
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("X-Api-Key", builder.Configuration["PagaFacilKey"]);
        });        
    }
}
