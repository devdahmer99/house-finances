namespace financesFlow.Dominio.Repositories.Usuarios
{
    public interface IRepositorioUsuarioSomenteLeitura
    {
        Task<bool> ExisteUsuarioAtivoComEmail(string email);
        Task<Dominio.Entidades.Usuario?> BuscaUsuarioPorEmail(string email);
    }
}
