namespace Payment.Application.Interfaces;

public interface IExternalServiceAdapter<T>
{
    Task<IEnumerable<T>> GetOrdersAsync(string provider);
    Task<T> GetOrderAsync(string id, string provider);
}
