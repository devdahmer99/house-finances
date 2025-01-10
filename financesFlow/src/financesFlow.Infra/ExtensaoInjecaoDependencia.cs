using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Dominio.Repositories.Usuarios;
using financesFlow.Dominio.Seguranca.Criptografia;
using financesFlow.Dominio.Seguranca.Tokens;
using financesFlow.Infra.DataAccess;
using financesFlow.Infra.DataAccess.Repositories.Despesa;
using financesFlow.Infra.DataAccess.Repositories.Usuario;
using financesFlow.Infra.Seguranca.Tokens;
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
        AddToken(services,configuration);
        services.AddScoped<IEncriptadorSenha, Seguranca.Criptograf.BCrypt>();
    }

    private static void AdicionarRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
        services.AddScoped<IRepositorioDespesaSomenteLeitura, RepositorioDespesa>();
        services.AddScoped<IRepositorioDepesaSomenteEscrita, RepositorioDespesa>();
        services.AddScoped<IRepositorioDespesaSomenteAtualizacao, RepositorioDespesa>();
        services.AddScoped<IRepositorioUsuarioSomenteEscrita, RepositorioUsuario>();
        services.AddScoped<IRepositorioUsuarioSomenteLeitura, RepositorioUsuario>();
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IGerarTokenAcesso>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }
    private static void AdicionarDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));

        services.AddDbContext<financesFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
