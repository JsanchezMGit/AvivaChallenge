using Microsoft.EntityFrameworkCore;
using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Application.Services;
using Payment.Application.UseCases;
using Payment.Data;
using Payment.Enterprice.Entities;
using Payment.Mappers;
using Payment.Repository;
using Payment.WebApi;
using Payment.WebApi.Api.V1.Endpoints;
using Payment.WebApi.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
SwaggerConfiguration.SetConfig(builder);

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "OrdersDB"));
builder.Services.AddScoped<IRepository<OrderEntity>, OrderRepository>();
builder.Services.AddScoped<ISyncRepository<OrderEntity>,SyncRepository>();
builder.Services.AddScoped<IPaymentProviderSelector, PaymentProviderSelector>();
builder.Services.AddScoped<IFeeCalculator, CazaPagosFee>();
builder.Services.AddScoped<IFeeCalculator, PagaFacilFee>();
builder.Services.AddScoped<IMapper<OrderResponseDTO, OrderEntity>, OrderMapper>();
builder.Services.AddScoped<GetOrdersUseCase>();
builder.Services.AddScoped<SetOrderUseCase>();
builder.Services.AddScoped<GetOrderUseCase>();
builder.Services.AddScoped<CancelOrderUseCase>();
builder.Services.AddScoped<PayOrderUseCase>();
builder.Services.AddScoped<SyncOrdersUseCase>();
ExternalServicesConfiguration.SetConfig(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<AuthenticationMiddleware>();
app.UseAuthorization();

OrdersEndpoint.PostOrderEndpoint(app);
OrdersEndpoint.GetOrdersEndpoint(app);
OrdersEndpoint.GetOrderEndpoint(app);
OrdersEndpoint.CancelOrderEndpoint(app);
OrdersEndpoint.PayOrderEndpoint(app);

app.MapPatch("/sync/orders", async (SyncOrdersUseCase syncOrdersUseCase) =>
{ 
    await syncOrdersUseCase.ExecuteAsync();
})
.WithName("SyncOrders")
.WithOpenApi();

app.Run();