using financesFlow.Aplicacao.AutoMapper;
using financesFlow.Aplicacao.useCase.Arquivo.Excel;
using financesFlow.Aplicacao.useCase.Arquivo.Pdf;
using financesFlow.Aplicacao.useCase.Despesa.Atualiza;
using financesFlow.Aplicacao.useCase.Despesa.Busca;
using financesFlow.Aplicacao.useCase.Despesa.BuscaPorId;
using financesFlow.Aplicacao.useCase.Despesa.BuscaTotal;
using financesFlow.Aplicacao.useCase.Despesa.Deleta;
using financesFlow.Aplicacao.useCase.Despesa.Registrar;
using Microsoft.Extensions.DependencyInjection;

namespace financesFlow.Aplicacao;
public static class ExtensaoInjecaoDependencia
{
    public static void AdicionarAplicacao(this IServiceCollection services)
    {
        AdicionarAutoMapper(services);
        AdicionarUseCase(services);
    }


    private static void AdicionarAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AdicionarUseCase(IServiceCollection services)
    {
        services.AddScoped<IRegistrarDespesaUseCase, RegistrarDespesaUseCase>();
        services.AddScoped<IBuscaDespesaUseCase, BuscaDespesaUseCase>();
        services.AddScoped<IBuscaDespesaPorIdUseCase, BuscaDespesaPorIdUseCase>();
        services.AddScoped<IDeletaDespesaUseCase, DeletaDespesaUseCase>();
        services.AddScoped<IAtualizaDespesaUseCase, AtualizaDespesaUseCase>();
        services.AddScoped<IGeraArquivoExcelDespesaUseCase, GeraArquivoExcelDespesaUseCase>();
        services.AddScoped<IGeraArquivoPdfDespesaUseCase, GeraArquivoPdfDespesaUseCase>();
        services.AddScoped<IBuscaTotalDespesasUseCase, BuscaTotalDespesasUseCase>();
    }
}
