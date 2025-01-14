using System.Runtime.ConstrainedExecution;
using CommonTestsUtilitis.Entidades;
using financesFlow.Dominio.Seguranca.Criptografia;
using financesFlow.Infra.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private financesFlow.Dominio.Entidades.Usuario _user;
        private string _password;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<financesFlowDbContext>(config =>
                    {
                        config.UseInMemoryDatabase("InMemoryDbForTesting");
                        config.UseInternalServiceProvider(provider);
                    });

                    var scope = services.BuildServiceProvider().CreateAsyncScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<financesFlowDbContext>();
                    var encriptador = scope.ServiceProvider.GetRequiredService<IEncriptadorSenha>();

                    StartDatabase(dbContext, encriptador);
                });
        }
        public string getNome() => _user.Nome;

        public string getEmail() => _user.Email;

        public string getPassword() => _password;

        private void StartDatabase(financesFlowDbContext dbContext, IEncriptadorSenha encriptador)
        {
            _user = UserBuilder.Build();

            _password = _user.Senha;

            _user.Senha = encriptador.Encript(_user.Senha);

            dbContext.Usuarios.Add(_user);

            dbContext.SaveChanges();
        }
    }
}
