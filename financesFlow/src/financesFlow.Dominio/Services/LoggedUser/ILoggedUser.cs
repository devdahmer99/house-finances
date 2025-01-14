using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Services.LoggedUser
{
    public interface ILoggedUser
    {
        Task<Usuario> Get();
    }
}
