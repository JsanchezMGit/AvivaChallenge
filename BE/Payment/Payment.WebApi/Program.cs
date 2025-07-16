using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Application.UseCases;
using Payment.Enterprice.Entities;
using Payment.Mappers;
using Payment.WebApi;
using Payment.WebApi.Api.V1.Endpoints;
using Payment.WebApi.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
SwaggerConfiguration.SetConfig(builder);

builder.Services.AddAuthorization();
builder.Services.AddScoped<IMapper<OrderResponseDTO, OrderEntity>, OrderMapper>();
builder.Services.AddScoped<GetOrdersUseCase>();
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

app.Run();