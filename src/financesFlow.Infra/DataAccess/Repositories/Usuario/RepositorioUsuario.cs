using System.ComponentModel.DataAnnotations;
using financesFlow.Dominio.Repositories.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace financesFlow.Infra.DataAccess.Repositories.Usuario
{
    public class RepositorioUsuario : IRepositorioUsuarioSomenteLeitura, IRepositorioUsuarioSomenteEscrita, IRepositorioUsuarioSomenteAtualizacao
    {
        private readonly financesFlowDbContext _db;
        public RepositorioUsuario(financesFlowDbContext db) => _db = db;

        public async Task<Dominio.Entidades.Usuario?> BuscaUsuarioPorEmail(string email)
        {
            return await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

        public async Task CriaUsuario(Dominio.Entidades.Usuario usuario)
        {
            await _db.Usuarios.AddAsync(usuario);
        }

        public async Task Delete(Dominio.Entidades.Usuario user)
        {
            var userToRemove = await _db.Usuarios.FirstAsync(u => u.Id == user.Id);
            _db.Usuarios.Remove(userToRemove);
        }

        public async Task<bool> ExisteUsuarioAtivoComEmail(string email)
        {
            return await _db.Usuarios.AnyAsync(user => user.Email.Equals(email));
        }

        public async Task<Dominio.Entidades.Usuario> GetById(long id)
        {
            return await _db.Usuarios.FirstAsync(user => user.Id == id);
        }

        public void Update(Dominio.Entidades.Usuario user)
        {
            _db.Usuarios.Update(user);
        }
    }
}
