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

        public bool VerificaSenha(string senha, string senhaHash)
        {
            return BC.Verify(senha, senhaHash);
        }
    }
}
