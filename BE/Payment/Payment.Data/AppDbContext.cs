using Microsoft.EntityFrameworkCore;
using Payment.Models;

namespace Payment.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<OrderModel> Orders { get; set; }
}