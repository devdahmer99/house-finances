namespace financesFlow.Aplicacao.useCase.Arquivo.Excel;
public interface IGeraArquivoExcelDespesaUseCase
{
    Task<byte[]> GeraArquivo(DateOnly mes);
}
