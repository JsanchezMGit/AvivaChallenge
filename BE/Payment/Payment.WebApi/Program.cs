using Microsoft.EntityFrameworkCore;
using Payment.Application.UseCases;
using Payment.Data;
using Payment.WebApi;
using Payment.WebApi.Api.V1.Endpoints;
using Payment.WebApi.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
SwaggerConfiguration.SetConfig(builder);

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "OrdersDB"));
RepositoriesConfiguration.SetConfig(builder);
AdaptersConfiguration.SetConfig(builder);
UseCasesConfiguration.SetConfig(builder);
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