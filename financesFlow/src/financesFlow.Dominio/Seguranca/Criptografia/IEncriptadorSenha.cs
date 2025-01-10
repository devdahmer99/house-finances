namespace financesFlow.Dominio.Seguranca.Criptografia
{
    public interface IEncriptadorSenha
    {
        string Encript(string senha);
        public bool Verify(string senha, string senhaHash);
    }
}
