using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Repositories.Usuarios
{
    public interface IRepositorioUsuarioSomenteEscrita
    {
        Task CriaUsuario(Usuario usuario);
        Task Delete(Usuario user);
    }
}
