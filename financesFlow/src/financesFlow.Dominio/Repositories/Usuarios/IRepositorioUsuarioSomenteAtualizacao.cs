namespace financesFlow.Dominio.Repositories.Usuarios
{
    public interface IRepositorioUsuarioSomenteAtualizacao
    {
        Task<Dominio.Entidades.Usuario> GetById(long id);
        void Update(Dominio.Entidades.Usuario user);
    }
}
