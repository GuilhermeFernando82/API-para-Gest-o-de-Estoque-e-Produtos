namespace MeuProjeto.Application.UseCases;

public interface IDeleteProductUseCase
{
    Task ExecuteAsync(Guid id);
}
