namespace Payment.Application.Interfaces;

public interface IMapper<TDto, TOutput>
{
    public TOutput ToEntity(TDto dto);
}
