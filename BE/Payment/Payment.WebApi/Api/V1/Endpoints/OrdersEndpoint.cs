using Payment.Application.UseCases;

namespace Payment.WebApi.Api.V1.Endpoints;

public static class OrdersEndpoint
{
    public static void PostOrderEndpoint(WebApplication app)
    {
        app.MapPost("/orders", () =>
        {
        })
        .WithName("PostOrder")
        .WithOpenApi();
    }

    public static void GetOrdersEndpoint(WebApplication app)
    {

        app.MapGet("/orders", async (GetOrdersUseCase getOrdersUseCase) =>
        {
            return await getOrdersUseCase.ExecuteAsync();
        })
        .WithName("GetOrders")
        .WithOpenApi();        
    }

    public static void GetOrderEndpoint(WebApplication app)
    {
        app.MapGet("/orders/{id}", (string id) =>
        {
        })
        .WithName("GetOrder")
        .WithOpenApi();
    }

    public static void CancelOrderEndpoint(WebApplication app)
    {
        app.MapDelete("/orders/{id}", (string id) =>
        {
        })
        .WithName("CancelOrder")
        .WithOpenApi();
    }
    
    public static void PayOrderEndpoint(WebApplication app)
    { 
        app.MapPatch("/orders/{id}", (string id) => { 
        })
        .WithName("PayOrder")
        .WithOpenApi();
    }
}
