using financesFlow.Dominio.Seguranca.Criptografia;
using BC = BCrypt.Net.BCrypt;

namespace financesFlow.Infra.Seguranca.Criptograf
{
    internal class BCrypt : IEncriptadorSenha
    {
        public string Encript(string senha)
        {
            string senhaHash = BC.HashPassword(senha);

            return senhaHash;
        }
    }
}
