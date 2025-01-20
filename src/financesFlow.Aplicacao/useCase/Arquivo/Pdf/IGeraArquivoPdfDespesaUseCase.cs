namespace financesFlow.Aplicacao.useCase.Arquivo.Pdf;
public interface IGeraArquivoPdfDespesaUseCase
{
    Task<byte[]> GeraArquivo(DateOnly mes);
}
