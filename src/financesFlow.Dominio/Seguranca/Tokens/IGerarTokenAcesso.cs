using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Seguranca.Tokens
{
    public interface IGerarTokenAcesso
    {
        string Generate(Usuario usuario);
    }
}
