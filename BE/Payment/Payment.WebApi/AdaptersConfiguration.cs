using Payment.Application.DTOs;
using Payment.Application.Interfaces;
using Payment.Enterprice.Entities;
using Payment.Mappers;
using Payment.Presenters;

namespace Payment.WebApi;

public static class AdaptersConfiguration
{
    public static void SetConfig(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMapper<OrderResponseDTO, OrderEntity>, OrderMapper>();
        builder.Services.AddScoped<IPresenter<OrderEntity, OrderViewModel>, OrderPresenter>();
    }
}
