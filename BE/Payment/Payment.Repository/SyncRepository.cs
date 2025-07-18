using Payment.Application.Interfaces;
using Payment.Data;
using Payment.Enterprice.Entities;
using Payment.Models;

namespace Payment.Repository;

public class SyncRepository : ISyncRepository<OrderEntity>
{
    public SyncRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly AppDbContext _dbContext;
    public async Task SyncOrdersAsync(IEnumerable<OrderEntity> orders)
    {
        foreach (var order in orders)
        {
            var orderModel = new OrderModel
            {
                Id = order.OrderId,
                Provider = order.Provider.ToString()
            };
            await _dbContext.Orders.AddAsync(orderModel);
        }
        await _dbContext.SaveChangesAsync();
    }
}