using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace financesFlow.Infra;
public static class ExtensaoInjecaoDependencia
{
    public static void AdicionarInfra(this IServiceCollection services)
    {
        services.AddScoped<IRepositorioDespensa, RepositorioDespesa>();
    }
}
