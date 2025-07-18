using Payment.Application.UseCases;
using Payment.Presenters;

namespace Payment.WebApi;

public static class UseCasesConfiguration
{
    public static void SetConfig(WebApplicationBuilder builder)
    { 
        builder.Services.AddScoped<GetOrdersUseCase<OrderViewModel>>();
        builder.Services.AddScoped<GetOrdersUseCase<OrderViewModel>>();
        builder.Services.AddScoped<SetOrderUseCase<OrderViewModel>>();
        builder.Services.AddScoped<GetOrderUseCase<OrderViewModel>>();
        builder.Services.AddScoped<CancelOrderUseCase>();
        builder.Services.AddScoped<PayOrderUseCase>();
    }
}
