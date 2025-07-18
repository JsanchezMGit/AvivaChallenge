using Microsoft.EntityFrameworkCore;
using Payment.Application.Interfaces;
using Payment.Data;
using Payment.Enterprice.Entities;
using Payment.Models;

namespace Payment.Repository;

public class OrderRepository : IRepository<OrderEntity>
{
    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private readonly AppDbContext _dbContext;
    public async Task AddAsync(OrderEntity input)
    {
        var orderModel = new OrderModel
        {
            Id = input.OrderId,
            Provider = input.Provider.ToString()
        };
        await _dbContext.Orders.AddAsync(orderModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderEntity>> GetAllAsync() =>
        await _dbContext.Orders
            .Select(c => new OrderEntity(c.Id, c.Provider))
            .ToListAsync();

    public async Task<OrderEntity> GetByIdAsync(string id)
    {
        var order = await _dbContext.Orders.FindAsync(id);
        return order != null ?
            new OrderEntity(order.Id, order.Provider) :
            throw new KeyNotFoundException("El id de la orden no se entcontro");
    }
}
