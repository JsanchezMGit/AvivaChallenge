using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Products.WebApi.Data;
using Products.WebApi.Middlewares;
using Products.WebApi.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedFrontend",
        policy => policy
            .WithOrigins("http://localhost:5555")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddEndpointsApiExplorer();
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

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "ProductsDB"));
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowedFrontend");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<AuthenticationMiddleware>();
app.UseAuthorization();

app.MapGet("/products", (AppDbContext dbcontext) =>
{
    var productList = dbcontext.Products.Select(p => new ProductViewModel(p.Id, p.Name, p.Descrition, p.Stock, p.Price)).ToList();
    return productList;
})
.WithName("GetProducts")
.WithOpenApi();

app.Run();
