namespace financesFlow.Aplicacao.useCase.Arquivo;
public interface IGeraArquivoDespesaUseCase
{
    Task<byte[]> GeraArquivo(DateOnly mes);
}
