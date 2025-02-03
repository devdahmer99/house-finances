using financesFlow.Dominio.Seguranca.Criptografia;
using BC = BCrypt.Net.BCrypt;

namespace financesFlow.Infra.Seguranca.Criptograf
{
    internal class BCrypt : IEncriptadorSenha
    {
        public string Encript(string password)
        {
            string passwordHash = BC.HashPassword(password);

            return passwordHash;
        }

        public bool Verify(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
