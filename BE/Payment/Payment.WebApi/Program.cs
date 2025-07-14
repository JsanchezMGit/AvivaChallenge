using Payment.WebApi;
using Payment.WebApi.Api.V1.Endpoints;
using Payment.WebApi.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
SwaggerConfiguration.SetConfig(builder);

builder.Services.AddAuthorization();

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