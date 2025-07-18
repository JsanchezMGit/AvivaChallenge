namespace Payment.Application.Interfaces;

public interface IRepository<T>
{
    Task<T> GetByIdAsync(string id);
    Task AddAsync(T input);
    Task<IEnumerable<T>> GetAllAsync();
}