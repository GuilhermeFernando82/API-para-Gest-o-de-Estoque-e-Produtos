namespace MeuProjeto.Application.Interfaces;

public interface IDeleteProductUseCase
{
    Task ExecuteAsync(Guid id);
}
