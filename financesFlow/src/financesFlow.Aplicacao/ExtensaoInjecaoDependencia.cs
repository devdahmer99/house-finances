using financesFlow.Aplicacao.AutoMapper;
using financesFlow.Aplicacao.useCase.Arquivo.Excel;
using financesFlow.Aplicacao.useCase.Arquivo.Pdf;
using financesFlow.Aplicacao.useCase.Despesas.Atualiza;
using financesFlow.Aplicacao.useCase.Despesas.Busca;
using financesFlow.Aplicacao.useCase.Despesas.BuscaPorId;
using financesFlow.Aplicacao.useCase.Despesas.BuscaTotal;
using financesFlow.Aplicacao.useCase.Despesas.Deleta;
using financesFlow.Aplicacao.useCase.Despesas.Registrar;
using financesFlow.Aplicacao.useCase.Usuarios.Criar;
using financesFlow.Aplicacao.useCase.Usuarios.Login;
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
        services.AddScoped<ICriarUsuarioUseCase, CriaUsuarioUseCase>();
        services.AddScoped<IFazLoginUsuario, LoginUsuarioUseCase>();
    }
}
