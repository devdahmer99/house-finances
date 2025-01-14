using financesFlow.Dominio.Enums;

namespace financesFlow.Dominio.Entidades;
public class Usuario
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public Guid IdentificadorUsuario { get; set; } = Guid.NewGuid();
    public string Permissao { get; set; } = Permissoes.USUARIO;

    public ICollection<Despesa>? Despesas { get; set; }
}
