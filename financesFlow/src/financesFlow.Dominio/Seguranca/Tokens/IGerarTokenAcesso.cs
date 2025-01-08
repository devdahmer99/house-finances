using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Seguranca.Tokens
{
    public interface IGerarTokenAcesso
    {
        string GerarTokenAcesso(Usuario usuario);
    }
}
