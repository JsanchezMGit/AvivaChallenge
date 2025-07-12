using Microsoft.EntityFrameworkCore;
using Products.WebApi.Data.Models;

namespace Products.WebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var defaultProducts = new List<Product>
        {
            new Product { Descrition = "32Gb / 512 SSD / i7 2.2Ghz", Name = "Dell Inspiron 5050", Id = 1, Stock = 5 },
            new Product { Descrition = "16Gb / 512 SSD / i5 1.6Ghz", Name = "Lenovo 3450", Id = 2, Stock = 3 },
            new Product { Descrition = "4 posiciones", Name = "Escritotio Flexible", Id = 3, Stock = 2 },
            new Product { Descrition = "Logitech / Cancelacion activa", Name = "Audifonos 450p", Id = 4, Stock = 0 },
        };
        modelBuilder.Entity<Product>().HasData(defaultProducts);
    }
}
