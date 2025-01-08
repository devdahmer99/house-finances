using financesFlow.Dominio.Entidades;

namespace financesFlow.Infra.DataAccess.Repositories.Usuario
{
    public class RepositorioUsuario
    {
        private readonly financesFlowDbContext _dbContext;

        private RepositorioUsuario(financesFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CriaUsuario(Dominio.Entidades.Usuario usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
        }
    }
}
