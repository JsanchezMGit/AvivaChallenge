using Microsoft.AspNetCore.Mvc;
using Payment.Application.DTOs;
using Payment.Application.UseCases;

namespace Payment.WebApi.Api.V1.Endpoints;

public static class OrdersEndpoint
{
    public static void PostOrderEndpoint(WebApplication app)
    {
        app.MapPost("/v1/orders", async ([FromBody] OrderRequestDTO orderRequest, [FromServices] SetOrderUseCase setOrderUseCase) =>
        {
            return await setOrderUseCase.ExecuteAsync(orderRequest);
        })
        .WithName("PostOrder")
        .WithOpenApi();
    }

    public static void GetOrdersEndpoint(WebApplication app)
    {

        app.MapGet("/v1/orders", async (GetOrdersUseCase getOrdersUseCase) =>
        {
            return await getOrdersUseCase.ExecuteAsync();
        })
        .WithName("GetOrders")
        .WithOpenApi();        
    }

    public static void GetOrderEndpoint(WebApplication app)
    {
        app.MapGet("/v1/orders/{id}", (string id) =>
        {
        })
        .WithName("GetOrder")
        .WithOpenApi();
    }

    public static void CancelOrderEndpoint(WebApplication app)
    {
        app.MapDelete("/v1/orders/{id}", (string id) =>
        {
        })
        .WithName("CancelOrder")
        .WithOpenApi();
    }
    
    public static void PayOrderEndpoint(WebApplication app)
    { 
        app.MapPatch("/v1/orders/{id}", (string id) => { 
        })
        .WithName("PayOrder")
        .WithOpenApi();
    }
}