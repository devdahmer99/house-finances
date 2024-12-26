using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Infra.DataAccess;
using financesFlow.Infra.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace financesFlow.Infra;
public static class ExtensaoInjecaoDependencia
{
    public static void AdicionarInfra(this IServiceCollection services, IConfiguration configuration)
    {
        AdicionarDbContext(services, configuration);
        AdicionarRepositorios(services);
    }

    private static void AdicionarRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
        services.AddScoped<IRepositorioDespesaSomenteLeitura, RepositorioDespesa>();
        services.AddScoped<IRepositorioDepesaSomenteEscrita, RepositorioDespesa>();
    }

    private static void AdicionarDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));

        services.AddDbContext<financesFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
