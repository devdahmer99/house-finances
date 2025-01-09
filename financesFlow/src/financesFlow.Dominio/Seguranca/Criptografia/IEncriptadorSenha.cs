namespace financesFlow.Dominio.Seguranca.Criptografia
{
    public interface IEncriptadorSenha
    {
        string Encript(string senha);
        public bool VerificaSenha(string senha, string senhaHash);
    }
}
