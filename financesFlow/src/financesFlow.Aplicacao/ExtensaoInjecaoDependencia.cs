using financesFlow.Aplicacao.useCase.Despesa.Registrar;
using Microsoft.Extensions.DependencyInjection;

namespace financesFlow.Aplicacao;
public static class ExtensaoInjecaoDependencia
{
    public static void AdicionarAplicacao(this IServiceCollection services)
    {
        services.AddScoped<IRegistrarDespesaUseCase, RegistrarDespesaUseCase>();
    }
}
