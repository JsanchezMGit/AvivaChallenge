using Payment.Enterprice.Entities;

namespace Payment.Application.Interfaces;

public interface IPresenter<TEntity, TViewModel>
{
    TViewModel Present(TEntity entity);
    IEnumerable<TViewModel> Present(IEnumerable<TEntity> entities);
}
