using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;
using Payment.Repository;

namespace Payment.WebApi;

public static class RepositoriesConfiguration
{
    public static void SetConfig(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRepository<OrderEntity>, OrderRepository>();
        builder.Services.AddScoped<ISyncRepository<OrderEntity>, SyncRepository>();
    }
}
