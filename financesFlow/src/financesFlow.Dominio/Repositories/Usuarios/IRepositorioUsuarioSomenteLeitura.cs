namespace financesFlow.Dominio.Repositories.Usuarios
{
    public interface IRepositorioUsuarioSomenteLeitura
    {
        Task<bool> ExisteUsuarioAtivoComEmail(string email);
    }
}
