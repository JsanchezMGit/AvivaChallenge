namespace Payment.Application.Interfaces;

public interface ISyncRepository<T>
{
    Task SyncOrdersAsync(IEnumerable<T> orders);
}
