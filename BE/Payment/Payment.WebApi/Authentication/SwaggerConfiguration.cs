using Microsoft.OpenApi.Models;

namespace Payment.WebApi.Authentication;

public static class SwaggerConfiguration
{
    public static void SetConfig(WebApplicationBuilder builder)
    { 
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "API Key necesaria para acceder a los endpoints. Agrega 'X-Api-Key: {tu-clave}' en los headers.",
                In = ParameterLocation.Header,
                Name = "X-Api-Key",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "ApiKeyScheme"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        },
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                }
            });
        });        
    }
}
